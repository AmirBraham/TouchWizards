using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    public Transform ShootingPoint;
    public GameObject Bullet;
    public RuntimeAnimatorController RedBulletAnim;
    void Update ()
	{
		if (!isLocalPlayer) {
			if(isServer)
			transform.rotation = Quaternion.Euler (0, 0, 180);
            
            

		} else {
            if (!isServer)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
               

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
	void OnGUI ()
	{
		if (GUI.RepeatButton (new Rect (400, 100, 300, 100), "left")) {
			gameObject.transform.Translate (0.1f, 0, 0);
		}

	}
	*/
}