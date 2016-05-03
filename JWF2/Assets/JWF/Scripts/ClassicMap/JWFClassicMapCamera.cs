using UnityEngine;
using JWF.Camera;

/// <summary>
/// Classic Map camera can switch between multiple cameras on each team.
/// Team Cameras are cached via Inspector.
/// </summary>
namespace JWF.ClassicMap
{
    [RequireComponent(typeof(JWFCameraSwitcher))]
    public class JWFClassicMapCamera : MonoBehaviour
    {
        // Set in Inspector
        public GameObject[] RedGoalCameras = null;
        public GameObject[] BlueGoalCameras = null;

        // Tweakables.
        private const float _CameraSwitchRate = 0.4f;

        // Local Cache
        private JWFCameraSwitcher _SwitcherScript = null;

        void Start()
        {
            _SwitcherScript = GetComponent<JWFCameraSwitcher>();
        }

        /// <summary>
        /// Switches between the cameras for the specific team. Think "posed snap shots". 
        /// Delay is given by the Camera Switch Rate.
        /// </summary>
        /// <param name="team">Which Team's cameras to switch.</param>
		public void GoalScoredCameraSwitcher(PlayerTeam team)
        {
            GameObject[] camsToSwitch = team == PlayerTeam.Red ? RedGoalCameras : BlueGoalCameras;
            _SwitcherScript.StartCameraSwitcher(camsToSwitch, _CameraSwitchRate);
        }
    }
}
