using UnityEngine;

namespace JWF.SmashMap
{
	public class JWFSmashMapPlayerController : MonoBehaviour
	{
		private Rigidbody _Rigidbody;
		private JWFSmashMapPlayerSounds _PlayerSounds;
		private Vector3 _SpawnLocation;

		private JWFPlayerData _PlayerData = null;
		public JWFPlayerData PlayerData
		{
			get { return _PlayerData; }
			set { _PlayerData = value; }
		}

		private float _RealignForce = 1100;
		private float _RealignForceDampenerScaler = 0;
		private float _JumpForce = 700;
		private float _AirControl = 300;
		private float _TiltPower = 1400;
		private bool _Grounded = false;
		private bool _ShouldTiltLeft = false;
		private bool _ShouldTiltRight = false;
		private float _SpawnDelay = 2.0f;

		private TimerHandle _RespawnTimer = new TimerHandle();

		void Start()
		{
			_Rigidbody = gameObject.GetComponent<Rigidbody>();
			_PlayerSounds = gameObject.GetComponent<JWFSmashMapPlayerSounds>();
		}

		void FixedUpdate()
		{
			if ( _Grounded )
			{
				AddRealignForce();
			}
		}

		void AddRealignForce()
		{
			float sign = Mathf.Sign(transform.up.x);
			float oppDot = OppositeDot(Vector3.up, transform.up);
			_Rigidbody.AddTorque( sign * oppDot * Vector3.forward * _RealignForce );
		}

		float OppositeDot(Vector3 v1, Vector3 v2)
		{
			return 1f - Mathf.Abs( Vector3.Dot( Vector3.Normalize( v1 ), Vector3.Normalize( v2 ) ) );
		}

		void Update()
		{
			JWFSmashMapPlayerActions Actions = (JWFSmashMapPlayerActions) PlayerData.Actions;

			if ( Actions.Jump.WasPressed )
			{
				PerformJump();
			}
			if ( Actions.Left.WasPressed )
			{
				_ShouldTiltLeft = true;
			}
			if ( Actions.Right.WasPressed )
			{
				_ShouldTiltRight = true;
			}
			if ( Actions.Left.WasReleased )
			{
				_ShouldTiltLeft = false;
			}
			if ( Actions.Right.WasReleased )
			{
				_ShouldTiltRight = false;
			}
			if ( _ShouldTiltLeft )
			{
				TiltLeft();
			}
			if ( _ShouldTiltRight )
			{
				TiltRight();
			}
		}

		void PerformJump()
		{
			if ( _Grounded )
			{
				PerformJump_SFX();
				_Rigidbody.AddForce( transform.up * _JumpForce, ForceMode.Impulse );
			}
		}

		void PerformJump_SFX()
		{
			_PlayerSounds.PlayJumpSound();
		}

		void TiltLeft()
		{
			float realignDampener = 0.5f;
			if (_Grounded)
			{
				realignDampener = ( Mathf.Abs( Vector3.Dot( transform.up, Vector3.up ) + _RealignForceDampenerScaler ) );
			}
			_Rigidbody.AddTorque( Vector3.forward * _TiltPower * realignDampener);
			_Rigidbody.AddForce( Vector3.left * _AirControl );
		}

		void TiltRight()
		{
			float realignDampener = 0.5f;
			if ( _Grounded )
			{
				realignDampener = ( Mathf.Abs( Vector3.Dot( transform.up, Vector3.up ) + _RealignForceDampenerScaler ) );
			}
			_Rigidbody.AddTorque( -Vector3.forward * _TiltPower * realignDampener );
			_Rigidbody.AddForce( Vector3.right * _AirControl );
		}

		void OnCollisionStay(Collision c)
		{
			if ( !c.gameObject.CompareTag( JWFStatics.BALL_TAG ) )
			{
				_Grounded = true;
			}
		}

		void OnCollisionExit(Collision c)
		{
			_Grounded = false;
		}

		void OnCollisionEnter(Collision c)
		{
			if ( c.gameObject.CompareTag( JWFStatics.KILLZONE_TAG ) )
			{
				PlayerDied();
			}
		}

		void PlayerDied()
		{
			_Rigidbody.isKinematic = true;
			transform.GetComponent<CapsuleCollider>().enabled = false;
			transform.GetComponent<MeshRenderer>().enabled = false;
			transform.position = _SpawnLocation;
			TimerManager.Get.SetTimer( _RespawnTimer, Respawn, _SpawnDelay, false );
		}

		void Respawn()
		{
			_PlayerSounds.ResetPlayFallingSound();
			transform.GetComponent<CapsuleCollider>().enabled = true;
			transform.GetComponent<MeshRenderer>().enabled = true;
			_Rigidbody.isKinematic = false;
		}

		public void SetSpawnLocation(Vector3 spawnLocation)
		{
			_SpawnLocation = spawnLocation;
		}
	}
}
