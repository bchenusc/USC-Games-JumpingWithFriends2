using UnityEngine;
using System.Collections;


namespace JWF.ClassicMap
{
	public class JWFClassicMapCameraSounds : MonoBehaviour
	{
		public AudioClip CameraShutter;
		private AudioSource _CameraShutterSource;
		private float _CameraShutterVolume = 0.1f;

		void Start()
		{
			if ( _CameraShutterSource == null )
			{
				_CameraShutterSource = gameObject.AddComponent<AudioSource>();
				_CameraShutterSource.clip = CameraShutter;
			}
		}

		void OnEnable()
		{
			Start();
			SoundManager.Get.PlaySingle( _CameraShutterSource, CameraShutter, _CameraShutterVolume );
		}
	}
}