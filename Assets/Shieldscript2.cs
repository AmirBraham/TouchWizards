using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shieldscript2 : MonoBehaviour {

	float ShieldResistance = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(ShieldResistance <= 0) {
			Destroy(gameObject);
		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag=="P1_Bullet" ) {
			ShieldResistance -= 20;
						Destroy(col.gameObject);

		}
	}
}
