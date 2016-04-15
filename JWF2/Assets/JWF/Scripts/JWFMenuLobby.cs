using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using InControl;

namespace JWF
{
	public class JWFMenuLobby : JWFMenuBase
	{
		// Set in inspector.
		public JWFPlayerManager _PlayerManager;
		public GameObject[] JoinButtons;
		public GameObject[] JoinInfoPanels;
		public GameObject[] PlayerStatues;

		private JWFMenuManager _MenuManager;
		private List<JWFMenuBase> _LobbyStates = new List<JWFMenuBase>();

		void Start()
		{
			_LobbyStates.Add( gameObject.GetComponent<JWFMenuStart>() ); // 0 Go back to Start
			_MenuManager = gameObject.GetComponent<JWFMenuManager>();

			foreach ( GameObject g in PlayerStatues )
			{
				g.SetActive( false );
			}
		}

		public override Vector3 GetCameraPosition()
		{
			return new Vector3( 10, 0.71f, -10.36f );
		}

		public override JWFMenuState GetState()
		{
			return JWFMenuState.MenuLobby;
		}

		public override void MenuUpdate()
		{
			// Joystick controls.
			if ( JoinButtonWasPressedOnListener( GetJoystickListener() ) )
			{
				var inputDevice = InputManager.ActiveDevice;

				if ( ThereIsNoPlayerUsingThisJoystick( inputDevice ) )
				{
					CreateJoystickPlayer( inputDevice );
				}
			}

			// Keyboard controls.
			int joiningPlayer = JoinButtonWasPressedOnKeyboard(GetKeyboardListener());
			if ( joiningPlayer != 0 )
			{
				CreateKeyboardPlayer( joiningPlayer );
			}

			int removingPlayer = RemoveButtonWasPressedOnKeybaord(GetKeyboardListener());
			if (removingPlayer != 0)
			{
				RemovePlayer( removingPlayer );
			}
		}

		JWFMenuActions GetJoystickListener()
		{
			return _MenuManager.joystickListener;
		}

		JWFMenuActions GetKeyboardListener()
		{
			return _MenuManager.keyboardListener;
		}

		bool JoinButtonWasPressedOnListener(JWFMenuActions actions)
		{
			return actions.Start.WasPressed;
		}

		int JoinButtonWasPressedOnKeyboard(JWFMenuActions actions)
		{
			if ( actions.P1Start.WasPressed ) return 1;
			if ( actions.P2Start.WasPressed ) return 2;
			return 0;
		}

		int RemoveButtonWasPressedOnKeybaord(JWFMenuActions actions)
		{
			if ( actions.P1Back.WasPressed ) return 1;
			if ( actions.P2Back.WasPressed ) return 2;
			return 0;
		}

		bool ThereIsNoPlayerUsingThisJoystick(InputDevice inputDevice)
		{
			return _PlayerManager.ThereIsNoPlayerUsingThisJoystick( inputDevice );
		}

		JWFPlayerData CreateJoystickPlayer(InputDevice inputDevice)
		{
			return _PlayerManager.CreateJoystickPlayer( inputDevice );
		}

		JWFPlayerData CreateKeyboardPlayer(int playerID)
		{
			PlayerStatues[playerID - 1].SetActive( true );
			return _PlayerManager.CreateKeyboardPlayer( playerID );
		}

		void RemovePlayer(int playerId)
		{
			PlayerStatues[playerId - 1].SetActive( false );
			_PlayerManager.RemovePlayer( playerId );
		}
	}

}
