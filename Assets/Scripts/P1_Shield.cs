using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {


	float ShieldResistance = 100;
	
	// Update is called once per frame
	void Update () {
		if(ShieldResistance <= 0) {
			Destroy(gameObject);
			GameManager.NumberOfShields-= 1;
		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag=="P2_Bullet" ) {
			ShieldResistance -= 20;
		}
	}
}
