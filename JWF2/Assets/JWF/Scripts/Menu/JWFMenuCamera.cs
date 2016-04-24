using UnityEngine;
using System.Collections;

public class JWFMenuCamera : MonoBehaviour
{
	private float CameraMoveSpeed = 2.0f;

	public void MoveCameraTo(Vector3 position)
	{
		iTween.MoveTo( gameObject, position, CameraMoveSpeed );
	}
}
