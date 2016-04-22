using UnityEngine;
using System.Collections;

namespace JWF
{
	public class Rotator : MonoBehaviour
	{
		public Space CoordinateSpace = Space.World;
		public Vector3 Axis = Vector3.up;
		public float Speed = 1.0f;

		void Update()
		{
			transform.Rotate( Axis * Time.deltaTime * Speed, CoordinateSpace );
		}
	}
}
