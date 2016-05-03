using UnityEngine;
using System.Collections;

namespace JWF.SmashMap
{
	public class JWFSmashMapPlayerSounds : MonoBehaviour
	{
		public AudioClip[] JumpSounds;
		public AudioClip ThumpSound;
		public AudioClip FallingSound;

		private AudioSource FallingSoundAudioSource;
		private AudioSource JumpingSoundAudioSource;

		private float FallingSoundVolume = 0.1f;
		private float JumpingSoundVolume = 0.025f;
		private float ThumpSoundVolume = 0.1f;

		private bool _PlayFallSoundOnce = false;
		private bool _PlayThudSoundOnce = false;

		void Start()
		{
			FallingSoundAudioSource = gameObject.AddComponent<AudioSource>();
			JumpingSoundAudioSource = gameObject.AddComponent<AudioSource>();
		}

		public void PlayJumpSound()
		{
			if (!JumpingSoundAudioSource.isPlaying)
			{
				SoundManager.Get.RandomizeSfx( JumpingSoundAudioSource, JumpingSoundVolume, JumpSounds );
			}
		}

		public void PlayThudSound()
		{
			SoundManager.Get.PlaySingle( FallingSoundAudioSource, ThumpSound , ThumpSoundVolume );
		}

		public void PlayFallingSound()
		{
			SoundManager.Get.PlaySingle( FallingSoundAudioSource, FallingSound, FallingSoundVolume );
		}

		void Update()
		{
			if ( transform.position.y < 0 && !_PlayFallSoundOnce )
			{
				_PlayFallSoundOnce = true;
				PlayFallingSound();
			}

			if ( transform.position.y > 9 && !_PlayThudSoundOnce )
			{
				_PlayThudSoundOnce = true;
			}
		}

		public void ResetPlayFallingSound()
		{
			_PlayFallSoundOnce = false;
		}

		void OnCollisionEnter(Collision c)
		{
			if ( _PlayThudSoundOnce )
			{
				_PlayThudSoundOnce = false;
				PlayThudSound();
			}
		}
	}
}
