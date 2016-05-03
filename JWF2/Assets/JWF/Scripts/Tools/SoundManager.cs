using UnityEngine;

namespace JWF
{
	public class SoundManager : Singleton<SoundManager>
	{
		public AudioSource MusicSource;                 //Drag a reference to the audio source which will play the music.
		public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
		public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

		public override void Init()
		{
			base.Init();
			if ( MusicSource == null )
			{
				MusicSource = gameObject.AddComponent<AudioSource>();
			}
		}

		public void StopBGMusic()
		{
			if ( MusicSource != null )
				MusicSource.Stop();
		}

		public void ChangeBGMusic(AudioClip clip, float volume)
		{
			if ( MusicSource == null )
			{
				Init();
			}
			MusicSource.Stop();
			MusicSource.clip = clip;
			MusicSource.volume = volume;
			MusicSource.loop = true;
			MusicSource.Play();
		}

		//Used to play single sound clips.
		public void PlaySingle(AudioSource source, AudioClip clip, float volume)
		{
			//Set the clip of our efxSource audio source to the clip passed in as a parameter.
			source.clip = clip;
			source.volume = volume;

			//Play the clip.
			source.Play();
		}

		//RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
		public void RandomizeSfx(AudioSource source, float volume, params AudioClip[] clips)
		{
			//Generate a random number between 0 and the length of our array of clips passed in.
			int randomIndex = Random.Range(0, clips.Length);

			//Choose a random pitch to play back our clip at between our high and low pitch ranges.
			float randomPitch = Random.Range(lowPitchRange, highPitchRange);

			//Set the pitch of the audio source to the randomly chosen pitch.
			source.pitch = randomPitch;

			//Set the clip to the clip at our randomly chosen index.
			source.clip = clips[randomIndex];

			source.volume = volume;

			//Play the clip.
			source.Play();
		}

		protected override bool ShouldDestroyOnLoad()
		{
			return false;
		}
	}
}