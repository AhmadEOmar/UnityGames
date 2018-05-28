using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;
		
	
	void Awake()
	{
		if(instance != null)
		{
			Destroy (gameObject);
			print ("Duplicate muic player self-destructing");
			
		}
		else 
		{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

}
