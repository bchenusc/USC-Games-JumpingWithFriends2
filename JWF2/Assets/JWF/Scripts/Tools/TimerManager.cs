using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using VCSKicksCollection;

public delegate void TimerCallback();

public enum ETimerStatus {
	Pending,
	Active,
	Paused,
	Executing,
}

public class TimerHandle : System.Object {

	private const int INVALID_HANDLE = -1;
	private static int LastAssignedHandle = 0;

	private int Handle = INVALID_HANDLE;

	public bool IsValid() {
		return this.Handle != TimerHandle.INVALID_HANDLE;
	}

	public void MakeValid() {
		if (!this.IsValid()) {
			this.Handle = TimerHandle.LastAssignedHandle++;
		}
	}

	public void Invalidate() {
		this.Handle = INVALID_HANDLE;
	}

	public override bool Equals(object obj) {
		if (obj == null) {
			return false;
		}

		TimerHandle h = obj as TimerHandle;
		if ((object)h == null) {
			return false;
		}

		return this.Handle == h.Handle;
	}

	public bool Equals(TimerHandle h) {
		if ((object)h == null) {
			return false;
		}

		return this.Handle == h.Handle;
	}

	public override int GetHashCode()
	{
		return this.Handle;
	}

	public static bool operator ==(TimerHandle a, TimerHandle b) {
		if (System.Object.ReferenceEquals(a, b)) {
			return true;
		}

		if (((object)a == null) || ((object)b == null)) {
			return false;
		}

		return a.Handle == b.Handle;
	}

	public static bool operator !=(TimerHandle a, TimerHandle b) {
		return !(a == b);
	}

	public TimerHandle Clone() {
		TimerHandle Ret = new TimerHandle();
		Ret.Handle = this.Handle;
		return Ret;
	}
}

public class TimerManager : Singleton<TimerManager> {

	private class TimerData : System.IComparable<TimerData> {
		public bool bLooping;
		public ETimerStatus Status;
		public float Rate;
		public double ExpireTime;
		public TimerCallback Callback;
		public TimerHandle Handle;

		public TimerData() {
			this.bLooping = false;
			this.Status = ETimerStatus.Active;
			this.Rate = 0.0f;
			this.ExpireTime = 0.0f;
			this.Callback = null;
			this.Handle = null;
		}

		public void Clear() {
			this.Callback = null;
			this.Handle.Invalidate();
		}

		public int CompareTo(TimerData other)
		{
			if (other == null) return 1;

			return this.ExpireTime.CompareTo(other.ExpireTime);
		}

		public TimerData Clone() {
			TimerData Ret = new TimerData();
			Ret.bLooping = this.bLooping;
			Ret.Status = this.Status;
			Ret.Rate = this.Rate;
			Ret.ExpireTime = this.ExpireTime;
			Ret.Callback = this.Callback;
			Ret.Handle = this.Handle.Clone();
			return Ret;
		}
	}

	private int LastTickedFrame = int.MaxValue;
	private double InternalTime = 0.0f;

	private PriorityQueue<TimerData> ActiveTimers = new PriorityQueue<TimerData>();
	private List<TimerData> PausedTimers = new List<TimerData>();
	private List<TimerData> PendingTimers = new List<TimerData>();

	TimerData CurrentlyExecutingTimer = null;

	protected TimerManager() {}

	protected override bool ShouldDestroyOnLoad()
	{
		return true;
	}

	public void SetTimer(TimerHandle InOutHandle, TimerCallback InCallback, float InRate, bool InRepeating = false, float InFirstDelay = -1.0f) {
		if (InOutHandle == null || InCallback == null) {
			return;
		}

		if (InOutHandle.IsValid()) {
			this.ClearTimer(InOutHandle);
		}

		if (InRate > 0.0f) {
			InOutHandle.MakeValid();

			TimerData NewTimer = new TimerData();
			NewTimer.Handle = InOutHandle.Clone();
			NewTimer.Callback = InCallback;

			this.InternalSetTimer(NewTimer, InRate, InRepeating, InFirstDelay);
		}
	}

	public void SetTimerForNextTick(TimerCallback InCallback) {
		if (InCallback == null) {
			return;
		}

		TimerData NewTimer = new TimerData();
		NewTimer.Rate = 0.0f;
		NewTimer.bLooping = false;
		NewTimer.Callback = InCallback;
		NewTimer.ExpireTime = this.InternalTime;
		NewTimer.Status = ETimerStatus.Active;
		NewTimer.Handle = new TimerHandle();
		ActiveTimers.Enqueue(NewTimer);
	}

	private void InternalSetTimer(TimerData NewTimer, float InRate, bool InRepeating, float InFirstDelay) {
		if (NewTimer.Handle.IsValid() && NewTimer.Callback != null) {
			NewTimer.Rate = InRate;
			NewTimer.bLooping = InRepeating;

			float FirstDelay = (InFirstDelay >= 0.0f) ? InFirstDelay : InRate;

			if (HasBeenTickedThisFrame()) {
				NewTimer.ExpireTime = this.InternalTime + FirstDelay;
				NewTimer.Status = ETimerStatus.Active;
				ActiveTimers.Enqueue(NewTimer);
			} else {
				NewTimer.ExpireTime = FirstDelay;
				NewTimer.Status = ETimerStatus.Pending;
				PendingTimers.Add(NewTimer);
			}
		}
	}

	public void ClearTimer(TimerHandle InHandle) {
		if (!InHandle.IsValid()) {
			return;
		}

		int Index = -1;
		TimerData Timer = FindTimer(InHandle, ref Index);

		if (Timer != null) {
			switch (Timer.Status) {
			case ETimerStatus.Pending:
				PendingTimers.RemoveAtSwap(Index);
				break;
			
			case ETimerStatus.Active:
				ActiveTimers.RemoveAt(Index);
				break;
			
			case ETimerStatus.Paused:
				PausedTimers.RemoveAtSwap(Index);
				break;
			
			case ETimerStatus.Executing:
				CurrentlyExecutingTimer.Clear();
				break;
			default:
				break;
			}
		}
	}

	public void ClearAllTimers() {
		this.ActiveTimers.Clear();
		this.PausedTimers.Clear();
		this.PendingTimers.Clear();

		if (this.CurrentlyExecutingTimer != null) {
			this.CurrentlyExecutingTimer.Clear();
		}
	}

	public void ClearAllTimers(object ForObject) {
		if (ForObject != null) {

			int OldActiveListSize = this.ActiveTimers.Count;

			for (int i = 1; i < this.ActiveTimers.Values.Count; ++i) {
				if (this.ActiveTimers.Values[i].Callback != null &&
					this.ActiveTimers.Values[i].Callback.Target == ForObject) {
					this.ActiveTimers.Values.RemoveAtSwap(i--);
				}
			}

			if (this.ActiveTimers.Count != OldActiveListSize) {
				this.ActiveTimers.Heapify();
			}

			for (int i = 0; i < this.PausedTimers.Count; ++i) {
				if (this.PausedTimers[i].Callback != null &&
					this.PausedTimers[i].Callback.Target == ForObject) {
					this.PausedTimers.RemoveAtSwap(i--);
				}
			}

			for (int i = 0; i < this.PendingTimers.Count; ++i) {
				if (this.PendingTimers[i].Callback != null &&
					this.PendingTimers[i].Callback.Target == ForObject) {
					this.PendingTimers.RemoveAtSwap(i--);
				}
			}

			if (this.CurrentlyExecutingTimer != null && this.CurrentlyExecutingTimer.Callback.Target == ForObject) {
				this.CurrentlyExecutingTimer.Clear();
			}
		}
	}

	public void PauseTimer(TimerHandle InHandle) {
		if (InHandle == null || !InHandle.IsValid()) {
			return;
		}

		int Index = -1;
		TimerData Timer = FindTimer(InHandle, ref Index);

		if (Timer != null && Timer.Status != ETimerStatus.Paused) {
			ETimerStatus PreviousStatus = Timer.Status;

			if (PreviousStatus != ETimerStatus.Executing || Timer.bLooping) {
				TimerData NewTimer = Timer.Clone();
				NewTimer.Status = ETimerStatus.Paused;

				this.PausedTimers.Add(NewTimer);

				if (PreviousStatus != ETimerStatus.Pending) {
					NewTimer.ExpireTime = NewTimer.ExpireTime - this.InternalTime;
				}
			}

			switch (PreviousStatus) {
			case ETimerStatus.Active:
				this.ActiveTimers.RemoveAt(Index);
				break;

			case ETimerStatus.Pending:
				this.PendingTimers.RemoveAtSwap(Index);
				break;
	
			case ETimerStatus.Executing:
				this.CurrentlyExecutingTimer.Clear();
				break;
			}
		}
	}

	public void ResumeTimer(TimerHandle InHandle) {
		if (InHandle == null || !InHandle.IsValid()) {
			return;
		}

		int Index = FindTimerInList(this.PausedTimers, InHandle);

		if (Index != -1) {
			TimerData Timer = PausedTimers[Index];

			if (HasBeenTickedThisFrame()) {
				Timer.ExpireTime += this.InternalTime;
				Timer.Status = ETimerStatus.Active;
				this.ActiveTimers.Enqueue(Timer);
			} else {
				Timer.Status = ETimerStatus.Pending;
				this.PendingTimers.Add(Timer);
			}

			this.PausedTimers.RemoveAtSwap(Index);
		}
	}

	void Update() {
		this.InternalTime += Time.deltaTime;

		while (this.ActiveTimers.Count > 0) {
			TimerData Top = this.ActiveTimers.Peek();
			if (this.InternalTime > Top.ExpireTime) {
				this.CurrentlyExecutingTimer = ActiveTimers.Dequeue();
				CurrentlyExecutingTimer.Status = ETimerStatus.Executing;

				int CallCount = CurrentlyExecutingTimer.bLooping ?
					(int)((this.InternalTime - this.CurrentlyExecutingTimer.ExpireTime) / this.CurrentlyExecutingTimer.Rate) + 1
					: 1;

				for (int i = 0; i < CallCount; ++i) {

					if (this.CurrentlyExecutingTimer.Callback == null) {
						break;
					}

					this.CurrentlyExecutingTimer.Callback();

					if (this.CurrentlyExecutingTimer.Status != ETimerStatus.Executing) {
						break;
					}
				}

				if (this.CurrentlyExecutingTimer.bLooping && this.CurrentlyExecutingTimer.Status == ETimerStatus.Executing) {
					if (this.CurrentlyExecutingTimer.Callback != null && this.CurrentlyExecutingTimer.Callback.Target != null) {
						this.CurrentlyExecutingTimer.ExpireTime += CallCount * this.CurrentlyExecutingTimer.Rate;
						this.CurrentlyExecutingTimer.Status = ETimerStatus.Active;
						this.ActiveTimers.Enqueue(this.CurrentlyExecutingTimer.Clone());
					}
				}

				this.CurrentlyExecutingTimer.Clear();
			}
			else {
				break;
			}
		}

		this.LastTickedFrame = Time.frameCount;

		if (this.PendingTimers.Count > 0) {
			foreach (TimerData Timer in this.PendingTimers) {
				Timer.ExpireTime += this.InternalTime;
				Timer.Status = ETimerStatus.Active;
				this.ActiveTimers.Enqueue(Timer);
			}
			PendingTimers.Clear();
		}
	}

	private TimerData FindTimer(TimerHandle InHandle, ref int OutIndex) {
		if (InHandle.IsValid ()) {

			if (this.CurrentlyExecutingTimer != null && this.CurrentlyExecutingTimer.Handle == InHandle) {
				OutIndex = -1;
				return this.CurrentlyExecutingTimer;
			}

			int ActiveTimer = FindTimerInList(this.ActiveTimers.Values, InHandle);
			if (ActiveTimer != -1) {
				OutIndex = ActiveTimer;
				return this.ActiveTimers.Values[ActiveTimer];
			}

			int PausedTimer = FindTimerInList(this.PausedTimers, InHandle);
			if (PausedTimer != -1) {
				OutIndex = PausedTimer;
				return this.PausedTimers[PausedTimer];
			}

			int PendingTimer = FindTimerInList(this.PendingTimers, InHandle);
			if (PendingTimer != -1) {
				OutIndex = PendingTimer;
				return this.PendingTimers[PendingTimer];
			}
		}

		return null;
	}

	private int FindTimerInList(List<TimerData> List, TimerHandle Handle) {
		if (Handle.IsValid()) {

			for (int i = 0; i < List.Count; ++i) {
				if (List[i] != null && List[i].Handle == Handle) {
					return i;
				}
			}
		}

		return -1;
	}

	private bool HasBeenTickedThisFrame() {
		return this.LastTickedFrame == Time.frameCount;
	}
}


