using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class P1_shoot_Movement : MonoBehaviour {

	public float bulletSpeed;

	public  GameObject DeathClone;

	public GameObject DeathPrefab;
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,bulletSpeed);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if(col.gameObject.tag == "Player_2") {
			Debug.Log("Player 1 Won");
			GameManager.P2_Health -= 25;
			//col.gameObject.GetComponent<SpriteRenderer>().DOFade(0,2f);
			DeathClone = Instantiate(DeathPrefab,GameObject.FindGameObjectWithTag("Player_2").transform.position,Quaternion.identity);

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
