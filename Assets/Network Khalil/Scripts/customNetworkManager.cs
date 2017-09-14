using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class customNetworkManager : NetworkManager {
	UIManager UImanager;
	void Start()
	{
		UImanager = gameObject.GetComponent<UIManager> ();
	}
	public void SelectedWan(){
		NetworkManager.singleton.StartMatchMaker();
		UImanager.showWAN ();
	}
	public void StartHost()
	{
		Setport ();
		NetworkManager.singleton.StartHost ();
		UImanager.showGameOn ();
		UImanager.showIPAddress ();
	}
	public void JoinGame()
	{
		string ip = GameObject.Find ("ipfield").transform.Find ("Text").GetComponent<Text> ().text;
		if (ip == "")
			return;
		setip (ip);
		Setport ();
		NetworkManager.singleton.StartClient ();
		UImanager.showGameOn ();
	}
	public void disconnect(){
		if(NetworkClient.active)
			NetworkClient.ShutdownAll ();
		if (GameObject.FindGameObjectWithTag ("localPlayer").GetComponent<WizardScriptNet> ().isServer && NetworkServer.active) {
			NetworkServer.Shutdown();
		}
		if (!NetworkClient.active && !NetworkServer.active && NetworkManager.singleton.matchMaker == null) {
			if (NetworkServer.active || NetworkClient.active) {
				if (GameObject.FindGameObjectWithTag ("localPlayer").GetComponent<WizardScriptNet> ().isServer) {
					NetworkManager.singleton.StopHost ();
				} else {
					NetworkManager.singleton.StopClient ();

				}
			}
		}
		Application.LoadLevel ("Start");
		//Network.Disconnect(0);
		//MasterServer.UnregisterHost();
	}
	//call this method to request a match to be created on the server
	public void CreateInternetMatch()
	{
		string matchName= GameObject.Find ("roomName").transform.Find ("Text").GetComponent<Text> ().text;
		if (matchName == "")
			return;
		NetworkManager.singleton.matchMaker.CreateMatch(matchName, 2, true, "", "", "", 0, 0, OnInternetMatchCreate);
	}

	public void FindInternetMatch()
	{
		string matchName= GameObject.Find ("roomName").transform.Find ("Text").GetComponent<Text> ().text;
		if (matchName == "")
			return;
		NetworkManager.singleton.matchMaker.ListMatches(0, 10, matchName, true, 0, 0, OnInternetMatchList);
	}

	private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			//Debug.Log("Create match succeeded");

			MatchInfo hostInfo = matchInfo;
			NetworkServer.Listen(hostInfo, 9000);

			NetworkManager.singleton.StartHost(hostInfo);
			UImanager.showGameOn ();
		}
		else
		{
			Debug.LogError("Create match failed");
		}
	}

	//call this method to find a match through the matchmaker

	//this method is called when a list of matches is returned
	private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (success)
		{
			if (matches.Count != 0)
			{
				//Debug.Log("A list of matches was returned");

				//join the last server (just in case there are two...)
				NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);

			}
			else
			{
				Debug.Log("No matches in requested room!");
			}
		}
		else
		{
			Debug.LogError("Couldn't connect to match maker");
		}
	}

	//this method is called when your request to join a match is returned
	private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			//Debug.Log("Able to join a match");

			MatchInfo hostInfo = matchInfo;
			NetworkManager.singleton.StartClient(hostInfo);
			UImanager.showGameOn ();
		}
		else
		{
			Debug.LogError("Join match failed");
		}
	}

	void Setport ()
	{
		NetworkManager.singleton.networkPort = 7777;
	}

	void setip(string Address)
	{
		NetworkManager.singleton.networkAddress = Address;
	}

}
