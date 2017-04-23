using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneManager : MonoBehaviour {
	public GameObject CREDITS;
	public GameObject PLAY;

	public GameObject CreditsText;

	public GameObject ReturnUI;

	public void GameScene () {
		Application.LoadLevel("Main");
	}

	public void Credits () {
		PLAY.transform.DOMoveX(10,5f);
		CREDITS.transform.DOMoveX(-10,5f);
		CreditsText.transform.DOLocalMoveX(0,2f);
	}

	public void Return () {
		PLAY.transform.DOMoveX(0,2f);
		CREDITS.transform.DOMoveX(0,2f);
		CreditsText.transform.DOLocalMoveX(1000f,1f);


	}
}
