using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AI_Controls : MonoBehaviour
{
    public static float MoveSpeed = 0.05f;
    public static float Health = 1;
    public static bool GameStarted;
    public GameObject Player1;
    Rigidbody2D rb2d;
    public Slider HealthSlider;
    public GameObject ShootingPoint;
    public GameObject Bullet;
    public GameObject ShieldPrefab;
    public Transform ShieldPoint;
    public static int NumberOfShields;
    List<float> BoosterXPos = new List<float>();
    public bool DetectedEnemyBullet;
    public bool DetectedEnemy;
    bool hasChanged;
    public LayerMask P1_Bullet;
    public LayerMask AI_Bullet;
    public static bool ReadyToShoot;
    public LayerMask Enemy;
    public static List<GameObject> Shields = new List<GameObject>();
    public float BulletDetectionRaduis;
    public float BulletShootingRaduis;
    Vector2 TrackingTransform;

    void Start()
    {
        hasChanged = false;
        rb2d = GetComponent<Rigidbody2D>();
        BoosterXPos.Add(-5f);
        BoosterXPos.Add(5f);
        NumberOfShields = 0;
        ReadyToShoot = true;
        Player1 = GameObject.FindWithTag("Player_1");
        Health = 1;
        Debug.Log(MoveSpeed);
        if (PlayerPrefs.GetInt("CurrentScore") != 0)
        {
            MoveSpeed += PlayerPrefs.GetInt("CurrentScore") / 100;
            BulletDetectionRaduis += PlayerPrefs.GetInt("CurrentScore") / 1.4f;
            BulletShootingRaduis -= PlayerPrefs.GetInt("CurrentScore") / 1.4f;
        }
        if (Health <= 4 && PlayerPrefs.GetInt("CurrentScore") != 0)
        {

            Health = PlayerPrefs.GetInt("CurrentScore") / 1.4f;
            HealthSlider.maxValue = Health;
        }
    }

    void Update()
    {
        if (Player1 != null)
        {
            TrackingTransform = new Vector2(Player1.GetComponent<Transform>().position.x,
                                       2.6f);
        }

        if (GameObject.FindGameObjectWithTag("Tutorial") == null)
        {
            DetectedEnemyBullet = Physics2D.OverlapCircle(transform.position, BulletDetectionRaduis, P1_Bullet);
            DetectedEnemy = Physics2D.OverlapArea(transform.position, new Vector2(transform.position.x, transform.position.y - 60f), Enemy);
            Loop();
            HealthStatus();
            if (Player1 != null)
                Movement();
            Shield();
            Shoot();
        }

    }

    public void Shield()
    {
        if (DetectedEnemyBullet)
        {
            if (NumberOfShields == 1 && System.Math.Abs(Shields[Shields.Count - 1].transform.position.x - transform.position.x) > 1)
            {
                if (NumberOfShields <= 2)
                {
                    Shields.Add(Instantiate(ShieldPrefab, ShieldPoint.position, Quaternion.identity) as GameObject);
                    NumberOfShields++;
                }
            }
            else
            {
                if (NumberOfShields <= 2)
                {
                    Shields.Add(Instantiate(ShieldPrefab, ShieldPoint.position, Quaternion.identity) as GameObject);
                    NumberOfShields++;
                }
            }

        }

    }

    public void Shoot()
    {

        ReadyToShoot = !Physics2D.OverlapCircle(transform.position, BulletShootingRaduis, AI_Bullet);
        if (DetectedEnemy && ReadyToShoot)
        {
            GameObject BulletClone = Instantiate(Bullet, ShootingPoint.transform.position, Quaternion.Euler(0, 0, -90f)) as GameObject;
            DetectedEnemy = false;
            ReadyToShoot = false;
        }
    }

    public void HealthStatus()
    {
        HealthSlider.value = Health;
        if (Health <= 0)
        {
            if (!hasChanged)
                PlayerPrefs.SetInt("CurrentScore", PlayerPrefs.GetInt("CurrentScore") + 1);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            SoloGameManager.GameOver.SetActive(true);
            SoloGameManager.replaytext = "Next";
            SoloGameManager.P1_GameOverText = "You Win!";
            GetComponent<SpriteRenderer>().DOFade(0, 2f);
            SoloGameManager.GameOn.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void Movement()
    {
        if (transform.position.x < Player1.transform.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, TrackingTransform, MoveSpeed);
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            GetComponent<Animator>().SetBool("P2_isRunning", true);
        }
        else if (transform.position.x > Player1.transform.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, TrackingTransform, MoveSpeed);
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            GetComponent<Animator>().SetBool("P2_isRunning", true);
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetBool("P2_isRunning", false);
        }
    }

    public void Loop()
    {
        if (transform.position.x <= -3)
        {
            transform.position = new Vector2(-(transform.position.x + 0.1f), transform.position.y);
        }
        else if (transform.position.x >= 3)
        {
            transform.position = new Vector2(-(transform.position.x - 0.1f), transform.position.y);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, BulletDetectionRaduis);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BulletShootingRaduis);
    }
}
