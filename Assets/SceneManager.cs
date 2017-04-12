using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneManager : MonoBehaviour {
	public GameObject CREDITS;
	public GameObject PLAY;

	public GameObject CreditsText;

	public GameObject ReturnUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameScene () {
		Application.LoadLevel("Main");
	}

	public void Credits () {
		PLAY.transform.DOMoveX(3000,2f);
		CREDITS.transform.DOMoveX(-1000,2f);
		CreditsText.transform.DOLocalMoveX(0,3f);
	}

	public void Return () {
		PLAY.transform.DOMoveX(550,2f);
		CREDITS.transform.DOMoveX(550,2f);
		CreditsText.transform.DOLocalMoveX(2001f,2f);


	}
}
