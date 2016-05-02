using UnityEngine;
using UnityEngine.SceneManagement;

namespace JWF
{
	public struct Scenes
	{
		public static string MENU = "JWFMenu";
		public static string CLASSIC_MAP = "JWFClassicMap";
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