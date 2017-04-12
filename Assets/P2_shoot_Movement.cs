using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_shoot_Movement : MonoBehaviour {
	public float bulletSpeed;
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,-bulletSpeed);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if(col.gameObject.tag == "Player_1") {
			Debug.Log("Player 1 Won");
			Destroy(col.gameObject);
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
