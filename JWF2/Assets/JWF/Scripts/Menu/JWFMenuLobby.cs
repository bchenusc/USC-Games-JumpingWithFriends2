using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using InControl;

namespace JWF
{
	public class JWFMenuLobby : JWFMenuBase
	{
		// Set in inspector.
		public Text TopInfoText;
		public GameObject[] JoinButtons;
		public GameObject[] JoinInfoPanels;
		public GameObject[] PlayerStatues;

		private static string OneVOne = "Press Start When Ready!";
		private static string TwoVTwo = "Press Start When Ready!";
		private static string Waiting = "Waiting for Players...";

		bool _ReadyToTransitionToGame = false;

		private enum JWFMenuLobbyState
		{
			Back = 0,
			Gameplay = 1
		}

		private JWFMenuManager _MenuManager;
		private List<JWFMenuBase> _LobbyStates = new List<JWFMenuBase>();

		void Start()
		{
			_ReadyToTransitionToGame = false;
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

		public override void StartPressed()
		{
			if ( _ReadyToTransitionToGame )
			{
				JWFSceneManager.LoadLevel( Scenes.CLASSIC_MAP );
			}
		}

		public override void MenuUpdate()
		{
			// Joystick controls.
			if ( JoinButtonWasPressedOnListener( GetJoystickListener() ) )
			{
				var inputDevice = InputManager.ActiveDevice;

				if ( ThereIsNoPlayerUsingThisJoystick( inputDevice ) )
				{
					CreatePlayer( false, inputDevice, 0 /*Null playerID = joystick*/ );
				}
			}

			// Keyboard controls.
			int joiningPlayer = JoinButtonWasPressedOnKeyboard(GetKeyboardListener());
			if ( joiningPlayer != 0 )
			{
				CreatePlayer( true, null /*Keyboard player has no inputDevice*/, joiningPlayer );
			}

			if ( BackButtonWasPressed( GetJoystickListener() ) || BackButtonWasPressed( GetKeyboardListener() ) )
			{
				RemoveAllPlayers();
				_MenuManager.ChangeStateTo( _LobbyStates[(int) JWFMenuLobbyState.Back] );
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
			return actions.Accept.WasPressed;
		}

		int JoinButtonWasPressedOnKeyboard(JWFMenuActions actions)
		{
			if ( actions.Keyboard1A.WasPressed ) return 1;
			if ( actions.Keyboard2A.WasPressed ) return 2;
			return 0;
		}

		int RemoveButtonWasPressedOnKeybaord(JWFMenuActions actions)
		{
			if ( actions.Keyboard1B.WasPressed ) return 1;
			if ( actions.Keyboard2B.WasPressed ) return 2;
			return 0;
		}

		bool BackButtonWasPressed(JWFMenuActions actions)
		{
			return actions.Back.WasPressed;
		}

		bool ThereIsNoPlayerUsingThisJoystick(InputDevice inputDevice)
		{
			return JWFPlayerManager.Get.ThereIsNoPlayerUsingThisJoystick( inputDevice );
		}

		JWFPlayerData CreatePlayer(bool isKeyboardPlayer, InputDevice inputDevice, int playerID)
		{
			JWFPlayerData data;
			if ( isKeyboardPlayer )
				data = CreateKeyboardPlayer( playerID );
			else
				data = CreateJoystickPlayer( inputDevice );
			ChangeTopTextToReflectPlayerCount();
			return data;
		}

		void ChangeTopTextToReflectPlayerCount()
		{
			TopInfoText.text = Waiting;
			_ReadyToTransitionToGame = false;
			if ( JWFPlayerManager.Get.GetPlayerCount() == 2 )
			{
				TopInfoText.text = OneVOne;
				_ReadyToTransitionToGame = true;
			}
			else if ( JWFPlayerManager.Get.GetPlayerCount() == 4 )
			{
				TopInfoText.text = TwoVTwo;
				_ReadyToTransitionToGame = true;
			}
		}

		JWFPlayerData CreateJoystickPlayer(InputDevice inputDevice)
		{
			JWFPlayerData data = JWFPlayerManager.Get.CreateJoystickPlayer( inputDevice );
			if ( data != null )
			{
				PlayerStatues[data.ID - 1].SetActive( true );
			}
			return data;
		}

		JWFPlayerData CreateKeyboardPlayer(int whichPlayer)
		{
			JWFPlayerData data = JWFPlayerManager.Get.CreateKeyboardPlayer( whichPlayer );
			if ( data != null )
			{
				PlayerStatues[data.ID - 1].SetActive( true );
			}
			return data;
		}

		void RemovePlayer(int playerId)
		{
			Debug.Log( "Remove Player " + playerId );
			PlayerStatues[playerId - 1].SetActive( false );
			JWFPlayerManager.Get.RemovePlayer( playerId );
			ChangeTopTextToReflectPlayerCount();
		}

		void RemoveAllPlayers()
		{
			Debug.Log( "Remove All Players " );
			foreach ( GameObject statue in PlayerStatues )
			{
				statue.SetActive( false );
			}
			JWFPlayerManager.Get.RemoveAllPlayers();
			ChangeTopTextToReflectPlayerCount();
		}
	}

}
