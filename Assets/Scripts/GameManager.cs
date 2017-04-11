using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public float moveSpeed; 
	Rigidbody2D rb2d;

	bool isHoldingRB;
	bool isHoldingLB;

	GameObject Player1;
	
	void Start()
	{
		Player1 = GameObject.FindGameObjectWithTag("Player_1");
		rb2d = Player1.GetComponent<Rigidbody2D>();	
	}


//Holding Right Button
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
void Update()
{
	if(isHoldingRB) {
          rb2d.velocity = new Vector2(moveSpeed,rb2d.velocity.y);
         Player1.transform.localScale = new Vector3(1,1,1);
		  Player1.GetComponent<Animator>().SetBool("Player1_isRunning",true);
	} else if(isHoldingLB) {
		  rb2d.velocity = new Vector2(-moveSpeed,rb2d.velocity.y);
                transform.localScale = new Vector3(-1,1,1);
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
