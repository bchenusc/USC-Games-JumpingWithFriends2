using UnityEngine;
using System.Collections.Generic;
using InControl;
using System;

namespace JWF
{
	public struct JWFPlayerData
	{
		public int id;
		public JWFPlayerActions actions;

		public void RemoveActions()
		{
			actions = null;
		}
	};

	public class JWFPlayerManager : Singleton<JWFPlayerManager>
	{
		static int MAX_PLAYERS = 4;

		List<JWFPlayerData> _players = new List<JWFPlayerData>( MAX_PLAYERS );

		public int GetPlayerCount()
		{
			return _players.Count;
		}

		void OnDeviceDetached(InputDevice inputDevice)
		{
			var player = FindPlayerUsingJoystick( inputDevice );
			if ( player.id != 0 )
			{
				RemovePlayer( player.id );
			}
		}

		public void RemoveAllPlayers()
		{
			for ( int i = 0 ; i < _players.Count ; i++ )
			{
				_players[i].RemoveActions();
			}
			_players.Clear();
		}

		public void RemovePlayer(int playerId)
		{
			JWFPlayerData player = new JWFPlayerData();
			foreach ( JWFPlayerData p in _players )
			{
				if ( p.id == playerId )
					player = p;
			}
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

		bool IsPlayerAlreadyRegistered(int playerID)
		{
			foreach ( JWFPlayerData p in _players )
			{
				if ( p.id == playerID )
				{
					return true;
				}
			}
			return false;
		}

		public JWFPlayerData CreateKeyboardPlayer(int playerID)
		{
			if ( IsPlayerAlreadyRegistered( playerID ) )
			{
				return new JWFPlayerData();
			}
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
			if ( !ThereIsNoPlayerUsingThisJoystick( inputDevice ) )
			{
				return new JWFPlayerData();
			}
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

		protected override bool ShouldDestroyOnLoad()
		{
			return false;
		}
	}
}
