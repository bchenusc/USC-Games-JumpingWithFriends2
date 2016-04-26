using UnityEngine;
using InControl;

namespace JWF.ClassicMap
{
	public class JWFClassicMapPlayerActions : PlayerActionSet
	{
		public PlayerAction Left;
		public PlayerAction Right;
		public PlayerAction Jump;
		public PlayerAction StartCommand;
		public PlayerAction Accept;

		public PlayerOneAxisAction Horizontal;

		public JWFClassicMapPlayerActions()
		{
			Left = CreatePlayerAction( "Move Left" );
			Right = CreatePlayerAction( "Move Right" );
			Jump = CreatePlayerAction( "Jump" );
			StartCommand = CreatePlayerAction( "StartCommand" );
			Accept = CreatePlayerAction( "Accept" );

			Horizontal = CreateOneAxisPlayerAction( Left, Right );
		}

		public static JWFClassicMapPlayerActions CreateWithKeyboardBindings(int playerID)
		{
			var actions = new JWFClassicMapPlayerActions();

			// Bindings for player 1 is default to WASD
			// Bindings for player 2 is default to Arrow Keys
			// Players 3 and 4 cannot use the keyboard.
			if ( playerID == 1 )
			{
				actions.Left.AddDefaultBinding( Key.A );
				actions.Right.AddDefaultBinding( Key.D );
				actions.Jump.AddDefaultBinding( Key.W );
				actions.StartCommand.AddDefaultBinding( Key.Escape );
				actions.Accept.AddDefaultBinding( Key.PadEnter );
				actions.Accept.AddDefaultBinding( Key.Return );
			}
			else if ( playerID == 2 )
			{
				actions.Left.AddDefaultBinding( Key.LeftArrow );
				actions.Right.AddDefaultBinding( Key.RightArrow );
				actions.Jump.AddDefaultBinding( Key.UpArrow );
				actions.StartCommand.AddDefaultBinding( Key.Escape );
				actions.Accept.AddDefaultBinding( Key.PadEnter );
				actions.Accept.AddDefaultBinding( Key.Return );
			}
			else
			{
				Debug.LogError( "Player " + playerID + " cannot use the keyboard." );
			}

			return actions;
		}

		public static JWFClassicMapPlayerActions CreateWithJoystickBindings(int playerID)
		{
			var actions = new JWFClassicMapPlayerActions();

			actions.Left.AddDefaultBinding( InputControlType.LeftStickLeft );
			actions.Right.AddDefaultBinding( InputControlType.LeftStickRight );
			actions.Jump.AddDefaultBinding( InputControlType.Action1 );

			actions.Left.AddDefaultBinding( InputControlType.DPadLeft );
			actions.Right.AddDefaultBinding( InputControlType.DPadRight );
			actions.Jump.AddDefaultBinding( InputControlType.DPadUp );

			actions.StartCommand.AddDefaultBinding( InputControlType.Command );
			actions.Accept.AddDefaultBinding( InputControlType.Action1 );

			return actions;
		}
	}

}
