using UnityEngine;
using System.Collections;

namespace JWF.ClassicMap
{
	public class JWFClassicMapInitSounds : MonoBehaviour
	{
		public AudioClip CountdownBeep;
		public AudioClip Whistle;

		private AudioSource _CountdownSource;
		private float _CountdownVolume = 0.5f;
		private float _WhistleVolume = 0.5f;

		void Start()
		{
			_CountdownSource = gameObject.AddComponent<AudioSource>();
		}

		public void PlayCountdown()
		{
			SoundManager.Get.PlaySingle( _CountdownSource, CountdownBeep, _CountdownVolume );
		}

		public void PlayWhistle()
		{
			SoundManager.Get.PlaySingle( _CountdownSource, Whistle, _WhistleVolume );
		}
	}
}
