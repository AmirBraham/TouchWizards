using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public float moveSpeed; 
	Rigidbody2D P1_rb2d;
	Rigidbody2D P2_rb2d;

	public GameObject P1_ShootingPoint;
	public GameObject P2_ShootingPoint;

	public GameObject P1_Bullet;

	public GameObject P1_ShieldPrefab;

	public Transform P1_ShieldPoint;
	public GameObject P2_Bullet;


	public GameObject P2_ShieldPrefab;

	public Transform P2_ShieldPoint;


	bool isP1HoldingRB;
	bool isP1HoldingLB;
	bool isP2HoldingRB;
	bool isP2HoldingLB;

	float P1PosX;


	int P1_NumberOfShields = 0 ;
	int P2_NumberOfShields = 0 ;

	GameObject Player1;
	GameObject Player2;
	
	void Start()
	{

		Player1 = GameObject.FindGameObjectWithTag("Player_1");
		Player2 = GameObject.FindGameObjectWithTag("Player_2");
		P1_rb2d = Player1.GetComponent<Rigidbody2D>();	
		P2_rb2d = Player2.GetComponent<Rigidbody2D>();
	}

//P1 CONTROLS
//Holding Right Button
	 public void P1_OnPointUpRightButton(){
		 isP1HoldingRB = false;	 
     }
	  public void P1_onPointerDownRightButton () {
		 isP1HoldingRB=true;
	 }

//Holding Left button
 public void P1_OnPointUpLeftButton(){
		 isP1HoldingLB = false;	 
     }
	  public void P1_onPointerDownLeftButton () {
		 isP1HoldingLB=true;
	 }


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


public void P1_Shoot () {
	GameObject P1_BulletClone;
	P1_BulletClone = Instantiate(P1_Bullet,P1_ShootingPoint.transform.position,Quaternion.Euler(0,0,90f)) as GameObject;
}


public void P1_Shield () {
	if(P1_NumberOfShields < 2) {
	GameObject P1_ShieldClone;
	P1_ShieldClone = Instantiate(P1_ShieldPrefab,P1_ShieldPoint.position,Quaternion.identity) as GameObject;
	P1_NumberOfShields++;
	}
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

void Update()
{
	if(isP1HoldingRB) {
          P1_rb2d.velocity = new Vector2(moveSpeed,P1_rb2d.velocity.y);
         Player1.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
		  Player1.GetComponent<Animator>().SetBool("Player1_isRunning",true);
	} else if(isP1HoldingLB) {
		 		P1_rb2d.velocity = new Vector2(-moveSpeed,P1_rb2d.velocity.y);
                Player1.transform.localScale = new Vector3(-1.5f,1.5f,1.5f);
				Player1.GetComponent<Animator>().SetBool("Player1_isRunning",true);

	} else {
				P1_rb2d.velocity = new Vector2(0,0);
				Player1.GetComponent<Animator>().SetBool("Player1_isRunning",false);
	}


	if(isP2HoldingRB) {
		P2_rb2d.velocity = new Vector2(moveSpeed,P2_rb2d.velocity.y);
		         Player2.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
				 		  Player2.GetComponent<Animator>().SetBool("Player2_isRunning",true);


	} else if (isP2HoldingLB) {
P2_rb2d.velocity = new Vector2(-moveSpeed,P2_rb2d.velocity.y);
		         Player2.transform.localScale = new Vector3(-1.5f,1.5f,1.5f);
				 		  Player2.GetComponent<Animator>().SetBool("Player2_isRunning",true);
	} else {
						P2_rb2d.velocity = new Vector2(0,0);

				 		  Player2.GetComponent<Animator>().SetBool("Player2_isRunning",false);


	}


/*
	
	*/
	if(Player1.transform.position.x <= -3) {

		Player1.transform.position = new Vector2(-(Player1.transform.position.x+0.1f),Player1.transform.position.y);
	} else if (Player1.transform.position.x >= 3 ) {
		Player1.transform.position = new Vector2(-(Player1.transform.position.x - 0.1f),Player1.transform.position.y);
		
	}


	if(Player2.transform.position.x <= -3) {

		Player2.transform.position = new Vector2(-(Player2.transform.position.x+0.1f),Player2.transform.position.y);
	} else if (Player2.transform.position.x >= 3 ) {
		Player2.transform.position = new Vector2(-(Player2.transform.position.x - 0.1f),Player2.transform.position.y);
		
	}


}
}
