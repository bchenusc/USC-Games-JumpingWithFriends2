using UnityEngine;
using System.Collections;

public class JWFPlayerRealign : MonoBehaviour
{

	/*float mRealignForce = 1600; // up force
	Transform mCenterOfMass;
	Rigidbody rb;

	void Start()
	{
		// Moves the center of mass to the proper location.
		mCenterOfMass = transform.FindChild( "CenterOfMass" );
		rb = gameObject.GetComponent<Rigidbody>();
		rb.centerOfMass = mCenterOfMass.position - transform.position;

	}

	void FixedUpdate()
	{
		if ( IsGrounded )
		{
			rb.AddTorque( Mathf.Sign( transform.up.x ) * OppositeDot( Vector3.up, transform.up ) * Vector3.forward * mRealignForce );
		}
	}

	float OppositeDot(Vector3 v1, Vector3 v2)
	{
		return 1f - Mathf.Abs( Vector3.Dot( Vector3.Normalize( v1 ), Vector3.Normalize( v2 ) ) );
	}

	void OnCollisionEnter(Collision c)
	{
		if ( c.gameObject.CompareTag( "Deadzone" ) )
		{
			PlayThudSound = true;
			rigidbody.isKinematic = true;
			transform.position = transform.GetComponent<PlayerGameState>().mSoccerGamePosition + Vector3.up * 15;
			SingletonObject.Get.getTimer().Add( gameObject.GetInstanceID() + "respawning", RespawnMe, 2.0f, false );
			return;
		}
		else
		{
			if ( PlayThudSound )
			{
				PlayThudSound = false;

				Vector3 effectLocation = transform.position;
				effectLocation.Set( transform.position.x + 0.5f, transform.position.y - 0.5f, transform.position.z );
				GameObject clone = Instantiate(Resources.Load("Effects/Thud", typeof(GameObject)),
												   effectLocation ,
												   Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
				Destroy( clone, 1.0f );
				SingletonObject.Get.getSoundManager().play( "Audio/whoop_big_thud", false, 2 );

			}
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if ( c.gameObject.CompareTag( "Thudzone" ) )
		{
			PlayThudSound = true;
		}
	}

	void RespawnMe()
	{
		if ( this == null ) return;
		rigidbody.isKinematic = false;
		rigidbody.AddForce( Vector3.down );
	}

	void OnCollisionStay(Collision c)
	{
		if ( !c.gameObject.CompareTag( "Ball" ) )
			mIsGrounded = true;
	}

	void OnCollisionExit(Collision c)
	{
		mIsGrounded = false;
		rigidbody.centerOfMass = initCenterOfMass;
		if ( CollisionExit != null )
		{
			CollisionExit();
		}
		if ( c.gameObject.CompareTag( "Ball" ) )
		{
			if ( mPlayerNumber == 1 || mPlayerNumber == 3 )
			{
				SingletonObject.Get.getGameState().GET_MODE_AS_SOCCER.UpdateLastTouch( 2 );
			}
			else
			{
				SingletonObject.Get.getGameState().GET_MODE_AS_SOCCER.UpdateLastTouch( 1 );
			}

		}
	}
*/	
}
