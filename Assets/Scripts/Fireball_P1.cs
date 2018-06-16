using UnityEngine;

public class Fireball_P1 : MonoBehaviour
{
    public float bulletSpeed;
    public Animator animator;
    public GameObject DeathClone;
    public GameObject DeathPrefab;
    private bool hasCollided = false;
    void Start()
    {
        hasCollided = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        animator.GetComponent<Animator>();
    }
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("P1_fireball_impact")&& hasCollided && animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag!= "Player_1" && col.gameObject.tag!= "P1_Shield" ){
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<BoxCollider2D>().enabled = false;
            animator.SetTrigger("hasCollided");
            hasCollided = true;
                if (col.gameObject.tag == "Player_2"){
                    P2_Controls.Health -= 0.25f;
                    AI_Controls.Health -= 0.25f;
                    DeathClone = Instantiate(DeathPrefab, GameObject.FindGameObjectWithTag("Player_2").transform.position, Quaternion.identity);
                }
        }
    }
}
