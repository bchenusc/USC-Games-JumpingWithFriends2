using UnityEngine;
using System.Collections;

public class TranslatorTreadmill2D : MonoBehaviour
{
	public Vector3 ResetPosition;
	public float EndXPosition = 0;
	public Vector3 Direction;
	public float SpeedRangeMin = 0.5f;
	public float SpeedRangeMax = 1.0f;
	public Space TranslateSpace = Space.World;

	private float _Speed = 1;

	void Start()
	{
		_Speed = Random.Range( SpeedRangeMin, SpeedRangeMax );
		Direction = Vector3.Normalize( Direction );
	}

	void Teleport()
	{
		_Speed = Random.Range( SpeedRangeMin, SpeedRangeMax );
		transform.position = ResetPosition;
	}

	void Update()
	{
		transform.Translate( Direction * Time.deltaTime * _Speed, TranslateSpace );
		float distance = Mathf.Abs((Vector2.right * EndXPosition).x - transform.position.x);
		if ( distance < 0.1f )
		{
			Teleport();
		}
	}
}
