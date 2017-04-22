using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_Shield : MonoBehaviour {


	public  float ShieldResistance = 100;
	
	// Update is called once per frame


	void Start()
	{
		
	}
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag=="P1_Bullet" ) {
			ShieldResistance -= 20;
			if(ShieldResistance <= 0) {
			Destroy(gameObject);
			P2_Controls.NumberOfShields--;
		}
		}
	}
}
