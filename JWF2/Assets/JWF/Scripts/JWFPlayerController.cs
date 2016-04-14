using UnityEngine;
using InControl;

namespace JWF
{
	public class JWFPlayerController : MonoBehaviour
	{
		private int _playerID = 0;
		public uint PlayerID
		{
			get { return _playerID; }
			set { _playerID = value; }
		}

		private JWFPlayerActions _playerActions;
		public JWFPlayerActions Actions
		{
			get { return Actions; }
			set { _playerActions = value; }
		}

		void Update()
		{
			if ( Actions.Jump.WasPressed )
			{
				PerformJump();
			}
		}

		void PerformJump()
		{
			Debug.Log( "Perform Jump" );
		}

		void PerformMove(float x)
		{
			Debug.Log( "Perform Move" );
		}
	}
}
