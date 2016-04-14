using UnityEngine;
using InControl;
using System.Collections;

namespace JWF
{
	public class JWFMenuManager : MonoBehaviour
	{
		JWFMenuActions keyboardListener;
		JWFMenuActions joystickListener;

		private enum MenuState
		{
			StartScreen,
			Menu,
			GameLobby
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
	}
}
