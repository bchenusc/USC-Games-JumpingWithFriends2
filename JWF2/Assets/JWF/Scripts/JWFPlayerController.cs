using UnityEngine;
using InControl;

namespace JWF
{
	public class JWFPlayerController : MonoBehaviour
	{
		public uint PlayerID = 0;

		private JWFPlayerActions playerActions;
		public JWFPlayerActions Actions
		{
			get { return Actions; }
		}

		void Start()
		{

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
