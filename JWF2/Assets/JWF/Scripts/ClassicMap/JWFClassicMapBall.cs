using UnityEngine;

namespace JWF.ClassicMap
{
	public class JWFClassicMapBall : MonoBehaviour
	{
		public Vector3 SpawnPosition;
		private int _LastTouch = 0;
		private Renderer _Renderer;

		void Start()
		{
			_Renderer = GetComponent<Renderer>();
		}

		public int GetLastTouch()
		{
			return _LastTouch;
		}

		void OnCollisionEnter(Collision c)
		{
			if ( c.gameObject.CompareTag( GameStatics.KILLZONE_TAG ) )
			{
				BallDied();
			}
			else if ( c.gameObject.CompareTag( GameStatics.PLAYER_TAG ) )
			{
				JWFClassicMapPlayerController controller =  c.gameObject.GetComponent<JWFClassicMapPlayerController>();
				_LastTouch = controller.PlayerData.ID;
				_Renderer.material.color = controller.PlayerData.Team == PlayerTeam.Red ? Color.red : Color.blue;
			}
		}

		void BallDied()
		{
			_LastTouch = 0;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.position = SpawnPosition;
		}
	}
}