using UnityEngine;

namespace JWF.ClassicMap
{
	public class JWFClassicMapGoal : MonoBehaviour
	{
		public PlayerTeam OwningGoal = PlayerTeam.Blue;

		void OnTriggerEnter(Collider c)
		{
			if ( c.gameObject.CompareTag( "Ball" ) )
			{
				int lastTouch = c.gameObject.GetComponent<JWFClassicMapBall>().GetLastTouch();
				BallScored( lastTouch );
			}
		}

		void BallScored(int playerId)
		{
			// HACKY
			PlayerTeam teamGetsScore = OwningGoal == PlayerTeam.Red ? PlayerTeam.Blue : PlayerTeam.Red;
			JWFClassicMapScoreManager.Get.AddScore( playerId, teamGetsScore );
		}
	}
}
