using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneManager : MonoBehaviour {
	public GameObject CREDITS;
	public GameObject PLAY;
	public GameObject CreditsText;

		public void SoloScene () {
			Application.LoadLevel("SinglePlayer");
		}

		public void MultiScene () {
			Application.LoadLevel("LocalMultiplayer");
		}

		public void Return () {
			PLAY.transform.DOMoveX(0,2f);
			CREDITS.transform.DOMoveX(0,2f);
			CreditsText.transform.DOLocalMoveX(1000f,1f);


		}
}
