using UnityEngine;
using System.Collections;
using InControl;

namespace JWF.InGameHUD
{

	public class JWFInGameHUDManagerBase : MonoBehaviour
	{
		protected JWFInGameHUDActions keyboardListener;
		protected JWFInGameHUDActions joystickListener;

		public enum JWFInGameHUDState
		{
			WaitingForPlayers = 0,
			ReadyToPlay = 1,
			Pause = 2,
			Playing = 3
		}

		protected virtual void Start()
		{
			JWFPlayerManager.Get.Init();
			JWFPlayerManager.Get.RemoveAllPlayers();
			keyboardListener.ClearInputState();
			joystickListener.ClearInputState();

#if !UNITY_EDITOR
			Cursor.visible = false;
#endif
		}

		protected virtual void OnEnable()
		{
			Debug.Log( "onEnabled" );
			joystickListener = JWFInGameHUDActions.CreateWithJoystickBindings();
			keyboardListener = JWFInGameHUDActions.CreateWithKeyboardBindings();
		}

		protected virtual void OnDisable()
		{
			joystickListener.Destroy();
			keyboardListener.Destroy();
		}

		protected bool AcceptWasPressed(JWFInGameHUDActions actions)
		{
			return actions.Accept.WasPressed;
		}

		protected bool StartWasPressed(JWFInGameHUDActions actions)
		{
			return actions.Start.WasPressed;
		}

		protected bool LeftWasPressed(JWFInGameHUDActions actions)
		{
			return actions.Left.WasPressed;
		}

		protected bool RightWasPressed(JWFInGameHUDActions actions)
		{
			return actions.Right.WasPressed;
		}

		protected bool BackWasPressed(JWFInGameHUDActions actions)
		{
			return actions.Back.WasPressed;
		}
	}
}
