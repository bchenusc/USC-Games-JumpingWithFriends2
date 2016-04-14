using UnityEngine;
using InControl;

namespace JWF
{
	public class JWFPlayerActions : PlayerActionSet
	{
		private int _KeyboardPlayer = 0;
		public int IsKeyboardPlayer
		{
			get { return _KeyboardPlayer; }
			set { _KeyboardPlayer = value; }
		}

		public PlayerAction Left;
		public PlayerAction Right;
		public PlayerAction Jump;
		public PlayerAction Start;

		// Player specific for keyboard only.
		public PlayerAction P1Start;
		public PlayerAction P2Start;

		public PlayerOneAxisAction Horizontal;

		public JWFPlayerActions()
		{
			Left = CreatePlayerAction( "Move Left" );
			Right = CreatePlayerAction( "Move Right" );
			Jump = CreatePlayerAction( "Jump" );
			Start = CreatePlayerAction( "Start" );
			P1Start = CreatePlayerAction( "P1Start" );
			P2Start = CreatePlayerAction( "P2Start" );
			Horizontal = CreateOneAxisPlayerAction( Left, Right );
		}

		public static JWFPlayerActions CreateKeyboardMenuBindings()
		{
			var actions = new JWFPlayerActions();
			// Used in menus that are player ambiguous.
			actions.P1Start.AddDefaultBinding( Key.W );
			actions.P2Start.AddDefaultBinding( Key.UpArrow );
			return actions;
		}

		public static JWFPlayerActions CreateWithKeyboardBindings(int playerID)
		{
			var actions = new JWFPlayerActions();

			// Bindings for player 1 is default to WASD
			// Bindings for player 2 is default to Arrow Keys
			// Players 3 and 4 cannot use the keyboard.
			if ( playerID == 1 )
			{
				actions.Left.AddDefaultBinding( Key.A );
				actions.Right.AddDefaultBinding( Key.D );
				actions.Jump.AddDefaultBinding( Key.W );
				actions.Start.AddDefaultBinding( Key.W );
			}
			else if ( playerID == 2 )
			{
				actions.Left.AddDefaultBinding( Key.LeftArrow );
				actions.Right.AddDefaultBinding( Key.RightArrow );
				actions.Jump.AddDefaultBinding( Key.UpArrow );
				actions.Start.AddDefaultBinding( Key.UpArrow );
			}
			else
			{
				Debug.LogError( "Player " + playerID + " cannot use the keyboard." );
			}

			return actions;
		}

		public static JWFPlayerActions CreateWithJoystickBindings(int playerID)
		{
			var actions = new JWFPlayerActions();

			// Bindings for player 1 is default to WASD
			// Bindings for player 2 is default to Arrow Keys
			// Players 3 and 4 cannot use the keyboard.
			if ( playerID == 1 )
			{
				actions.Left.AddDefaultBinding( Key.A );
				actions.Right.AddDefaultBinding( Key.D );
				actions.Jump.AddDefaultBinding( Key.W );
			}
			else if ( playerID == 2 )
			{
				actions.Left.AddDefaultBinding( Key.LeftArrow );
				actions.Right.AddDefaultBinding( Key.RightArrow );
				actions.Jump.AddDefaultBinding( Key.UpArrow );
			}

			actions.Left.AddDefaultBinding( InputControlType.LeftStickLeft );
			actions.Right.AddDefaultBinding( InputControlType.LeftStickRight );
			actions.Jump.AddDefaultBinding( InputControlType.Action1 );

			actions.Left.AddDefaultBinding( InputControlType.DPadLeft );
			actions.Right.AddDefaultBinding( InputControlType.DPadRight );
			actions.Jump.AddDefaultBinding( InputControlType.DPadUp );

			actions.Start.AddDefaultBinding( InputControlType.Start );

			return actions;
		}
	}

}
