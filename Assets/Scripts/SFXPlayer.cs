using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
	public AudioSource source;
	public AudioClip clip;
	// Use this for initialization
	void Start ()
	{
		source.clip = clip;
		source.playOnAwake = false;
	}
	
	// Update is called once per frame
	public void playSound ()
	{
		source.volume = PlayerPrefs.GetFloat ("SFXVol");
		source.PlayOneShot (clip);
	}
}
