using UnityEngine;

/*
 * Lazily-instanted, doubly locked, templated version of a Singleton class pattern.
 * 
 * Notes:
 * 1. This is adapted from a singleton tutorial I found online somewhere (I'll provide a link if I can find it again).
 * 
 * @ Matthew Pohlmann
 * 
*/

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	
	private static T _instance;
	private static object _lock = new object();
	
	protected static bool ApplicationIsQuitting = false;

	public void Create()
	{
		// Literally Does nothing.
	}
	
	public static T Get
	{
		get
		{
			if (ApplicationIsQuitting) {
				Debug.LogWarning("[Singleton (" + typeof(T) + ")] Instance already destroyed on application quit. Won't create again - returning null.");
				return null;
			}
			
			lock(_lock) {
				
				if (_instance == null) {
					
					_instance = (T) FindObjectOfType(typeof(T));
					
					if (FindObjectsOfType(typeof(T)).Length > 1 ) {
						Debug.LogError("[Singleton (" + typeof(T) + ")] Something went really wrong - there should never be more than 1 singleton! Reopening the scene might fix it.");
						return _instance;
					}
					
					if (_instance == null) {
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<T>();
						singleton.name = "[Singleton (" + typeof(T).ToString() + ")]";
						
						if (!(_instance as Singleton<T>).ShouldDestroyOnLoad()) {
							DontDestroyOnLoad(singleton);
							Debug.Log("[Singleton (" + typeof(T) + ")] New instance created with DontDestroyOnLoad().");
						} else {
							Debug.Log("[Singleton (" + typeof(T) + ")] New instance created.");
						}
					} else {
						Debug.Log("[Singleton] Using the already created instance: " + _instance.gameObject.name);
					}
				}
				
				return _instance;
			}
		}
	}

	protected abstract bool ShouldDestroyOnLoad();
	
	void OnApplicationQuit() {
		ApplicationIsQuitting = true;
	}
}