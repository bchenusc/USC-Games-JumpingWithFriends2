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

        private float _RealignForce = 1600;
        private float _JumpForce = 700;
        private float _TiltPower = 2000;
        private float _MomentumForce = 200;
        private bool _Grounded = false;
        private Vector3 _StartCenterOfMass;
        private bool _ShouldTiltLeft = false;
        private bool _ShouldTiltRight = false;

        void Start()
        {
            _Rigidbody = gameObject.GetComponent<Rigidbody>();
            _CenterOfMass = transform.FindChild("CenterOfMass");
            _StartCenterOfMass = _Rigidbody.centerOfMass;
            if (_CenterOfMass == null)
                Debug.LogError("No Center of Mass Found! on Player " + PlayerData.ID);
            _Rigidbody.centerOfMass = _CenterOfMass.position - transform.position;
        }

        void FixedUpdate()
        {
            if (_Grounded)
            {
                AddRealignForce();
            }
        }

        void AddRealignForce()
        {
            float sign = Mathf.Sign(transform.up.x);
            float oppDot = OppositeDot(Vector3.up, transform.up);
            _Rigidbody.AddTorque(sign * oppDot * Vector3.forward * _RealignForce);
        }

        float OppositeDot(Vector3 v1, Vector3 v2)
        {
            return 1f - Mathf.Abs(Vector3.Dot(Vector3.Normalize(v1), Vector3.Normalize(v2)));
        }

		void Update()
		{
            
			if ( PlayerData.Actions.Jump.WasPressed )
			{
				PerformJump();
			}
			if ( PlayerData.Actions.Left.WasPressed)
			{
                _ShouldTiltLeft = true;
			}
			if ( PlayerData.Actions.Right.WasPressed)
			{
                _ShouldTiltRight = true;
			}
            if (PlayerData.Actions.Left.WasReleased)
            {
                _ShouldTiltLeft = false;
            }
            if (PlayerData.Actions.Right.WasReleased)
            {
                _ShouldTiltRight = false;
            }
            if (_ShouldTiltLeft)
            {
                TiltLeft();
            }
            if (_ShouldTiltRight)
            {
                TiltRight();
            }
		}

		void PerformJump()
		{
            if (_Grounded)
            {
                _Rigidbody.AddForce(transform.up * _JumpForce, ForceMode.Impulse);
            }
        }

		void TiltLeft()
		{
            _Rigidbody.AddTorque(Vector3.forward * _TiltPower);
        }

        void MomentumLeft()
        {
            _Rigidbody.AddForceAtPosition (Vector3.left* _TiltPower, transform.position);
        }

        void TiltRight()
		{
            _Rigidbody.AddTorque(-Vector3.forward * _TiltPower);
        }

        void MomentumRight()
        {
            _Rigidbody.AddForceAtPosition (Vector3.right * _TiltPower, transform.position);
        }

        void OnCollisionStay(Collision c)
		{
            if (!c.gameObject.CompareTag("Ball"))
            {
                _Grounded = true;
            }
		}

		void OnCollisionExit(Collision c)
		{
			_Grounded = false;
            _Rigidbody.centerOfMass = _StartCenterOfMass;
		}

		void OnCollisionEnter(Collision c)
		{
			if (c.gameObject.CompareTag("Killzone"))
			{
				// Player died.

			}
		}

        void Respawn()
        {

        }
	}
}
