using UnityEngine;

/// <summary>
/// Classic Map camera can switch between multiple cameras on each team.
/// Team Cameras are cached via Inspector.
/// </summary>
namespace JWF.Camera
{
    public class JWFCameraSwitcher : MonoBehaviour
    {
        // Local Cache
        private TimerHandle _CameraSwitchHandle = new TimerHandle();
        private GameObject[] _CameraSwitchCache = null;
        private GameObject _MainCamera = null;
        private int _CurrentCamera = -1;

        void Start()
        {
            // Cache the main camera in the scene to switch back to it when switcher is done.
            _MainCamera = UnityEngine.Camera.main.gameObject;
        }

        /// <summary>
        /// Switches between the cameras for the specific team. Think "posed snap shots". 
        /// Delay is given by the Camera Switch Rate.
        /// </summary>
        /// <param name="team">Which Team's character you should switch.</param>
		public void StartCameraSwitcher(GameObject[] cams, float switchRate)
        {
            _CameraSwitchCache = cams;
            NextCamera();
            TimerManager.Get.SetTimer(_CameraSwitchHandle, NextCamera, switchRate, true);
        }

        /// <summary>
        /// Switch between cameras.
        /// </summary>
		private void NextCamera()
        {
            ++_CurrentCamera;
            if (_CurrentCamera >= _CameraSwitchCache.Length)
            {
                // Finished Swapping through all the cameras.
                ResetCameras();
                return;
            }
            _CameraSwitchCache[_CurrentCamera].SetActive(true);
        }

        /// <summary>
        /// Resets all alternative cameras back to the off state.
        /// Clears all timers associated with this class.
        /// Sets main camera back to the original camera.
        /// </summary>
        private void ResetCameras()
        {
            _CurrentCamera = -1;
            TimerManager.Get.ClearTimer(_CameraSwitchHandle);
            UnityEngine.Camera mainCam = _MainCamera.GetComponent<UnityEngine.Camera>();
            UnityEngine.Camera.SetupCurrent(mainCam);
            foreach (GameObject cam in _CameraSwitchCache)
            {
                cam.SetActive(false);
            }
        }
    }
}
