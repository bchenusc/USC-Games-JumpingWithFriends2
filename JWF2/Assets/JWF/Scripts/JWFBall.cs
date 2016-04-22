using UnityEngine;

namespace JWF
{
	public class JWFBall : MonoBehaviour
	{
		public Vector3 SpawnPosition;
		private int _LastTouch = 0;

		public int GetLastTouch()
		{
			return _LastTouch;
		}

		void OnCollisionEnter(Collision c)
		{
			if ( c.gameObject.CompareTag( "Killzone" ) )
			{
				BallDied();
			}
			else if (c.gameObject.CompareTag("Player"))
			{
				_LastTouch = c.gameObject.GetComponent<JWFPlayerController>().PlayerData.ID;
			}
		}

		void BallDied()
		{
			_LastTouch = 0;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = SpawnPosition;
			gameObject.SetActive( false );
		}

		public static void RespawnBall(GameObject ball)
		{
			ball.SetActive( true );
		}
	}
}