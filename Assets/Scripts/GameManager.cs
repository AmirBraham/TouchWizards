using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	float timer = 0.0f;
    int normalPlayerSpeed = 4;
	
	//Player 1 
		GameObject Player1;
		Rigidbody2D rb2d;
		public  static Slider P1_HealthSlider;
		public  static float Health = 100;
		public GameObject ShootingPoint;
		public GameObject Bullet;
		public GameObject ShieldPrefab;
		public Transform ShieldPoint;
		
		float P1PosX;
		public static int NumberOfShields;

		public static float MoveSpeed;


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

		public static  GameObject GameOver;

		public static  Text  GameOverText;
		public Text P2_GameOverText;
        public Button Shield1_Button;
        public Button Shield2_Button;
		public static GameObject GameOn;




void Start() {
        normalPlayerSpeed = 4;
		NumberOfShields = 0;
		P2_NumberOfShields = 0;
		Player1 = GameObject.FindGameObjectWithTag("Player_1");
		Player2 = GameObject.FindGameObjectWithTag("Player_2");
		rb2d = Player1.GetComponent<Rigidbody2D>();	
		P2_rb2d = Player2.GetComponent<Rigidbody2D>();
		Health = 1;
		P2_Health = 1;
		Time.timeScale=1;
		GameOn.SetActive(true);
		MoveSpeed = P2_MoveSpeed = normalPlayerSpeed;
		BoosterXPos.Add(-5f);
		BoosterXPos.Add(5f);
		InvokeRepeating("SpawnSpeedBoost",10,Random.Range(10,15));



	}

//P1 CONTROLS
	
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
        Shield2_Button.interactable = true;
        }
        else
        {
            Shield2_Button.interactable = false;
        }
	}
	public void P2_Shoot () {
		GameObject P2_BulletClone;
		P2_BulletClone = Instantiate(P2_Bullet,P2_ShootingPoint.transform.position,Quaternion.Euler(0,0,-90f)) as GameObject;
	}
//
void Update()
{
	Player2Loop();
	Player2HealthStatus ();
	Player2Controls ();
	if(MoveSpeed != P2_MoveSpeed) {
		timer+=Time.deltaTime;
	}
	if(timer >=10f) {
				MoveSpeed = P2_MoveSpeed = normalPlayerSpeed;
            timer = 0;
	}
        if (NumberOfShields<2)
        {
            Shield1_Button.interactable = true;
        }
        else
        {
            Shield1_Button.interactable = false;
        }
        if (P2_NumberOfShields < 2)
        {
            Shield2_Button.interactable = true;
        }
        else
        {
            Shield2_Button.interactable = false;
        }
    }
	


	public void Player2HealthStatus () {
			P2_HealthSlider.value = P2_Health;
			if(P2_Health <=0) {
						GameOver.SetActive(true);
						Player2.GetComponent<SpriteRenderer>().DOFade(0,2f);
						GameOverText.text = "You Win!";
						P2_GameOverText.text = "You Lose!";

						//Time.timeScale=0;
						GameOn.SetActive(false);

			}}




	public void Player2Loop () {
			if(Player2.transform.position.x <= -3) {

			Player2.transform.position = new Vector2(-(Player2.transform.position.x+0.1f),Player2.transform.position.y);
		} else if (Player2.transform.position.x >= 3 ) {
			Player2.transform.position = new Vector2(-(Player2.transform.position.x - 0.1f),Player2.transform.position.y);
			
		}} 
	public void RestartLevel () {
		Application.LoadLevel("Main");}
	

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
