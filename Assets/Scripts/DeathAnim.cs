using UnityEngine;

public class DeathAnim : MonoBehaviour {
	public void OnFinishAnim () {
		Destroy(gameObject);
	}
}
