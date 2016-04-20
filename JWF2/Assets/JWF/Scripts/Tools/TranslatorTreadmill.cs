using UnityEngine;
using System.Collections;

public class TranslatorTreadmill : MonoBehaviour
{
	public Vector3 StartPosition;
	public Vector3 EndPosition;
	public float SpeedRangeMin = 0.5f;
	public float SpeedRangeMax = 1.0f;
	public Space TranslateSpace = Space.World;

	private Vector3 _Target;
	private float _Speed;

	void Start()
	{
		_Target = EndPosition;
		_Speed = Random.Range( SpeedRangeMin, SpeedRangeMax );
	}

	void Teleport()
	{
		_Speed = Random.Range( SpeedRangeMin, SpeedRangeMax );
		transform.position = StartPosition;
	}

	void Update()
	{
		transform.Translate( Vector3.Normalize( _Target - transform.position ) * Time.deltaTime * _Speed, TranslateSpace );
		if ( Vector3.SqrMagnitude( transform.position - _Target ) < 0.1f )
		{
			Teleport();
		}
	}
}
