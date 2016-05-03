using UnityEngine;
using System.Collections;
using JWF.InGameHUD;
using InControl;

namespace JWF.ClassicMap
{
	public class JWFClassicMapInGameHUD : JWFInGameHUDManagerBase
	{
		private JWFClassicMapInit _Initter;

		protected override void Start()
		{
			base.Start();
			_Initter = GameObject.FindGameObjectWithTag( GameStatics.SCENE_INIT_TAG ).GetComponent<JWFClassicMapInit>();
			GetJoystickListener().ClearInputState();
			GetKeyboardListener().ClearInputState();
		}

		void Update()
		{
			// Joystick controls.
			if ( JoinButtonWasPressedOnJoystick( GetJoystickListener() ) )
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

			int removingPlayer = RemoveButtonWasPressedOnKeybaord(GetKeyboardListener());
			if ( removingPlayer != 0 )
			{
				RemovePlayer( removingPlayer );
			}

			if ( BackButtonWasPressed( GetJoystickListener() ) || BackButtonWasPressed( GetKeyboardListener() ) )
			{
				RemoveAllPlayers();
			}

			// Start pressed - Trying to start the game.
			if (StartWasPressed(keyboardListener) || StartWasPressed(joystickListener))
			{
				StartPressed();
			}
		}

		void StartPressed()
		{
			int playerCount = JWFPlayerManager.Get.GetPlayerCount();
			if ( playerCount == 2 || playerCount == 4 )
			{
				_Initter.StartPreGame();
			}
		}

		bool JoinButtonWasPressedOnJoystick(JWFInGameHUDActions actions)
		{
			return actions.Accept.WasPressed ;
		}

		JWFPlayerData CreatePlayer(bool isKeyboardPlayer, InputDevice inputDevice, int playerID)
		{
			JWFPlayerData data;
			if ( isKeyboardPlayer )
				data = CreateKeyboardPlayer( playerID );
			else
				data = CreateJoystickPlayer( inputDevice );
			return data;
		}

		JWFPlayerData CreateJoystickPlayer(InputDevice inputDevice)
		{
			return JWFPlayerManager.Get.CreateJoystickPlayer( inputDevice );
		}

		JWFPlayerData CreateKeyboardPlayer(int playerID)
		{
			return JWFPlayerManager.Get.CreateKeyboardPlayer( playerID );
		}

		void RemovePlayer(int playerId)
		{
			JWFPlayerManager.Get.RemovePlayer( playerId );
		}

		void RemoveAllPlayers()
		{
			JWFPlayerManager.Get.RemoveAllPlayers();
		}

		JWFInGameHUDActions GetJoystickListener()
		{
			return joystickListener;
		}

		JWFInGameHUDActions GetKeyboardListener()
		{
			return keyboardListener;
		}

		bool ThereIsNoPlayerUsingThisJoystick(InputDevice inputDevice)
		{
			return JWFPlayerManager.Get.ThereIsNoPlayerUsingThisJoystick( inputDevice );
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

		bool BackButtonWasPressed(JWFInGameHUDActions actions)
		{
			return actions.Back.WasPressed;
		}
	}
}
