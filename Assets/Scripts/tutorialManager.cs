using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialManager : MonoBehaviour
{
	public GameObject NextButton;
	public GameObject BeforeButton;

	public GameObject DefaultContent;
	public GameObject LMultiContent;

	void Start ()
	{
		BeforeButton.SetActive (false);
		if (Application.loadedLevelName == "SinglePlayer") {
			if (PlayerPrefs.HasKey ("SoloFirstTime")) {
				Destroy (gameObject);
			} else {
				PlayerPrefs.SetInt ("SoloFirstTime", 1);
			}
			BeforeButton.SetActive (false);
			NextButton.SetActive (false);
		}
		if (Application.loadedLevelName == "LocalMultiplayer") {
			if (PlayerPrefs.HasKey ("LMultiFirstTime")) {
				Destroy (gameObject);
			} else {
				PlayerPrefs.SetInt ("LMultiFirstTime", 1);
			}
		}
	}

	public void Close ()
	{
		Time.timeScale = 1;	
		Destroy (gameObject);
	}

	void Update ()
	{
		Time.timeScale = 0;	
	}

	public void Next ()
	{
		BeforeButton.SetActive (true);
		NextButton.SetActive (false);

		if (Application.loadedLevelName == "LocalMultiplayer" || true) {
			DefaultContent.SetActive (false);
			LMultiContent.SetActive (true);

		}
	}

	public void Before ()
	{
		BeforeButton.SetActive (false);
		NextButton.SetActive (true);
		DefaultContent.SetActive (true);
		LMultiContent.SetActive (false);
		
	}
}
