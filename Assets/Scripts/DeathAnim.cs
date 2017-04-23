using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnim : MonoBehaviour {
	public void OnFinishAnim () {
		Destroy(gameObject);
	}
}
