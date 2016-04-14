using UnityEngine;
using UnityEngine.SceneManagement;

namespace JWF
{
	public class JWFSceneManager : MonoBehaviour
	{
		public static void LoadLevel(string levelName)
		{
			SceneManager.LoadScene( levelName );
		}
	}
}