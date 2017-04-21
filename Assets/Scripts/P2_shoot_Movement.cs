using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class P2_shoot_Movement : MonoBehaviour {
	public float bulletSpeed;
	GameObject DeathClone;

	public GameObject DeathPrefab;


	void Start () {

	}
	// Update is called once per frame
	void Update () {
				if(GetComponent<Rigidbody2D>() != null) {

		GetComponent<Rigidbody2D>().velocity = new Vector2(0,-bulletSpeed);
				}
	}

	void OnFinishAnim () {
		Destroy(gameObject);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if(col.gameObject.tag == "Player_1") {
			//GameManager.Health -= 0.25f;
			DeathClone = Instantiate(DeathPrefab,GameObject.FindGameObjectWithTag("Player_1").transform.position,Quaternion.identity);

		} else if (col.gameObject.tag == "Bullet") {
			Destroy(col.gameObject);
			Destroy(gameObject);
		} else if (col.gameObject.tag =="Shield") {
			Destroy(gameObject);
		} else if (col.gameObject.tag =="ground") {
			Destroy(gameObject);

		}


	}


	
}
