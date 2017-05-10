using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	float timer = 0.0f;
    int normalPlayerSpeed = 4;
	public GameObject SpeedBoostPrefab;
    List<float> BoosterXPos = new List<float>();
	public static  GameObject GameOver;
	public static  Text  P1_GameOverText;
	public static Text P2_GameOverText;
	public static GameObject GameOn;
	public static  float timeLeft = 10.0f;

	public static bool TimeUp;
     


	void Start() {
			normalPlayerSpeed = 4;
			Time.timeScale=1;
			GameOn = GameObject.FindGameObjectWithTag("GameOn");
			GameOver = GameObject.FindGameObjectWithTag("GameOver");
			P1_GameOverText = GameObject.FindGameObjectWithTag("P1_GameOverText").GetComponent<Text>();
			GameOver.SetActive(false);
			GameOn.SetActive(true);
			BoosterXPos.Add(-5f);
			BoosterXPos.Add(5f);
			InvokeRepeating("SpawnSpeedBoost",10,Random.Range(10,15));
			TimeUp = false;
		}


	void Update(){
		Debug.Log(timeLeft);
		if(timeLeft > 0) {
			timeLeft -= Time.deltaTime;

		}	else {
				TimeUp = true;
				GameOn.SetActive(false);
		}
			if(P1_Controls.MoveSpeed!= P2_Controls.MoveSpeed) {
				timer+=Time.deltaTime;
			}
			if(timer >=10f) {
						P1_Controls.MoveSpeed = P2_Controls.MoveSpeed = normalPlayerSpeed;
					timer = 0;
			}
		
		
		}
	
	public void RestartLevel () {
		Application.LoadLevel(Application.loadedLevelName);}
	
		void SpawnSpeedBoost () {
			GameObject SpeedBoostClone;

			SpeedBoostClone = Instantiate(SpeedBoostPrefab, new Vector3 (BoosterXPos[Random.Range(0,BoosterXPos.Count)],Random.Range(0.5f,-0.65f),0),Quaternion.identity) as GameObject;
		}


}
