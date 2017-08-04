using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
	void Update ()
	{
		if (!isLocalPlayer) {
			return;
		}


		if (Network.isClient && isLocalPlayer) {
			gameObject.transform.localRotation = Quaternion.Euler (180, 0, 0);
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = -1;
		}
	}

	void OnGUI ()
	{
		if (GUI.RepeatButton (new Rect (400, 100, 300, 100), "left")) {
			gameObject.transform.Translate (0.1f, 0, 0);
		}

	}
}