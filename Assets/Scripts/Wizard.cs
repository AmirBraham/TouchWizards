using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Wizard : MonoBehaviour {
	int MoveSpeed;
	float Health;
	bool isHoldingRB;
	bool isHoldingLB;
	Slider P1_HealthSlider;
	int NumberOfShields;
	GameObject Bullet;
	Transform ShootingPoint;
	GameObject ShieldPrefab;
	Vector2 ShieldPoint;
	Button Shield1_Button;
	List<float> BoosterXPos = new List<float>();
	Rigidbody2D rb2d;
	Vector3 StartPos;


	public Wizard () {}

	public void setMoveSpeed (int moveSpeed) {
		MoveSpeed = moveSpeed;
	}
	public int getMoveSpeed () {
		return MoveSpeed;
	}
	public void setNumberOfShields (int numberOfShields) {
		NumberOfShields = numberOfShields;
	}	

	public int getNumberOfShields () {
		return NumberOfShields;
	}
	public void setShieldPrefab (GameObject prefab) {
		ShieldPrefab = prefab;
	}
	public GameObject getShieldPrefab () {
		return ShieldPrefab;
	}

	public void setShieldPoint (Vector2 position) {
		ShieldPoint = position;
	}
	public Vector2 getShieldPoint () {
		return ShieldPoint;
	}
	public void setShootingPoint (Transform position) {
		ShootingPoint = position;
	}
	public Transform getShootingPoint() {
		return ShootingPoint;
	}

	public void setRigidBody (Rigidbody2D rb) {
		rb2d = rb;
	}
	public Rigidbody2D getRigidBody() {
		return rb2d;
	}
	public void setHealth(float hl) {
		Health = hl;
	}
	public float getHealth () {
		return Health;
	}

	public void setIsHoldingRB (bool x) {
		isHoldingRB = x;
	}
	public bool getIsHoldingRB () {
		return isHoldingRB;
	}

	public void setIsHoldingLB (bool x) {
		isHoldingRB = x;
	}
	public bool getIsHoldingLB () {
		return isHoldingRB;
	}

	public void GenerateControls () {
			Instantiate(new GameObject("leftButton"),Vector3.zero,Quaternion.identity);
	}

	public void GenerateShield () {
			Instantiate(ShieldPrefab,ShieldPoint,Quaternion.identity);
	}

	public void Movement () {

		//if(isHoldingRB) {
					gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(MoveSpeed,gameObject.GetComponent<Rigidbody2D>().velocity.y);
					transform.localScale = new Vector3(1.5f,1.5f,1.5f);
					GetComponent<Animator>().SetBool("Player1_isRunning",true);
		/*} else if(isHoldingLB) {
					rb2d.velocity = new Vector2(-MoveSpeed,rb2d.velocity.y);
					transform.localScale = new Vector3(-1.5f,1.5f,1.5f);
					GetComponent<Animator>().SetBool("Player1_isRunning",true);
		} else {
					rb2d.velocity = new Vector2(0,0);
					GetComponent<Animator>().SetBool("Player1_isRunning",false);
		}
		*/
    }
	public void Loop () {
		if(transform.position.x <= -3) {
		    transform.position = new Vector2(-(transform.position.x+0.1f),transform.position.y);
		} else if (transform.position.x >= 3 ) {
	        transform.position = new Vector2(-(transform.position.x-0.1f),transform.position.y);
	    }
    }

	
	      

}
