using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class P2_shoot_Movement : MonoBehaviour {
	public float bulletSpeed;
	public GameObject DeathClone;

	public GameObject DeathPrefab;


	void Start () {

	}
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,-bulletSpeed);
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		if(col.gameObject.tag == "Player_1") {
			Debug.Log("Player 2 Won");
			GameManager.P1_Health -= 0.25f;

 			//col.gameObject.GetComponent<SpriteRenderer>().DOFade(0,2f);

			DeathClone = Instantiate(DeathPrefab,GameObject.FindGameObjectWithTag("Player_1").transform.position,Quaternion.identity);

		} else if (col.gameObject.tag == "P1_Bullet") {
			Destroy(col.gameObject);
			Destroy(gameObject);
		} else if (col.gameObject.tag =="P1_Shield") {
			Destroy(gameObject);
		} else if (col.gameObject.tag =="ground") {
			Destroy(gameObject);

		}


	}


	
}
