using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScriptNetwork : MonoBehaviour {
	private bool hasCollided = false;
	string layerName;
	Animator animator;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		hasCollided = false;
		layerName = LayerMask.LayerToName (gameObject.layer);
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName(layerName+"_fireball_impact")&& hasCollided && animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		GetComponent<BoxCollider2D>().enabled = false;
		animator.SetTrigger("hasCollided");
		hasCollided = true;
	}
}
