using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_shoot_Movement : MonoBehaviour {
	public float bulletSpeed;
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,-bulletSpeed);
	}
}
