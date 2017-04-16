using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	float timer = 0.0f;
	
	//Player 1 
		GameObject Player1;
		Rigidbody2D P1_rb2d;
		public Slider P1_HealthSlider;
		public  static float P1_Health = 100;
		public GameObject P1_ShootingPoint;
		public GameObject P1_Bullet;
		public GameObject P1_ShieldPrefab;
		public Transform P1_ShieldPoint;
		bool isP1HoldingRB;
		bool isP1HoldingLB;
		float P1PosX;
		public static int P1_NumberOfShields;

		public static float P1_MoveSpeed;


	//Player 2
		GameObject Player2;		
		Rigidbody2D P2_rb2d;
		public static float P2_Health = 100;
		public GameObject P2_ShootingPoint;
		public GameObject P2_Bullet;
		public GameObject P2_ShieldPrefab;
		public Transform P2_ShieldPoint;
		public Slider P2_HealthSlider;

		bool isP2HoldingRB;
		bool isP2HoldingLB;
		public static int P2_NumberOfShields;

		public static float P2_MoveSpeed ;

	
	//UI Elements
	//Boosts
		public GameObject SpeedBoostPrefab;
         List<float> BoosterXPos = new List<float>();

		public GameObject GameOver;
		public Text P1_GameOverText;
		public Text P2_GameOverText;
		public GameObject GameOn;




void Start() {
		P1_NumberOfShields = 0;
		P2_NumberOfShields = 0;
		Player1 = GameObject.FindGameObjectWithTag("Player_1");
		Player2 = GameObject.FindGameObjectWithTag("Player_2");
		P1_rb2d = Player1.GetComponent<Rigidbody2D>();	
		P2_rb2d = Player2.GetComponent<Rigidbody2D>();
		P1_Health = 1;
		P2_Health = 1;
		Time.timeScale=1;
		GameOn.SetActive(true);
		P1_MoveSpeed = P2_MoveSpeed = 3;
		BoosterXPos.Add(-5f);
		BoosterXPos.Add(5f);
		InvokeRepeating("SpawnSpeedBoost",10,Random.Range(5,7));



	}

//P1 CONTROLS
	public void P1_OnPointUpRightButton(){
			isP1HoldingRB = false;	 
		}
		public void P1_onPointerDownRightButton () {
			isP1HoldingRB=true;
		}

	public void P1_OnPointUpLeftButton(){
				isP1HoldingLB = false;	 
			}
	public void P1_onPointerDownLeftButton () {
				isP1HoldingLB=true;
			}
	public void P1_Shoot () {
		GameObject P1_BulletClone;
		P1_BulletClone = Instantiate(P1_Bullet,P1_ShootingPoint.transform.position,Quaternion.Euler(0,0,90f)) as GameObject;
		}


	public void P1_Shield () {
		if(P1_NumberOfShields < 2) {
		GameObject P1_ShieldClone;
		P1_ShieldClone = Instantiate(P1_ShieldPrefab,P1_ShieldPoint.position,Quaternion.identity) as GameObject;
		P1_NumberOfShields++;
			}}
//P2 CONTROLS
	public void P2_OnPointUpRightButton(){
			isP2HoldingRB = false;	 
		}
		public void P2_onPointerDownRightButton () {
			isP2HoldingRB=true;
		}

	//Holding Left button
	public void P2_OnPointUpLeftButton(){
			isP2HoldingLB = false;	 
		}
		public void P2_onPointerDownLeftButton () {
			isP2HoldingLB=true;
		}

	public void P2_Shield () {
		if(P2_NumberOfShields <2) {
		GameObject P2_ShieldClone;
		P2_ShieldClone = Instantiate(P2_ShieldPrefab,P2_ShieldPoint.position,Quaternion.identity) as GameObject;
		P2_NumberOfShields++;
		}
	}
	public void P2_Shoot () {
		GameObject P2_BulletClone;
		P2_BulletClone = Instantiate(P2_Bullet,P2_ShootingPoint.transform.position,Quaternion.Euler(0,0,-90f)) as GameObject;
	}
//
void Update()
{
	Debug.Log(timer);
	Player1Loop();
	Player2Loop();
	Player1HealthStatus ();
	Player2HealthStatus ();
	Player1Controls ();
	Player2Controls ();
	if(P1_MoveSpeed != P2_MoveSpeed) {
		timer+=Time.deltaTime;
	}
	if(timer >=10f) {
				P1_MoveSpeed = P2_MoveSpeed = 3;

	}
}
	public void Player1HealthStatus () {
		P1_HealthSlider.value = P1_Health;
		if(P1_Health <=0) {
			GameOver.SetActive(true);
			Player1.GetComponent<SpriteRenderer>().DOFade(0,2f);
			P2_GameOverText.text = "You Win!";
			P1_GameOverText.text = "You Lose!";
			GameOn.SetActive(false);
			//Time.timeScale=0;
		}}


	public void Player2HealthStatus () {
			P2_HealthSlider.value = P2_Health;
			if(P2_Health <=0) {
						GameOver.SetActive(true);
						Player2.GetComponent<SpriteRenderer>().DOFade(0,2f);
						P1_GameOverText.text = "You Win!";
						P2_GameOverText.text = "You Lose!";

						//Time.timeScale=0;
						GameOn.SetActive(false);

			}}



	public void Player1Loop () {
			if(Player1.transform.position.x <= -3) {
				Player1.transform.position = new Vector2(-(Player1.transform.position.x+0.1f),Player1.transform.position.y);
			} else if (Player1.transform.position.x >= 3 ) {
				Player1.transform.position = new Vector2(-(Player1.transform.position.x - 0.1f),Player1.transform.position.y);
		
		}}

	public void Player2Loop () {
			if(Player2.transform.position.x <= -3) {

			Player2.transform.position = new Vector2(-(Player2.transform.position.x+0.1f),Player2.transform.position.y);
		} else if (Player2.transform.position.x >= 3 ) {
			Player2.transform.position = new Vector2(-(Player2.transform.position.x - 0.1f),Player2.transform.position.y);
			
		}} 
	public void RestartLevel () {
		Application.LoadLevel("Main");}
	public void Player1Controls () {

		if(isP1HoldingRB) {
					P1_rb2d.velocity = new Vector2(P1_MoveSpeed,P1_rb2d.velocity.y);
					Player1.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
					Player1.GetComponent<Animator>().SetBool("Player1_isRunning",true);
		} else if(isP1HoldingLB) {
					P1_rb2d.velocity = new Vector2(-P1_MoveSpeed,P1_rb2d.velocity.y);
					Player1.transform.localScale = new Vector3(-1.5f,1.5f,1.5f);
					Player1.GetComponent<Animator>().SetBool("Player1_isRunning",true);
		} else {
					P1_rb2d.velocity = new Vector2(0,0);
					Player1.GetComponent<Animator>().SetBool("Player1_isRunning",false);
		}}

	public void Player2Controls () {

		if(isP2HoldingRB) {
				 P2_rb2d.velocity = new Vector2(P2_MoveSpeed,P2_rb2d.velocity.y);
		         Player2.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
				 Player2.GetComponent<Animator>().SetBool("Player2_isRunning",true);
		} else if (isP2HoldingLB) {
					P2_rb2d.velocity = new Vector2(-P2_MoveSpeed,P2_rb2d.velocity.y);
					Player2.transform.localScale = new Vector3(-1.5f,1.5f,1.5f);
					Player2.GetComponent<Animator>().SetBool("Player2_isRunning",true);
		} else {
					P2_rb2d.velocity = new Vector2(0,0);
					Player2.GetComponent<Animator>().SetBool("Player2_isRunning",false);
		}}


		void SpawnSpeedBoost () {
			GameObject SpeedBoostClone;

			SpeedBoostClone = Instantiate(SpeedBoostPrefab, new Vector3 (BoosterXPos[Random.Range(0,BoosterXPos.Count)],Random.Range(0.5f,-0.65f),0),Quaternion.identity) as GameObject;
		}


}
