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
			new Vector3( -2,0,0),
			new Vector3(-1,0,0),
			new Vector3(1,0,0),
			new Vector3(2,0,0)
		};

		private List<JWFPlayerController> _players = new List<JWFPlayerController>(MAX_PLAYERS);

		JWFPlayerActions keyboardListener;
		JWFPlayerActions joystickListener;

		void OnEnable()
		{
			InputManager.OnDeviceDetached += OnDeviceDetached;
			keyboardListener = JWFPlayerActions.CreateWithKeyboardBindings();
			joystickListener = JWFPlayerActions.CreateWithJoystickBindings();
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

		void Update()
		{
			if ( JoinButtonWasPressedOnListener( joystickListener ) )
			{
				var inputDevice = InputManager.ActiveDevice;
				if ( ThereIsNoPlayerUsingThisJoystick( inputDevice ) )
				{
					CreatePlayer( inputDevice );
				}
			}
		}

		bool JoinButtonWasPressedOnListener(JWFPlayerActions actions)
		{
			return actions.Start.WasPressed;
		}

		bool ThereIsNoPlayerUsingThisJoystick(InputDevice inputDevice)
		{
			return FindPlayerUsingJoystick( inputDevice ) == null;
		}

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
	}
}
