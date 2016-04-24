using UnityEngine;

namespace JWF.ClassicMap
{
	public class JWFClassicMapCamera : MonoBehaviour
	{
		public GameObject[] RedGoalCameras;
		public GameObject[] BlueGoalCameras;

		private float _CameraSwitchRate = 0.4f;
		private int _CurrentCamera = -1;
		private GameObject _MainCamera;
		private TimerHandle _CameraSwitchHandle = new TimerHandle();

		public void Start()
		{
			_MainCamera = Camera.main.gameObject;
		}

		public void GoalScoredCameraBM(PlayerTeam team)
		{
			switch ( team )
			{
				case PlayerTeam.Red:
				NextCameraRed();
				_MainCamera.SetActive( false );
				TimerManager.Get.SetTimer( _CameraSwitchHandle, NextCameraRed, _CameraSwitchRate, true );
				break;

				case PlayerTeam.Blue:
				NextCameraBlue();
				_MainCamera.SetActive( false );
				TimerManager.Get.SetTimer( _CameraSwitchHandle, NextCameraBlue, _CameraSwitchRate, true );
				break;
			}
		}

		private void NextCameraRed()
		{
			++_CurrentCamera;
			if ( _CurrentCamera >= RedGoalCameras.Length )
			{
				ResetBMCameras( PlayerTeam.Red );
				return;
			}
			RedGoalCameras[_CurrentCamera].SetActive( true );
		}

		private void NextCameraBlue()
		{
			++_CurrentCamera;
			if ( _CurrentCamera >= BlueGoalCameras.Length )
			{
				ResetBMCameras( PlayerTeam.Blue );
				return;
			}
			BlueGoalCameras[_CurrentCamera].SetActive( true );
		}

		private void ResetBMCameras(PlayerTeam team)
		{
			GameObject[] cams = team == PlayerTeam.Red ? RedGoalCameras : BlueGoalCameras;
			_CurrentCamera = -1;
			TimerManager.Get.ClearTimer( _CameraSwitchHandle );
			_MainCamera.SetActive( true );
			foreach ( GameObject cam in cams )
			{
				cam.SetActive( false );
			}
		}
	}
}
