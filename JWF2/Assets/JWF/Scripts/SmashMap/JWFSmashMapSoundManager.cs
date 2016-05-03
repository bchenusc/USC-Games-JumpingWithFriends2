using UnityEngine;

namespace JWF.SmashMap
{
	public class JWFSmashMapSoundManager : MonoBehaviour
	{
		public AudioClip BackgroundMusic;
		public AudioClip CountdownBeep;
		public AudioClip Whistle;

		private AudioSource _CountdownSource;

		private float _BackgroundVolume = 0.1f;
		private float _CountdownVolume = 0.5f;
		private float _WhistleVolume = 0.5f;

		void Start()
		{
			SoundManager.Get.Init();
			_CountdownSource = gameObject.AddComponent<AudioSource>();
			SoundManager.Get.ChangeBGMusic(BackgroundMusic, _BackgroundVolume);
		}

		public void PlayCountdown()
		{
			SoundManager.Get.PlaySingle(_CountdownSource, CountdownBeep, _CountdownVolume);
		}

		public void PlayWhistle()
		{
			SoundManager.Get.PlaySingle(_CountdownSource, Whistle, _WhistleVolume);
		}
	}
}
