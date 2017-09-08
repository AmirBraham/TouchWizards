using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneManager : MonoBehaviour
{
	public GameObject sorryPanel;
	public GameObject MainButtons;
	public GameObject MultiChooseButtons;
	public GameObject OptionsMenu;
	public Slider MusicSlider;

	void Start ()
	{
		sorryPanel.SetActive (false);
		Time.timeScale = 1;
		MusicSlider.value = PlayerPrefs.GetFloat ("MusicVol");
	}

	public void ShowSorry ()
	{
		sorryPanel.SetActive (true);
	}

	public void HideSorry ()
	{
		sorryPanel.SetActive (false);
	}

	public void bringMultioptions ()
	{
		MultiChooseButtons.transform.DOMoveX (0, 0.5f);
		MainButtons.transform.DOMoveX (-10, 0.5f);
	}

	public void TakeMultioptions ()
	{
		MultiChooseButtons.transform.DOMoveX (10, 0.5f);
		MainButtons.transform.DOMoveX (0, 0.5f);
	}

	public void changeMusicVolume ()
	{
		PlayerPrefs.SetFloat ("MusicVol", MusicSlider.value);
	}

	public void ShowOptions ()
	{
		OptionsMenu.SetActive (true);
	}

	public void HideOptions ()
	{
		OptionsMenu.SetActive (false);
	}

	public void SoloScene ()
	{
		Application.LoadLevel ("SinglePlayer");
	}

	public void MultiScene ()
	{
		Application.LoadLevel ("LocalMultiplayer");
	}
}
