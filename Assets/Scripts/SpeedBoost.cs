using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

	Rigidbody2D BoosterRb;
	public float speed;
	void Start () {
		BoosterRb = GetComponent<Rigidbody2D>();
	}
	void Update () {
		if(transform.position.x == -5f) {
		BoosterRb.velocity = new Vector2(speed,BoosterRb.velocity.y);
		} else if (transform.position.x == 5f) {
					BoosterRb.velocity = new Vector2(-speed,BoosterRb.velocity.y);
					transform.localScale = new Vector3(-1,1,1);
		}

		if(transform.position.x >= 8f || transform.position.x <=-8f ) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{

        

        if (col.gameObject.tag=="Bullet") {
			//P1_Controls.MoveSpeed+=2;
            Destroy(gameObject);
        } else if (col.gameObject.tag=="P2_Bullet") {
			P2_Controls.MoveSpeed+=2;
            Destroy(gameObject);
        }
	}
}
