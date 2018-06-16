using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public GameObject LAN;
	public GameObject WAN;
	public GameObject lanwanButton;
	public GameObject GameOver;
	public GameObject GameOn;
	public GameObject MatchMakingUI;
	public GameObject IPAddress;
	public GameObject SettingsPanel;
	public Button shieldButton;

	public Slider MusicSlider;

	// Use this for initialization
	void Start () {
		showLanWanButtons ();
		MusicSlider.value = PlayerPrefs.GetFloat ("MusicVol");
	}

	// Update is called once per frame
	void Update () {
	}
	public void showSettings(){
		SettingsPanel.SetActive (true);
	}
	public void hideSettings(){
		SettingsPanel.SetActive (false);
	}
	public void changeMusicVolume ()
	{
		PlayerPrefs.SetFloat ("MusicVol", MusicSlider.value);
	}
	public void showIPAddress(){
		GameObject ip = Instantiate(IPAddress,IPAddress.transform);
		ip.transform.SetParent(GameObject.Find("UI").transform.Find("GameOn").transform,false);
		ip.GetComponent<Text> ().text ="Your IP Address: "+ Network.player.ipAddress.ToString();
	}
	public void showLAN(){
		MatchMakingUI.SetActive (true);
		LAN.SetActive (true);
		WAN.SetActive (false);
		lanwanButton.SetActive (false);
		GameOn.SetActive (false);
		GameOver.SetActive (false);
	}
	public void showWAN(){
		MatchMakingUI.SetActive (true);
		WAN.SetActive (true);
		LAN.SetActive (false);
		lanwanButton.SetActive (false);
		GameOn.SetActive (false);
		GameOver.SetActive (false);
	}
	public void showGameOver(){
		MatchMakingUI.SetActive (false);
		GameOn.SetActive (false);
		GameOver.SetActive (true);
	}
	public void showGameOn(){
		MatchMakingUI.SetActive (false);
		GameOver.SetActive (false);
		GameOn.SetActive (true);
	}
	public void showLanWanButtons(){
		MatchMakingUI.SetActive (true);
		lanwanButton.SetActive (true);
		LAN.SetActive (false);
		WAN.SetActive (false);
		GameOn.SetActive (false);
		GameOver.SetActive (false);
	}
	public void activateShieldButton(){
		shieldButton.interactable = true;
	}
	public void deactivateShieldButton(){
		shieldButton.interactable = false;
	}


}
