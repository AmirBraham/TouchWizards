using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shield : MonoBehaviour {

public  float ShieldResistance = 100;

SpriteRenderer shieldSpriteRenderer;
void Start () {
	shieldSpriteRenderer = GetComponent<SpriteRenderer>();
	if(gameObject.tag=="Player_1") {
		//shieldSpriteRenderer.color 
	}

}
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
