using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_Shield : MonoBehaviour {
	public  float ShieldResistance = 100;
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag=="P2_Bullet" ) {
			ShieldResistance -= 20;
			if(ShieldResistance <= 0) {
			Destroy(gameObject);
			SoloP1_Controls.NumberOfShields--;
                P1_Controls.NumberOfShields--;
		}
		}
	}
}
