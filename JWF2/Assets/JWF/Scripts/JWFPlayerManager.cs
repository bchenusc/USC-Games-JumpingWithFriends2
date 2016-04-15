using UnityEngine;
using System.Collections.Generic;
using InControl;

namespace JWF
{
	public struct JWFPlayerData
	{
		public int id;
		public JWFPlayerActions actions;
	};

	public class JWFPlayerManager : MonoBehaviour
	{
		static int MAX_PLAYERS = 4;

		List<JWFPlayerData> _players = new List<JWFPlayerData>( MAX_PLAYERS );

		JWFMenuActions joystickListener;
		JWFMenuActions keyboardListener;

		void OnEnable()
		{
			InputManager.OnDeviceDetached += OnDeviceDetached;
			joystickListener = JWFMenuActions.CreateWithJoystickBindings();
			keyboardListener = JWFMenuActions.CreateWithKeyboardBindings();
		}

		void OnDisbale()
		{
			InputManager.OnDeviceDetached -= OnDeviceDetached;
			joystickListener.Destroy();
			keyboardListener.Destroy();
		}

		void OnDeviceDetached(InputDevice inputDevice)
		{
			var player = FindPlayerUsingJoystick( inputDevice );
			if ( player.id != 0 )
			{
				RemovePlayer( player );
			}
		}

		public void RemovePlayer(JWFPlayerData player)
		{
			_players.Remove( player );
			player.actions = null;
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

		public bool ThereIsNoPlayerUsingThisJoystick(InputDevice inputDevice)
		{
			return FindPlayerUsingJoystick( inputDevice ).id == 0;
		}

		JWFPlayerData FindPlayerUsingJoystick(InputDevice inputDevice)
		{
			foreach ( var player in _players )
			{
				if ( player.actions.Device == inputDevice )
				{
					return player;
				}
			}
			return new JWFPlayerData();
		}

		public JWFPlayerData CreateKeyboardPlayer(int playerID)
		{
			Debug.Log( "Create Keyboard Player " + playerID );
			var player = new JWFPlayerData();
			player.id = playerID;

			// Keyboard player for only player 1 and player 2.
			JWFPlayerActions actions;
			actions = JWFPlayerActions.CreateWithKeyboardBindings( playerID );
			player.actions = actions;

			_players.Add( player );
			return player;
		}

		public JWFPlayerData CreateJoystickPlayer(InputDevice inputDevice)
		{
			Debug.Log( "Create joystick player " + inputDevice );
			int playerID = _players.Count + 1;

			if ( playerID <= MAX_PLAYERS )
			{
				var player = new JWFPlayerData();
				player.id = playerID;

				// Create either keyboard only controls or controller controls!
				if ( inputDevice == null )
				{
					// Keyboard player for only player 1 and player 2.
					JWFPlayerActions actions;
					actions = JWFPlayerActions.CreateWithKeyboardBindings( playerID );
					player.actions = actions;
				}
				else
				{
					// Controller
					JWFPlayerActions actions;
					actions = JWFPlayerActions.CreateWithJoystickBindings( playerID );
					actions.Device = inputDevice;
					player.actions = actions;
				}
				_players.Add( player );
				return player;
			}
			return new JWFPlayerData();
		}
	}
}
