using UnityEngine;
using System.Collections;

namespace JWF
{
	public class JWFMenuBase : MonoBehaviour
	{
		protected float _ButtonScaleUp = 0.003f;
		protected float _ButtonScaleDown = 0.002f;
		protected float _ButtonScaleTime = 0.5f;

		public virtual Vector3 GetCameraPosition()
		{
			return new Vector3( 0, 0.71f, -10.36f );
		}

		public virtual JWFMenuState GetState()
		{
			return JWFMenuState.MenuStart;
		}

		public virtual void LeftPressed() { }

		public virtual void RightPressed() { }

		public virtual void AcceptPressed() { }

		public virtual void StartPressed() { }

		public virtual void BackPressed() { }

		public virtual void MenuUpdate() { }
	}

}
