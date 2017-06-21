using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	Transform ShieldPoint;
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

	public void setShieldPoint (Transform position) {
		ShieldPoint = position;
	}
	public Transform getShieldPoint () {
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

}
