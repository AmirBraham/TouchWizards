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
	public static  Text  GameOverText;
	public static Text P2_GameOverText;
	public static GameObject GameOn;


	void Start() {
			normalPlayerSpeed = 4;
			Time.timeScale=1;
			GameOn = GameObject.FindGameObjectWithTag("GameOn");
			GameOver = GameObject.FindGameObjectWithTag("GameOver");
			
			GameOver.SetActive(false);
			GameOn.SetActive(true);
			BoosterXPos.Add(-5f);
			BoosterXPos.Add(5f);
			InvokeRepeating("SpawnSpeedBoost",10,Random.Range(10,15));
		}


	void Update(){
		
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
