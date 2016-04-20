using UnityEngine;
using InControl;

namespace JWF
{
	public class JWFPlayerController : MonoBehaviour
	{
		private int _playerID = 0;
		public int PlayerID
		{
			get { return _playerID; }
			set { _playerID = value; }
		}

		private JWFPlayerActions _playerActions = null;
		public JWFPlayerActions Actions
		{
			get { return Actions; }
			set { _playerActions = value; }
		}

		void Update()
		{
			if ( _playerActions.Jump.WasPressed )
			{
				PerformJump();
			}
			if (_playerActions.Left.WasPressed)
			{
				TiltLeft();
			}
			if (_playerActions.Right.WasPressed)
			{
				TiltRight();
			}
		}

		void PerformJump()
		{
			Debug.Log( "Player " + PlayerID + " jumped." );

		}

		void TiltLeft()
		{
			Debug.Log( "Player " + PlayerID + " tilted left." );
		}

		void TiltRight()
		{
			Debug.Log( "Player " + PlayerID + " tilted right." );

		}
	}
}
