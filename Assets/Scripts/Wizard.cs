using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : MonoBehaviour {
	int MoveSpeed = 4;
	float Health = 1;
	bool isHoldingRB;
	bool isHoldingLB;
	Slider P1_HealthSlider;
	 int NumberOfShields =2;
	 GameObject Bullet;
	 Transform ShootingPoint;
	 GameObject ShieldPrefab;
	Transform ShieldPoint;
	 Button Shield1_Button;
	List<float> BoosterXPos = new List<float>();
	Rigidbody2D rb2d;
	Vector3 StartPos;

	public Wizard () 
	{
		
	}

	
	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
		
	}
	public void setHealth(float hl) {
		Health = hl;
	}
	public float getHealth () {
		return Health;
	}
}
