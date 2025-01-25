using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioSource musicSource;
	public AudioClip songToPlay;
	private static AudioManager instance = null;
	
	public static AudioManager Instance
	{
		get {return instance;}
	}
	
	// Use this for initialization
	void Awake () 
	{
		if(instance != null && instance != this)
		{
			Destroy (this.gameObject);
			//Destroys any pre-existing AudioManagers to prevent audio distortion and general nosie 
		}
		else
		{
			instance = this;
		}
		
		DontDestroyOnLoad(this.gameObject);
		musicSource = AudioManager.Instance.gameObject.GetComponent<AudioSource>();
		//muscSource is now set and available to other audio-based scripts
		
		musicSource.Play();
	}
	
	public void Pause()
	{
		musicSource.Pause();
	}
	
	public void ChooseSong(AudioClip song)
	{
		musicSource.clip = song;
		musicSource.Play();
	}
}
