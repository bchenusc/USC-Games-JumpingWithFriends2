using UnityEngine;

namespace JWF.ClassicMap
{
	public class JWFClassicMapGoal : MonoBehaviour
	{
		public AudioClip GoalScoredSound;
		private float _GoalScoredSoundVolume = 0.2f;
		private AudioSource GoalScoredSource;

		public PlayerTeam OwningGoal = PlayerTeam.Blue;

		void Start()
		{
			GoalScoredSource = gameObject.AddComponent<AudioSource>();
		}

		void OnTriggerEnter(Collider c)
		{
			if ( c.gameObject.CompareTag( GameStatics.BALL_TAG ) )
			{
				int lastTouch = c.gameObject.GetComponent<JWFClassicMapBall>().GetLastTouch();
				BallScored( lastTouch );
			}
		}

		void BallScored(int playerId)
		{
			SoundManager.Get.PlaySingle( GoalScoredSource, GoalScoredSound, _GoalScoredSoundVolume );

			// HACKY
			PlayerTeam teamGetsScore = OwningGoal == PlayerTeam.Red ? PlayerTeam.Blue : PlayerTeam.Red;
			JWFClassicMapScoreManager.Get.AddScore( playerId, teamGetsScore );
		}
	}
}
