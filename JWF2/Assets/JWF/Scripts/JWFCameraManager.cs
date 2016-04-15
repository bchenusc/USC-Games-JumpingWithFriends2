using UnityEngine;
using System.Collections;

namespace JWF
{
	public class JWFCameraManager : MonoBehaviour
	{
        public float CameraMoveSpeed = 0.5f;

		public void MoveCameraByState(JWFMenuState state)
		{
            Vector3 camPosition = JWFMenuDefines.CameraMenuPositions[(int)state];
            iTween.MoveTo(gameObject, camPosition, CameraMoveSpeed);
		}
	}
}
