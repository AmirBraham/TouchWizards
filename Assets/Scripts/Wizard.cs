using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Wizard : MonoBehaviour {
	string Wizard_Name;
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

	public void setWizardName (string name) {
		Wizard_Name = name;
	} 
	public string getWizardName () {
		return Wizard_Name;
	}

	
	
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

	public void GenerateControls (Sprite MoveButton) {
			GameObject Canvas = Instantiate(new GameObject(Wizard_Name+"_Canvas"),Vector3.zero,Quaternion.identity) as GameObject;
			Canvas.AddComponent<RectTransform>();
			Canvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
			Canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(1080,1920);
			Canvas.GetComponent<RectTransform>().localScale = new Vector3(0.0052f,0.0052f,0.0052f);
			Canvas.GetComponent<RectTransform>().position = new Vector3(0,0,90);
			Canvas.AddComponent<Canvas>();
			Canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
			Canvas.GetComponent<Canvas>().worldCamera = Camera.main;
			Canvas.GetComponent<Canvas>().sortingOrder = 30;
			Canvas.AddComponent<CanvasScaler>();
			Canvas.AddComponent<GraphicRaycaster>();
			GameObject LeftButton =  Instantiate(new GameObject(Wizard_Name+"_LeftButton"),Vector3.zero,Quaternion.identity) as GameObject;
			Destroy (LeftButton.GetComponent<Transform>());
						LeftButton.transform.parent = Canvas.transform;

			LeftButton.AddComponent<RectTransform>();
			LeftButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-403f,-851f,0.1f);
			LeftButton.GetComponent<RectTransform>().sizeDelta = new Vector2(163,154);
			LeftButton.GetComponent<RectTransform>().localScale = new Vector3(1.5f,1.5f,1.5f);
			LeftButton.GetComponent<RectTransform>().localRotation =Quaternion.Euler(0,0,90);
			LeftButton.AddComponent<CanvasRenderer>();
			LeftButton.AddComponent<Image>();
			LeftButton.GetComponent<Image>().sprite = MoveButton;
			LeftButton.AddComponent<Button>();
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
