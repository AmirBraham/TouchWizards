using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AI_Controls : MonoBehaviour {

	public static int MoveSpeed =4;
	public static float Health = 1;
	Rigidbody2D rb2d;
		public   Slider HealthSlider;

	public GameObject ShootingPoint;
	public GameObject Bullet;
	public GameObject ShieldPrefab;
	public Transform ShieldPoint;
	public static int NumberOfShields;
	List<float> BoosterXPos = new List<float>();

	public bool DetectedEnemyBullet;
	public bool DetectedEnemy;

	public static bool ReadyToShoot;

	public LayerMask P1_Bullet;
	public LayerMask Enemy;

	public float BulletDetectionRaduis;

	float random ;
	float randomDuration;


	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		BoosterXPos.Add(-5f);
		BoosterXPos.Add(5f);
		NumberOfShields = 2;
		Health =1;
		random	= Random.Range(0,110);
		randomDuration = Random.Range(4,30);
		ReadyToShoot = true;

	}
	
	void Update () {
		DetectedEnemyBullet = Physics2D.OverlapCircle(transform.position,BulletDetectionRaduis,P1_Bullet);
		DetectedEnemy = Physics2D.OverlapArea(transform.position, new Vector2(transform.position.x ,transform.position.y-60f),Enemy);
		Loop ();
		HealthStatus ();
		Movement ();
		Shield(); 
		Shoot();


	}
	
	public void Shield () {
		if(DetectedEnemyBullet) {
			if(NumberOfShields <=3) {
				GameObject ShieldClone;
				ShieldClone = Instantiate(ShieldPrefab,ShieldPoint.position,Quaternion.identity) as GameObject;
				NumberOfShields++;
			}
			
		}
	}
	public void Shoot () {
		if(DetectedEnemy && ReadyToShoot) {
			GameObject BulletClone;
			BulletClone = Instantiate(Bullet,ShootingPoint.transform.position,Quaternion.Euler(0,0,-90f)) as GameObject;
			DetectedEnemy = false;
			ReadyToShoot = false;
		}
	}

	public void HealthStatus () {
		HealthSlider.value = Health;
			if(Health <=0) {
                        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
						GameManager.GameOver.SetActive(true);
						GetComponent<SpriteRenderer>().DOFade(0,2f);	
						GameManager.GameOn.SetActive(false);
						Destroy(gameObject);
			}}

	public void Movement () {
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
			}
		}
	public void Loop () {
			if(transform.position.x <= -3) {
				transform.position = new Vector2(-(transform.position.x+0.1f),transform.position.y);
			} else if (transform.position.x >= 3 ) {
				transform.position = new Vector2(-(transform.position.x - 0.1f),transform.position.y);
			}
		} 

	void OnDrawGizmosSelected()
     {
		Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BulletDetectionRaduis);
		
	 }
}
