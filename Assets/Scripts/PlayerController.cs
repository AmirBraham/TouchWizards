using UnityEngine;
using System.Collections;
using CnControls;

public class PlayerController : MonoBehaviour {

               //Floating point variable to store the player's movement speed.
    public float jumpSpeed;
    bool canDoubleJump;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public GameObject point;
    public float radius;
    public LayerMask whatIsGround;

    public bool isGrounded;

    GameManager game_manager;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        

        //check if the player in on ground
        isGrounded = Physics2D.OverlapCircle(point.transform.position,radius,whatIsGround);
       
        
    }

    void Update () {
       /* if        (game_manager.isRightPressed && game_manager.isLeftPressed==false) {
                rb2d.velocity = new Vector2(moveSpeed,rb2d.velocity.y);
                transform.localScale = new Vector3(1,1,1);

        } else if (game_manager.isLeftPressed && game_manager.isRightPressed==false) {
                rb2d.velocity = new Vector2(-moveSpeed,rb2d.velocity.y);
                transform.localScale = new Vector3(-1,1,1);
                
        } else {
                rb2d.velocity = new Vector2(0,rb2d.velocity.y);
                
        }
        */

       /* if(CnInputManager.GetAxisRaw("Vertical") > 0) {
            
            if(isGrounded) {
                rb2d.velocity = new Vector2(rb2d.velocity.x,jumpSpeed);

            } 
                

            } else {
                 //GetComponent<Animator>().SetBool("isJumping",false); 

            }
        }
    

     void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point.transform.position, radius);

    }
    */
}
}
