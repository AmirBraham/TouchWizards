using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
<<<<<<< HEAD
	void Update ()
	{
		if (!isLocalPlayer) {
			return;
		}
=======
    public Transform ShootingPoint;
    public GameObject Bullet;
    public RuntimeAnimatorController RedBulletAnim;
    void Update ()
	{
		if (!isLocalPlayer) {
			if(isServer)
			transform.rotation = Quaternion.Euler (0, 0, 180);
            
            
>>>>>>> V2

		} else {
            if (!isServer)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
               

<<<<<<< HEAD
		if (Network.isClient && isLocalPlayer) {
			gameObject.transform.localRotation = Quaternion.Euler (180, 0, 0);
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = -1;
		}
	}

=======
            }
            float h = Input.GetAxis ("Horizontal");
			GetComponent<Rigidbody2D> ().transform.Translate (h * 0.1f, 0, 0);
            if (Input.GetKeyDown(KeyCode.Space))
                Cmdshoot();
		}
	}
    [Command]
    void Cmdshoot()
    {
        GameObject _bullet = Instantiate(Bullet, ShootingPoint.position, ShootingPoint.rotation);
        _bullet.GetComponent<Rigidbody2D>().velocity = _bullet.transform.right * 10;
        NetworkServer.Spawn(_bullet);
        
    }
    /*
>>>>>>> V2
	void OnGUI ()
	{
		if (GUI.RepeatButton (new Rect (400, 100, 300, 100), "left")) {
			gameObject.transform.Translate (0.1f, 0, 0);
		}

	}
<<<<<<< HEAD
=======
	*/
>>>>>>> V2
}