using UnityEngine;
using UnityEngine.SceneManagement;

namespace JWF
{
	public struct Scenes
	{
		public static Pair<string, int> MENU = new Pair<string, int>("JWFMenu", 0);
		public static Pair<string, int> CLASSIC_MAP = new Pair<string, int>("JWFClassicMap", 1);
		public static Pair<string, int> SMASH_MAP = new Pair<string, int>("JWFSmashMap", 2);
	}

	public class JWFSceneManager : MonoBehaviour
	{
		public static void LoadLevel(string levelName)
		{
			SceneManager.LoadScene( levelName );
			Time.timeScale = 1.0f;
		}
	}
}