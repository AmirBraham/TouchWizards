using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AI_Controls : MonoBehaviour {

	public static int MoveSpeed =4;
	public static float Health = 1;
	Rigidbody2D rb2d;
	public GameObject ShootingPoint;
	public GameObject Bullet;
	public GameObject ShieldPrefab;
	public Transform ShieldPoint;
	bool isHoldingRB;
	bool isHoldingLB;
	public static int NumberOfShields;
	List<float> BoosterXPos = new List<float>();

	public bool DetectedEnemyBullet;

	public LayerMask P1_Bullet;

	public float DetectionRaduis;

			float random = Random.Range(0,110);
			float randomDuration = Random.Range(4,30);


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
		DetectedEnemyBullet = Physics2D.OverlapCircle(transform.position,DetectionRaduis,P1_Bullet);
		Debug.Log(DetectedEnemyBullet);
		if(DetectedEnemyBullet) {
		Shield();
		}

	}
		 void OnDrawGizmosSelected()
     {

		  Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRaduis);
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
        }
        else
        {
        }
	}
	public void Shoot () {
		GameObject BulletClone;
		BulletClone = Instantiate(Bullet,ShootingPoint.transform.position,Quaternion.Euler(0,0,-90f)) as GameObject;
	}

	public void HealthStatus () {
			if(Health <=0) {
                        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
						GameManager.GameOver.SetActive(true);
						GetComponent<SpriteRenderer>().DOFade(0,2f);
						if(GameManager.GameOverText!=null ){
							GameManager.GameOverText.text = "You Win!";
						}
						if(GameManager.P2_GameOverText!=null) {
							GameManager.P2_GameOverText.text = "You Lose!";
						}
						//Time.timeScale=0;
						GameManager.GameOn.SetActive(false);

			}}

			public void Movement () {
				Debug.Log(random);
		if(random > 30 && random < 60) {
				rb2d.velocity = new Vector2(MoveSpeed,rb2d.velocity.y);
		         transform.localScale = new Vector3(1.5f,1.5f,1.5f);
				 GetComponent<Animator>().SetBool("Player2_isRunning",true);
				 randomDuration++;
				 if(randomDuration >=30) {
					 random = Random.Range(0,110);
					 randomDuration = Random.Range(4,30);
				 }
		} else if (random > 60 && random < 100) {
					rb2d.velocity = new Vector2(-MoveSpeed,rb2d.velocity.y);
					transform.localScale = new Vector3(-1.5f,1.5f,1.5f);
					GetComponent<Animator>().SetBool("Player2_isRunning",true);
					 randomDuration++;
				 if(randomDuration >=30) {
					 random = Random.Range(0,110);
					 randomDuration = Random.Range(4,30);
				 }
		} else {
					rb2d.velocity = new Vector2(0,0);
					GetComponent<Animator>().SetBool("Player2_isRunning",false);
										 randomDuration++;
				 if(randomDuration >=30) {
					 random = Random.Range(0,110);
					 randomDuration = Random.Range(4,30);
				 }

		}}
			public void Loop () {
			if(transform.position.x <= -3) {

			transform.position = new Vector2(-(transform.position.x+0.1f),transform.position.y);
		} else if (transform.position.x >= 3 ) {
			transform.position = new Vector2(-(transform.position.x - 0.1f),transform.position.y);
			
		}} 
}
