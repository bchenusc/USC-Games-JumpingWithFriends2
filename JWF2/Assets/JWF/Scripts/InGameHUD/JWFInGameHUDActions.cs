using UnityEngine;
using InControl;

namespace JWF.InGameHUD
{
	public class JWFInGameHUDActions : PlayerActionSet
	{
		// Player specific for keyboard only.
		public PlayerAction Left;
		public PlayerAction Right;
		public PlayerAction Jump;

		public PlayerAction Start;
		public PlayerAction Accept;
		public PlayerAction Back;

		public PlayerAction Keyboard1A;
		public PlayerAction Keyboard1B;
		public PlayerAction Keyboard2A;
		public PlayerAction Keyboard2B;

		public JWFInGameHUDActions()
		{
			Left = CreatePlayerAction( "Move Left" );
			Right = CreatePlayerAction( "Move Right" );
			Jump = CreatePlayerAction( "Jump" );

			Start = CreatePlayerAction( "Start" );

			Keyboard1A = CreatePlayerAction( "Keyboard1A" );
			Keyboard2A = CreatePlayerAction( "Keyboard2A" );

			Keyboard1B = CreatePlayerAction( "Keyboard1B" );
			Keyboard2B = CreatePlayerAction( "Keyboard2B" );

			Accept = CreatePlayerAction( "Accept" );
			Back = CreatePlayerAction( "Back" );
		}

		public static JWFInGameHUDActions CreateWithKeyboardBindings()
		{
			var actions = new JWFInGameHUDActions();
			// Used in menus that are player ambiguous.
			actions.Start.AddDefaultBinding( Key.PadEnter );
			actions.Start.AddDefaultBinding( Key.Return );

			actions.Keyboard1A.AddDefaultBinding( Key.W );
			actions.Keyboard2A.AddDefaultBinding( Key.UpArrow );

			actions.Back.AddDefaultBinding( Key.Backspace );
			actions.Keyboard1B.AddDefaultBinding( Key.S );
			actions.Keyboard2B.AddDefaultBinding( Key.DownArrow );

			actions.Left.AddDefaultBinding( Key.A );
			actions.Right.AddDefaultBinding( Key.D );
			actions.Jump.AddDefaultBinding( Key.W );

			actions.Left.AddDefaultBinding( Key.LeftArrow );
			actions.Right.AddDefaultBinding( Key.RightArrow );
			actions.Jump.AddDefaultBinding( Key.UpArrow );
			return actions;
		}

		public static JWFInGameHUDActions CreateWithJoystickBindings()
		{
			var actions = new JWFInGameHUDActions();
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

			actions.Start.AddDefaultBinding( InputControlType.Command );
			actions.Start.AddDefaultBinding( Key.PadEnter );

			actions.Accept.AddDefaultBinding( InputControlType.Action1 );
			actions.Back.AddDefaultBinding( InputControlType.Action2 );
			return actions;
		}
	}
}

