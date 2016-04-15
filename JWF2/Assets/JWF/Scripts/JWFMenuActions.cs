using UnityEngine;
using InControl;

namespace JWF
{
	public class JWFMenuActions : PlayerActionSet
	{
		// Player specific for keyboard only.
		public PlayerAction Left;
		public PlayerAction Right;
		public PlayerAction Jump;
		public PlayerAction Start;
		public PlayerAction P1Start;
		public PlayerAction P2Start;
		public PlayerAction Back;

		public JWFMenuActions()
		{
			Left = CreatePlayerAction( "Move Left" );
			Right = CreatePlayerAction( "Move Right" );
			Jump = CreatePlayerAction( "Jump" );

			Start = CreatePlayerAction( "Start" );
			P1Start = CreatePlayerAction( "P1Start" );
			P2Start = CreatePlayerAction( "P2Start" );

			Back = CreatePlayerAction( "Back" );
		}

		public static JWFMenuActions CreateWithKeyboardBindings()
		{
			var actions = new JWFMenuActions();
			// Used in menus that are player ambiguous.
			actions.P1Start.AddDefaultBinding( Key.W );
			actions.P2Start.AddDefaultBinding( Key.UpArrow );
			actions.Start.AddDefaultBinding( Key.PadEnter );
			actions.Start.AddDefaultBinding( Key.Return );

			actions.Back.AddDefaultBinding( Key.Backspace );
			actions.Back.AddDefaultBinding( Key.S );
			actions.Back.AddDefaultBinding( Key.DownArrow );

			actions.Left.AddDefaultBinding( Key.A );
			actions.Right.AddDefaultBinding( Key.D );
			actions.Jump.AddDefaultBinding( Key.W );
			actions.Left.AddDefaultBinding( Key.LeftArrow );
			actions.Right.AddDefaultBinding( Key.RightArrow );
			actions.Jump.AddDefaultBinding( Key.UpArrow );
			return actions;
		}

		public static JWFMenuActions CreateWithJoystickBindings()
		{
			var actions = new JWFMenuActions();
			actions.Left.AddDefaultBinding( Key.A );
			actions.Right.AddDefaultBinding( Key.D );
			actions.Jump.AddDefaultBinding( Key.W );

			actions.Left.AddDefaultBinding( Key.LeftArrow );
			actions.Right.AddDefaultBinding( Key.RightArrow );
			actions.Jump.AddDefaultBinding( Key.UpArrow );

			actions.Left.AddDefaultBinding( InputControlType.LeftStickLeft );
			actions.Right.AddDefaultBinding( InputControlType.LeftStickRight );
			actions.Jump.AddDefaultBinding( InputControlType.Action1 );

			actions.Left.AddDefaultBinding( InputControlType.DPadLeft );
			actions.Right.AddDefaultBinding( InputControlType.DPadRight );
			actions.Jump.AddDefaultBinding( InputControlType.DPadUp );

			actions.Start.AddDefaultBinding( InputControlType.Start );
			actions.Start.AddDefaultBinding( Key.PadEnter );
			return actions;
		}
	}
}

