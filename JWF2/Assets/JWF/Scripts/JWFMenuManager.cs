using UnityEngine;
using InControl;
using System.Collections;

namespace JWF
{
	public enum JWFMenuState
	{
		StartScreen = 0,
		GameLobby = 1
	}

	private List<Vector3> _playerStartPositions = new List<Vector3>()
		{
			new Vector3( 0, 0.71f,-10.36f),
			new Vector3(10,0.71f,-10.36f),
		};

	public class JWFMenuManager : MonoBehaviour
	{
		JWFMenuActions keyboardListener;
		JWFMenuActions joystickListener;

		void OnEnable()
		{
			joystickListener = JWFMenuActions.CreateWithJoystickBindings();
			keyboardListener = JWFMenuActions.CreateWithKeyboardBindings();
		}

		void OnDisbale()
		{
			joystickListener.Destroy();
			keyboardListener.Destroy();
		}



		bool StartButtonListenerWasPressed(JWFMenuActions actions)
		{
			return actions.Start.WasPressed;
		}
	}
}
