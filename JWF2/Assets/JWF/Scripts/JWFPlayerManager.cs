using UnityEngine;
using System.Collections.Generic;
using InControl;


namespace JWF
{
	public class JWFPlayerManager : MonoBehaviour
	{
		/// <summary>
		/// Maximum players allowed in the game.
		/// </summary>
		static int MAX_PLAYERS = 4;

		public GameObject PlayerPrefab;

		private List<Vector3> _playerStartPositions = new List<Vector3>()
		{
			new Vector3( -2,3,0),
			new Vector3(-1,3,0),
			new Vector3(1,3,0),
			new Vector3(2,3,0)
		};

		List<JWFPlayerController> _players = new List<JWFPlayerController>( MAX_PLAYERS );

		JWFPlayerActions joystickListener;
		JWFPlayerActions keyboardListener;

		void OnEnable()
		{
			InputManager.OnDeviceDetached += OnDeviceDetached;
			joystickListener = JWFPlayerActions.CreateWithJoystickBindings(0);
			keyboardListener = JWFPlayerActions.CreateKeyboardMenuBindings();
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
			if ( player != null )
			{
				RemovePlayer( player );
			}
		}

		void RemovePlayer(JWFPlayerController player)
		{
			_players.Remove( player );
			player.Actions = null;
			Destroy( player.gameObject );
		}

		void Update()
		{
			// Joystick controls.
			if ( JoinButtonWasPressedOnListener( joystickListener ) )
			{
				var inputDevice = InputManager.ActiveDevice;

				if ( ThereIsNoPlayerUsingThisJoystick( inputDevice ) )
				{
					CreateJoystickPlayer( inputDevice );
				}
			}

			// Keyboard controls.
			int playerID = JoinButtonWasPressedOnKeyboard(keyboardListener);
			if (playerID != 0)
			{
				CreateKeyboardPlayer( playerID );
			}
		}

		/// <summary>
		/// Returns if the start button is pressed by a controller.
		/// </summary>
		/// <param name="actions"></param>
		/// <returns></returns>
		bool JoinButtonWasPressedOnListener(JWFPlayerActions actions)
		{
			return actions.Start.WasPressed;
		}

		/// <summary>
		/// 
		/// </summary>  
		/// <param name="actions"></param>
		/// <returns>0 = false; 1 = Player1; 2 = Player2 </returns>
		int JoinButtonWasPressedOnKeyboard(JWFPlayerActions actions)
		{
			if ( actions.P1Start.WasPressed ) return 1;
			if ( actions.P2Start.WasPressed ) return 2;
			return 0;
		}

		/// <summary>
		/// Returns if there is no player currently using specific joystick.
		/// </summary>
		/// <param name="inputDevice">THe joystick.</param>
		/// <returns></returns>
		bool ThereIsNoPlayerUsingThisJoystick(InputDevice inputDevice)
		{
			return FindPlayerUsingJoystick( inputDevice ) == null;
		}

		/// <summary>
		/// Finds a player that is using this joystick.
		/// </summary>
		/// <param name="inputDevice">The joystick.</param>
		/// <returns></returns>
		JWFPlayerController FindPlayerUsingJoystick(InputDevice inputDevice)
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

		/// <summary>
		/// Create a keyboard only player.
		/// </summary>
		/// <param name="playerID"></param>
		/// <returns></returns>
		JWFPlayerController CreateKeyboardPlayer(int playerID)
		{
			Debug.Log( "Create Keyboard Player " + playerID );
			var spawnPosition = _playerStartPositions[playerID];
			var gameObject = (GameObject) Instantiate( PlayerPrefab, spawnPosition, Quaternion.identity );
			var player = gameObject.GetComponent<JWFPlayerController>();
			player.PlayerID = playerID;

			// Keyboard player for only player 1 and player 2.
			JWFPlayerActions actions;
			actions = JWFPlayerActions.CreateWithKeyboardBindings( playerID );
			player.Actions = actions;

			_players.Add( player );
			return player;
		}

		/// <summary>
		/// Creates a joystick player.
		/// </summary>
		/// <param name="inputDevice"></param>
		/// <returns></returns>
		JWFPlayerController CreateJoystickPlayer(InputDevice inputDevice)
		{
			Debug.Log( "Create joystick player " + inputDevice );
			int playerID = _players.Count + 1;

			if ( playerID <= MAX_PLAYERS )
			{
				var spawnPosition = _playerStartPositions[playerID];
				var gameObject = (GameObject) Instantiate( PlayerPrefab, spawnPosition, Quaternion.identity );
				var player = gameObject.GetComponent<JWFPlayerController>();
				player.PlayerID = playerID;

				// Create either keyboard only controls or controller controls!
				if (inputDevice == null)
				{
					// Keyboard player for only player 1 and player 2.
					JWFPlayerActions actions;
					actions = JWFPlayerActions.CreateWithKeyboardBindings( playerID );
					player.Actions = actions;
				}
				else
				{
					// Controller
					JWFPlayerActions actions;
					actions = JWFPlayerActions.CreateWithJoystickBindings( playerID );
					actions.Device = inputDevice;
					player.Actions = actions;
				}

				_players.Add( player );
				return player;
			}

			return null;
		}
	}
}
