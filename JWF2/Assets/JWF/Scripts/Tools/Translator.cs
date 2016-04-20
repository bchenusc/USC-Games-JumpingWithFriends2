using UnityEngine;

public class Translator : MonoBehaviour
{
	// Set in inspector.
	public float SpeedRangeMin = 0.5f;
	public float SpeedRangeMax = 1.0f;
	public Vector3 Direction = Vector3.right;
	public Space TranslateSpace = Space.World;

	private float _Speed = 0.0f;

	void Start()
	{
		_Speed = Random.Range( SpeedRangeMin, SpeedRangeMax );
	}

	public void RerollSpeed(float rangeMin, float rangeMax)
	{
		_Speed = Random.Range( rangeMin, rangeMax );
	}

	void Update()
	{
		transform.Translate( Direction * Time.deltaTime * _Speed, TranslateSpace );
	}
}
