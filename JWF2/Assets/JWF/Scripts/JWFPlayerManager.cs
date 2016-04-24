using UnityEngine;
using System.Collections.Generic;
using InControl;
using JWF.ClassicMap;

namespace JWF
{
	public class JWFPlayerManager : Singleton<JWFPlayerManager>
	{
		static int MAX_PLAYERS = 4;

		List<JWFPlayerData> _players = new List<JWFPlayerData>( MAX_PLAYERS );

		public int GetPlayerCount()
		{
			return _players.Count;
		}

		public PlayerTeam GetPlayerTeam(int playerId)
		{
			return GetPlayerWithID( playerId ).Team;
		}
		
		public JWFPlayerData GetPlayerWithID(int id)
		{
			foreach (JWFPlayerData player in _players)
			{
				if (player.ID == id)
				{
					return player;
				}
			}
			return null;
		}

		void OnDeviceDetached(InputDevice inputDevice)
		{
			var player = FindPlayerUsingJoystick( inputDevice );
			if ( player == null )
			{
				RemovePlayer( player.ID );
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
			JWFPlayerData player = null;
			foreach ( JWFPlayerData p in _players )
			{
				if ( p.ID == playerId )
				{
					player = p;
					_players.Remove( player );
					player.Reset();
					return;
				}
			}
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
			return FindPlayerUsingJoystick( inputDevice ) == null;
		}

		JWFPlayerData FindPlayerUsingJoystick(InputDevice inputDevice)
		{
			foreach ( var player in _players )
			{
				if ( player.Actions.Device == inputDevice )
				{
					return player;
				}
			}
			return null;
		}

		bool IsPlayerAlreadyRegistered(int playerID)
		{
			foreach ( JWFPlayerData p in _players )
			{
				if ( p.ID == playerID )
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
				return null;
			}
			Debug.Log( "Create Keyboard Player " + playerID );

			// Keyboard player for only player 1 and player 2.
			JWFClassicMapPlayerActions actions;
			actions = JWFClassicMapPlayerActions.CreateWithKeyboardBindings( playerID );

			var player = new JWFPlayerData(playerID, actions, DetermineTeam(playerID));
			_players.Add( player );
			return player;
		}

		public JWFPlayerData CreateJoystickPlayer(InputDevice inputDevice)
		{
			if ( !ThereIsNoPlayerUsingThisJoystick( inputDevice ) )
			{
				Debug.Log( "Device in use" );
				return null;
			}
			Debug.Log( "Create joystick player " + inputDevice );
			int playerID = _players.Count + 1;

			if ( playerID <= MAX_PLAYERS )
			{
				JWFClassicMapPlayerActions actions;

				// Create either keyboard only controls or controller controls!
				if ( inputDevice == null )
				{
					// Keyboard player for only player 1 and player 2.
					actions = JWFClassicMapPlayerActions.CreateWithKeyboardBindings( playerID );
				}
				else
				{
					// Controller
					actions = JWFClassicMapPlayerActions.CreateWithJoystickBindings( playerID );
					actions.Device = inputDevice;
				}

				var player = new JWFPlayerData(playerID, actions, DetermineTeam(playerID));
				_players.Add( player );
				return player;
			}
			return null;
		}

		// Hack for determining the team.
		private PlayerTeam DetermineTeam(int playerID)
		{
			return playerID == 1 || playerID == 3 ? PlayerTeam.Blue : PlayerTeam.Red;
		}

		protected override bool ShouldDestroyOnLoad()
		{
			return false;
		}
	}
}
