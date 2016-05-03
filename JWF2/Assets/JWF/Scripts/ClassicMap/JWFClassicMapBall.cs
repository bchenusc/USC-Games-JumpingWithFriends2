using UnityEngine;

namespace JWF.ClassicMap
{
	public class JWFClassicMapBall : MonoBehaviour
	{
		public Vector3 SpawnPosition;
		private int _LastTouch = 0;
		private Renderer _Renderer;
		private ParticleSystem _Trail;

		void Start()
		{
			_Renderer = GetComponent<Renderer>();
			_Trail = GetComponent<ParticleSystem>();
		}

		public int GetLastTouch()
		{
			return _LastTouch;
		}

		void OnCollisionEnter(Collision c)
		{
			if ( c.gameObject.CompareTag( JWFStatics.KILLZONE_TAG ) )
			{
				BallDied();
			}
			else if ( c.gameObject.CompareTag( JWFStatics.PLAYER_TAG ) )
			{
				JWFClassicMapPlayerController ctl =  c.gameObject.GetComponent<JWFClassicMapPlayerController>();
				_LastTouch = ctl.PlayerData.ID;
				Color color = ctl.PlayerData.Team == PlayerTeam.Red ? Color.red : Color.blue;
				_Renderer.material.color = color;
				_Trail.startColor = color;
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