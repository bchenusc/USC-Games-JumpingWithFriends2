using UnityEngine;
using InControl;

namespace JWF
{
	public class JWFPlayerActions : PlayerActionSet
	{
		public PlayerAction Left;
		public PlayerAction Right;
		public PlayerAction Jump;
		public PlayerAction Start;
		public PlayerOneAxisAction Horizontal;

		public JWFPlayerActions()
		{
			Left = CreatePlayerAction( "Move Left" );
			Right = CreatePlayerAction( "Move Right" );
			Jump = CreatePlayerAction( "Jump" );
			Jump = CreatePlayerAction( "Start" );
			Horizontal = CreateOneAxisPlayerAction( Left, Right );
		}

		public static JWFPlayerActions CreateWithKeyboardBindings(int player)
		{
			var actions = new JWFPlayerActions();

			// Bindings for player 1 is default to WASD
			// Bindings for player 2 is default to Arrow Keys
			// Players 3 and 4 cannot use the keyboard.
			if ( player == 1 )
			{
				actions.Left.AddDefaultBinding( Key.A );
				actions.Right.AddDefaultBinding( Key.D );
				actions.Jump.AddDefaultBinding( Key.W );
			}
			else if ( player == 2 )
			{
				actions.Left.AddDefaultBinding( Key.LeftArrow );
				actions.Right.AddDefaultBinding( Key.RightArrow );
				actions.Jump.AddDefaultBinding( Key.UpArrow );
			}
			else
			{
				Debug.LogError( "Player " + player + " cannot use the keyboard." );
			}
			return actions;
		}

		public static JWFPlayerActions CreateWithJoystickBindings()
		{
			var actions = new JWFPlayerActions();

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
