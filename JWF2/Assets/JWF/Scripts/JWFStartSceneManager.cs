using UnityEngine;
using System.Collections.Generic;

namespace JWF
{ 

	public class JWFMenuManager : MonoBehaviour
	{
		JWFMenuActions keyboardListener;
		JWFMenuActions joystickListener;

        JWFCameraManager cameraMoverScript;

        private JWFMenuState state = JWFMenuState.StartScreen;
        private JWFMenuBase menuBase = new JWFMenuBase();

        void Start()
        {
            cameraMoverScript = Camera.main.GetComponent<JWFCameraManager>();
        }

        public void ChangeStateTo(JWFMenuState state)
        {
            if (this.state == state) return;
            if ((int)state >= System.Enum.GetNames(typeof(JWFMenuState)).Length) return;

            this.state = state;
            cameraMoverScript.MoveCameraByState(state);
        }

        void OnEnable()
		{
			joystickListener = JWFMenuActions.CreateWithJoystickBindings();
			keyboardListener = JWFMenuActions.CreateWithKeyboardBindings();
		}

		void OnDisbale()
		{
			joystickListener.Destroy();
			keyboardListener.Destroy();
		}

		bool StartButtonListenerWasPressed(JWFMenuActions actions)
		{
            return actions.Start.WasPressed;
		}

        bool LeftWasPressed(JWFMenuActions actions)
        {
            return actions.Left.WasPressed;
        }

        bool RightWasPressed(JWFMenuActions actions)
        {
            return actions.Right.WasPressed;
        }

        void Update()
        {
            if (StartButtonListenerWasPressed(joystickListener)
                || StartButtonListenerWasPressed(keyboardListener))
            {
                ChangeStateTo(state + 1);
            }

            if (LeftWasPressed(joystickListener) || LeftWasPressed(keyboardListener))
            {

            }

            if (RightWasPressed(joystickListener) || RightWasPressed(keyboardListener))
            {

            }
        }
	}
}
