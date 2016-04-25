using UnityEngine;
using System.Collections;

namespace JWF.Menu
{
	public class JWFMenuMusic : MonoBehaviour
	{
		public AudioClip MenuMusic;
		private float _MusicVolume = 0.1f;

		void Start()
		{
			SoundManager.Get.Init();
			SoundManager.Get.ChangeBGMusic( MenuMusic, _MusicVolume );
		}
	}
}
