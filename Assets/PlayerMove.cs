using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    [SyncVar]
    public float xPos;

    void Update()
    {
        if (!isLocalPlayer)
        {
            gameObject.transform.position = new Vector3(xPos,gameObject.transform.position.y,gameObject.transform.position.z) ;
            gameObject.transform.localRotation = Quaternion.Euler(180,0,0);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
            return;
        }

        var x = Input.GetAxis("Horizontal")*0.1f;
        var z = Input.GetAxis("Vertical")*0.1f;

        transform.Translate(x, 0, z);
        xPos = gameObject.transform.position.x;
        CmdChangexPos(xPos);
    }
    [Command]
    public void CmdChangexPos(float changedPos)
    {
        xPos = changedPos;   
    }
}