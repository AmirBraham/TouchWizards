using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class P1_shoot_Movement : MonoBehaviour {
	public float bulletSpeed;
	GameObject P2_DeathClone;
	public GameObject P2_DeathPrefab;
	// Update is called once per frame
	void Update () {
		if(GetComponent<Rigidbody2D>() != null) {
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,bulletSpeed);
		}
	}

	void OnFinishAnim () {
		Destroy(gameObject);
	}
	void OnTriggerEnter2D (Collider2D col) {
		if(col.gameObject.tag == "Player_2") {
			P2_Controls.Health -= 0.25f;
			P2_DeathClone = Instantiate(P2_DeathPrefab,GameObject.FindGameObjectWithTag("Player_2").transform.position,Quaternion.identity);
		} else if (col.gameObject.tag == "P2_Bullet") {
			Destroy(col.gameObject);
			Destroy(gameObject);
		} else if (col.gameObject.tag =="P2_Shield") {
			Destroy(gameObject);

		} else if (col.gameObject.tag =="ground") {
			Destroy(gameObject);
		}

	}

	
}
