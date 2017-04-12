using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public float moveSpeed; 
	Rigidbody2D P1_rb2d;
	Rigidbody2D P2_rb2d;

	GameObject P1_ShootingPoint;
	GameObject P2_ShootingPoint;

	GameObject P1_Bullet;
	GameObject P2_Bullet;

	bool isP1HoldingRB;
	bool isP1HoldingLB;
	bool isP2HoldingRB;
	bool isP2HoldingLB;

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
				 		  Player2.GetComponent<Animator>().SetBool("Player2_isRunning",false);


	}


/*
	GameObject P1_BulletClone;
	P1_BulletClone = Instantiate(P1_Bullet,P1_ShootingPoint.transform.position,Quaternion.identity) as GameObject;
	*/

}
}
