using UnityEngine;
using System.Collections.Generic;

namespace JWF
{
	public enum JWFMenuState
	{
		MenuStart = 0,
		MenuLobby = 1,
		MenuOptions = 2
	}

	public class JWFMenuManager : MonoBehaviour
	{
		public JWFMenuActions keyboardListener;
		public JWFMenuActions joystickListener;
		JWFMenuCamera cameraManager;

		private JWFMenuBase menuState = null /* Default MenuStart in Start()*/;

		void Start()
		{
			cameraManager = Camera.main.GetComponent<JWFMenuCamera>();
			ChangeStateTo(gameObject.GetComponent<JWFMenuStart>());
			JWFPlayerManager.Get.Init();
			JWFPlayerManager.Get.RemoveAllPlayers();

#if !UNITY_EDITOR
			Cursor.visible = false;
#endif
		}

		public void ChangeStateTo(JWFMenuBase state)
		{
			if ( menuState != null && menuState.GetState() == state.GetState() )
			{
				return;
			}
			joystickListener.ClearInputState();
			keyboardListener.ClearInputState();
			menuState = state;
			cameraManager.MoveCameraTo( menuState.GetCameraPosition() );
		}

		void Update()
		{
			if ( AcceptWasPressed( joystickListener ) || AcceptWasPressed( keyboardListener ) )
			{
				menuState.AcceptPressed();
			}

			if ( StartWasPressed( joystickListener ) || StartWasPressed( keyboardListener ) )
			{
				menuState.StartPressed();
			}

			if ( LeftWasPressed( joystickListener ) || LeftWasPressed( keyboardListener ) )
			{
				menuState.LeftPressed();
			}

			if ( RightWasPressed( joystickListener ) || RightWasPressed( keyboardListener ) )
			{
				menuState.RightPressed();
			}

			if ( BackWasPressed( joystickListener ) || BackWasPressed( keyboardListener ) )
			{
				menuState.BackPressed();
			}

			menuState.MenuUpdate();
		}

		void OnEnable()
		{
			joystickListener = JWFMenuActions.CreateWithJoystickBindings();
			keyboardListener = JWFMenuActions.CreateWithKeyboardBindings();
		}

		void OnDisable()
		{
			joystickListener.Destroy();
			keyboardListener.Destroy();
		}

		bool AcceptWasPressed(JWFMenuActions actions)
		{
			return actions.Accept.WasPressed;
		}

		bool StartWasPressed(JWFMenuActions actions)
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

		bool BackWasPressed(JWFMenuActions actions)
		{
			return actions.Back.WasPressed;
		}
	}
}
