using UnityEngine;
using InControl;

namespace JWF
{
	public class JWFPlayerController : MonoBehaviour
	{
		private Transform _CenterOfMass;
		private Rigidbody _Rigidbody;

		private JWFPlayerData _PlayerData = null;
		public JWFPlayerData PlayerData
		{
			get { return _PlayerData; }
			set { _PlayerData = value; }
		}

		private bool _Grounded = false;

		void Update()
		{
			if ( PlayerData.Actions.Jump.WasPressed )
			{
				PerformJump();
			}
			if ( PlayerData.Actions.Left.WasPressed)
			{
				TiltLeft();
			}
			if ( PlayerData.Actions.Right.WasPressed)
			{
				TiltRight();
			}
		}

		void PerformJump()
		{

		}

		void TiltLeft()
		{

		}

		void TiltRight()
		{

		}

		void FixedUpdate()
		{

		}

		void OnCollisionStay(Collision c)
		{

		}

		void OnCollisionExit(Collision c)
		{
			_Grounded = false;

		}

		void OnCollisionEnter(Collision c)
		{
			if (c.gameObject.CompareTag("Killzone"))
			{
				// Player died.
			}
		}
	}
}
