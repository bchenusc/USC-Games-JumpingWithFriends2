using UnityEngine;

public class TranslatorPingPong : MonoBehaviour
{
	public Vector3 A;
	public Vector3 B;
	public float SpeedRangeMin = 0.5f;
	public float SpeedRangeMax = 1.0f;
	public Space TranslateSpace = Space.World;

	private Vector3 _Target;
	private float _Speed;

	// Always go to position A first and then go to position B.
	void Start()
	{
		_Target = A;
		_Speed = Random.Range( SpeedRangeMin, SpeedRangeMax );
	}

	void Reverse()
	{
		_Speed = Random.Range( SpeedRangeMin, SpeedRangeMax );
		if ( _Target == A ) _Target = B;
		else if ( _Target == B ) _Target = A;
	}

	void Update()
	{
		transform.Translate( Vector3.Normalize( _Target - transform.position ) * Time.deltaTime * _Speed, TranslateSpace );
		if (Vector3.SqrMagnitude(transform.position - _Target) < 0.1f)
		{
			Reverse();
		}
	}
}
