using UnityEngine;

namespace JWF.ClassicMap
{
	public class JWFClassicMapMusicManager : MonoBehaviour
	{
		public AudioClip BackgroundMusic;
		private float _MusicVolume = 0.1f;

		// Use this for initialization
		void Start()
		{
			SoundManager.Get.ChangeBGMusic( BackgroundMusic, _MusicVolume );
		}
	}
}