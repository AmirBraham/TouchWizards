using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class P2_Controls : MonoBehaviour {

	public static int MoveSpeed =4;
	public static float Health = 1;
	Rigidbody2D rb2d;
	public GameObject ShootingPoint;
	public GameObject Bullet;
	public GameObject ShieldPrefab;
	public Transform ShieldPoint;
	public Slider HealthSlider;
	bool isHoldingRB;
	bool isHoldingLB;
	public static int NumberOfShields;
	public Button  Shield2_Button;
	List<float> BoosterXPos = new List<float>();


	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
			BoosterXPos.Add(-5f);
			BoosterXPos.Add(5f);
			NumberOfShields = 2;
			Health =1;
	}
	
	void Update () {
		Loop ();
		HealthStatus ();
		Movement ();
		ShieldButtonStatus ();
	}

	public void OnPointUpRightButton(){
			isHoldingRB = false;	 
		}
		public void onPointerDownRightButton () {
			isHoldingRB=true;
		}

	//Holding Left button
	public void OnPointUpLeftButton(){
			isHoldingLB = false;	 
		}
		public void onPointerDownLeftButton () {
			isHoldingLB=true;
		}

	public void Shield () {
		if(NumberOfShields <=3) {
		GameObject ShieldClone;
		ShieldClone = Instantiate(ShieldPrefab,ShieldPoint.position,Quaternion.identity) as GameObject;
		NumberOfShields++;
        Shield2_Button.interactable = true;
        }
        else
        {
            Shield2_Button.interactable = false;
        }
	}
	public void Shoot () {
		GameObject BulletClone;
		BulletClone = Instantiate(Bullet,ShootingPoint.transform.position,Quaternion.Euler(0,0,-90f)) as GameObject;
	}

	public void HealthStatus () {
			HealthSlider.value = Health;
			if(Health <=0) {
                        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
						GameManager.GameOver.SetActive(true);
						GetComponent<SpriteRenderer>().DOFade(0,2f);
						if(GameManager.P1_GameOverText!=null ){
							GameManager.P1_GameOverText.text = "You Win!";
						}
						if(GameManager.P2_GameOverText!=null) {
							GameManager.P2_GameOverText.text = "You Lose!";
						}
						//Time.timeScale=0;
						GameManager.GameOn.SetActive(false);

			}}

			public void Movement () {

		if(isHoldingRB) {
				rb2d.velocity = new Vector2(MoveSpeed,rb2d.velocity.y);
		         transform.localScale = new Vector3(1.5f,1.5f,1.5f);
				 GetComponent<Animator>().SetBool("Player2_isRunning",true);
		} else if (isHoldingLB) {
					rb2d.velocity = new Vector2(-MoveSpeed,rb2d.velocity.y);
					transform.localScale = new Vector3(-1.5f,1.5f,1.5f);
					GetComponent<Animator>().SetBool("Player2_isRunning",true);
		} else {
					rb2d.velocity = new Vector2(0,0);
					GetComponent<Animator>().SetBool("Player2_isRunning",false);
		}}
			public void Loop () {
			if(transform.position.x <= -3) {

			transform.position = new Vector2(-(transform.position.x+0.1f),transform.position.y);
		} else if (transform.position.x >= 3 ) {
			transform.position = new Vector2(-(transform.position.x - 0.1f),transform.position.y);
			
		}} 

		void ShieldButtonStatus () {
				if (NumberOfShields <= 3)
			{
				Shield2_Button.interactable = true;
			}
			else
			{
				Shield2_Button.interactable = false;
			}
		}
}
