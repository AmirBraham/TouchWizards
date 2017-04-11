using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public float moveSpeed; 
	Rigidbody2D rb2d;

	bool isP1HoldingRB;
	bool isP1HoldingLB;
	bool isP2HoldingRB;
	bool isP2HoldingLB;

	GameObject Player1;
	GameObject Player2;
	
	void Start()
	{
		Player1 = GameObject.FindGameObjectWithTag("Player_1");
		rb2d = Player1.GetComponent<Rigidbody2D>();	
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
		 isP1HoldingRB = false;	 
     }
	  public void P2_onPointerDownRightButton () {
		 isP1HoldingRB=true;
	 }

//Holding Left button
 public void P2_OnPointUpLeftButton(){
		 isP1HoldingLB = false;	 
     }
	  public void P2_onPointerDownLeftButton () {
		 isP1HoldingLB=true;
	 }




void Update()
{
	if(isP1HoldingRB) {
          rb2d.velocity = new Vector2(moveSpeed,rb2d.velocity.y);
         Player1.transform.localScale = new Vector3(1,1,1);
		  Player1.GetComponent<Animator>().SetBool("Player1_isRunning",true);
	} else if(isP1HoldingLB) {
		  rb2d.velocity = new Vector2(-moveSpeed,rb2d.velocity.y);
                Player1.transform.localScale = new Vector3(-1,1,1);
				Player1.GetComponent<Animator>().SetBool("Player1_isRunning",true);

	} else {
				Player1.GetComponent<Animator>().SetBool("Player1_isRunning",false);
					

	}
	
}
	


	
     /*public void onPointerUpRightButton()
     {
         isRightPressed = false;
     }
*/
	/*public void HoldLeftButton()
     {
		 		 Debug.Log("move L");

         rb2d.velocity = new Vector2(-moveSpeed,rb2d.velocity.y);
                transform.localScale = new Vector3(-1,1,1);
     }
     /*public void onPointerUpLeftButton()
     {
         isLeftPressed = false;
     }
	 */
	
}
