using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
	public AudioSource source;
	public AudioClip clip;
	void Start ()
	{
		source.clip = clip;
		source.playOnAwake = false;
	}
	
	public void playSound ()
	{
		source.volume = PlayerPrefs.GetFloat ("SFXVol");
		source.PlayOneShot (clip);
	}
}
