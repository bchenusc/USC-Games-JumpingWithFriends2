using UnityEngine;
using System.Collections;

namespace JWF
{
	public class JWFGoal : MonoBehaviour
	{
		public PlayerTeam OwningGoal = PlayerTeam.Blue;

		void OnTriggerEnter(Collider c)
		{
			if ( c.gameObject.CompareTag( "Ball" ) )
			{
				int lastTouch = c.gameObject.GetComponent<JWFBall>().GetLastTouch();
				BallScored(lastTouch);
			}
		}

		void BallScored(int playerId)
		{
			// HACKY
			PlayerTeam teamGetsScore = OwningGoal == PlayerTeam.Red ? PlayerTeam.Blue : PlayerTeam.Red;
			JWFScoreManager.Get.AddScore( playerId , teamGetsScore );
		}
	}
}
