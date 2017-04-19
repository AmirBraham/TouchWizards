using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

	Rigidbody2D BoosterRb;
	// Use this for initialization
	void Start () {
		BoosterRb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x == -5f) {
		BoosterRb.velocity = new Vector2(8f,BoosterRb.velocity.y);
		} else if (transform.position.x == 5f) {
					BoosterRb.velocity = new Vector2(-8f,BoosterRb.velocity.y);
					transform.localScale = new Vector3(-1,1,1);
		}

		if(transform.position.x >= 8f || transform.position.x <=-8f ) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag=="P1_Bullet") {
			GameManager.P1_MoveSpeed+=2;
			
			Destroy(gameObject);
		} else if (col.gameObject.tag=="P2_Bullet") {
			GameManager.P2_MoveSpeed+=2;
			Destroy(gameObject);

		}
	}
}
