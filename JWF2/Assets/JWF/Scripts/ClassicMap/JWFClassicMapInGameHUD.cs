using UnityEngine;
using System.Collections;
using JWF.InGameHUD;
using InControl;

namespace JWF.ClassicMap
{
	public class JWFClassicMapInGameHUD : JWFInGameHUDManagerBase
	{
		private JWFClassicMapInit _Initter;

		public enum HUDState
		{
			WaitingForPlayers = 1,
			Playing = 2,
			Pause = 3
		}

		private HUDState _HUDState = HUDState.WaitingForPlayers;

		protected override void Start()
		{
			base.Start();
			_Initter = GameObject.FindGameObjectWithTag( GameStatics.SCENE_INIT_TAG ).GetComponent<JWFClassicMapInit>();

			// Turn off all UI at the bottom of the screen.
			foreach ( GameObject ui in _Initter.PlayerBottomUI )
			{
				ui.SetActive( false );
			}
		}

		void Update()
		{
			// Only works in Waiting for Player state.
			WaitingForPlayersHUDActions();
		}

		void WaitingForPlayersHUDActions()
		{
			if ( _HUDState == HUDState.WaitingForPlayers)
			{
				// Joystick controls.
				if ( JoinButtonWasPressedOnJoystick( joystickListener ) )
				{
					var inputDevice = InputManager.ActiveDevice;

					// If controller isn't already being used.
					if ( JWFPlayerManager.Get.ThereIsNoPlayerUsingThisJoystick( inputDevice ) )
					{
						CreatePlayer( false, inputDevice, 0 /*Null playerID = joystick*/ );
					}
				}

				// Keyboard controls.
				int joiningPlayer = JoinButtonWasPressedOnKeyboard(keyboardListener);
				if ( joiningPlayer != 0 )
				{
					CreatePlayer( true, null /*Keyboard player has no inputDevice*/, joiningPlayer );
				}

				// Start pressed - Trying to start the game.
				if ( StartWasPressed( keyboardListener ) || StartWasPressed( joystickListener ) )
				{
					StartPressed();
				}
			}
		}

		// Starts the game.
		void StartPressed()
		{
			int playerCount = JWFPlayerManager.Get.GetPlayerCount();
			if ( playerCount == 2 || playerCount == 4 )
			{
				_HUDState = HUDState.Playing;
				_Initter.StartPreGame();
			}
		}

		JWFPlayerData CreatePlayer(bool isKeyboardPlayer, InputDevice inputDevice, int playerID)
		{
			JWFPlayerData data;
			if ( isKeyboardPlayer )
				data = JWFPlayerManager.Get.CreateKeyboardPlayer( playerID );
			else
				data = JWFPlayerManager.Get.CreateJoystickPlayer( inputDevice );

			// Turn on Player's bottom UI.
			if ( data != null )
			{
				_Initter.PlayerBottomUI[data.ID - 1].SetActive( true );
			}

			return data;
		}

		int JoinButtonWasPressedOnKeyboard(JWFInGameHUDActions actions)
		{
			if ( actions.Keyboard1A.WasPressed ) return 1;
			if ( actions.Keyboard2A.WasPressed ) return 2;
			return 0;
		}

		int RemoveButtonWasPressedOnKeybaord(JWFInGameHUDActions actions)
		{
			if ( actions.Keyboard1B.WasPressed ) return 1;
			if ( actions.Keyboard2B.WasPressed ) return 2;
			return 0;
		}

		bool JoinButtonWasPressedOnJoystick(JWFInGameHUDActions actions)
		{
			return actions.Accept.WasPressed;
		}

		bool BackButtonWasPressed(JWFInGameHUDActions actions)
		{
			return actions.Back.WasPressed;
		}
	}
}
