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
    public static int NumberOfShields; // max shields deployed at the same time : 2
    public bool DetectedEnemyBullet;
    public bool DetectedEnemy;
    bool hasChanged;
    public LayerMask P1_Bullet;
    public LayerMask AI_Bullet;
    public LayerMask Enemy;
    public static List<GameObject> Shields = new List<GameObject>();
    public float BulletDetectionRaduis;
    public float BulletInterval; // time between instantiating bullets
    public float BulletIntervalDefault;
    public bool Shot;
    public bool first;
    Vector2 TrackingTransform; // Player position 
    float random;
    float timeLeft;
    float randomStrategy;
    float RandomStrategytimeleft;
    public float shieldtiming; // time in seconds  before Ai can deploy another shield
    float defaultShieldTiming;
    bool deployedShield;
    void Start()
    {
        defaultShieldTiming = shieldtiming;
        first = true;
        hasChanged = false;
        rb2d = GetComponent<Rigidbody2D>();
        NumberOfShields = 0;
        Player1 = GameObject.FindWithTag("Player_1");
        Health = 1;
        if (PlayerPrefs.GetInt("CurrentScore") != 0)
        {
            MoveSpeed += PlayerPrefs.GetInt("CurrentScore") / 100.0f;
            if (BulletIntervalDefault >= 0.08)
                BulletIntervalDefault = 1.0f - PlayerPrefs.GetInt("CurrentScore") / 70.0f;
            BulletDetectionRaduis += PlayerPrefs.GetInt("CurrentScore") / 1.4f;
        }
        if (Health <= 4 && PlayerPrefs.GetInt("CurrentScore") != 0)
        {
            Health = PlayerPrefs.GetInt("CurrentScore") / 1.4f;
            HealthSlider.maxValue = Health;
        }

        random = Random.Range(0, 110);
        timeLeft = Random.Range(10, 30);
        randomStrategy = Random.Range(0, 110);
        RandomStrategytimeleft = Random.Range(5, 20);
    }

    void Update()
    {
        if (deployedShield)
            shieldtiming -= Time.deltaTime;
        if (Player1 != null)
        {
            TrackingTransform = new Vector2(Player1.GetComponent<Transform>().position.x,
                                       2.6f);
        }

        if (GameObject.FindGameObjectWithTag("Tutorial") == null)
        {
            DetectedEnemyBullet = Physics2D.OverlapCircle(transform.position, BulletDetectionRaduis, P1_Bullet); // if bullet is within AI range 
            DetectedEnemy = Physics2D.OverlapArea(transform.position, new Vector2(transform.position.x, transform.position.y - 60f), Enemy); // if player is within AI shooting range 
            Loop();
            HealthStatus();
            if (Player1 != null)
                Movement();
            Shield();
            Shoot();
        }
        if (Shot)
        {
            BulletInterval -= Time.deltaTime;
        }

    }

    public void Shield()
    {
        if (DetectedEnemyBullet)
        {
            if (!deployedShield || shieldtiming < 1)
            {
                if (NumberOfShields == 1 && System.Math.Abs(Shields[Shields.Count - 1].transform.position.x - transform.position.x) > 1)
                {
                    if (NumberOfShields <= 2)
                    {
                        Shields.Add(Instantiate(ShieldPrefab, ShieldPoint.position, Quaternion.identity) as GameObject);
                        deployedShield = true;
                        shieldtiming = defaultShieldTiming;
                        NumberOfShields++;
                    }
                }
                else
                {
                    if (NumberOfShields <= 2)
                    {
                        Shields.Add(Instantiate(ShieldPrefab, ShieldPoint.position, Quaternion.identity) as GameObject);
                        deployedShield = true;
                        NumberOfShields++;
                        shieldtiming = defaultShieldTiming;

                    }
                }
            }
        }
    }
    public void Shoot()
    {
        if ((BulletInterval <= 0.02f || first) && DetectedEnemy)
        {
            GameObject BulletClone = Instantiate(Bullet, ShootingPoint.transform.position, Quaternion.Euler(0, 0, -90f)) as GameObject;
            Shot = true;
            BulletInterval = BulletIntervalDefault;
            first = false;
        }
    }

    public void HealthStatus() //  manges both health and UIA slider
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
            SoloP1_Controls.Health = 1;
            SoloGameManager.GameOn.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void Movement() // chooses a strategy randomly for a short period of time 
    {

        if (random > 30 && random < 60)
        {
            Debug.Log("follow");

            FollowStrategy();

            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                random = Random.Range(0, 110);
                timeLeft = Random.Range(10, 30);
            }
        }
        else if (random > 60 && random < 100)
        {
            RunningStrategy();
            timeLeft -= Time.deltaTime;
            Debug.Log("RUN");

            if (timeLeft <= 0)
            {
                random = Random.Range(0, 110);
                timeLeft = Random.Range(10, 30);
            }
        }
        else
        {
            RandomStrategy();
            Debug.Log("");

            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                random = Random.Range(0, 110);
                timeLeft = Random.Range(1, 30);
            }
        }
    }

    public void Loop() // transports AI to the other side when he reaches the edge 
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

    public void FollowStrategy()
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

    public void RunningStrategy()
    {
        if (transform.position.x < Player1.transform.position.x)
        {

            if (transform.position.x > 0)
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
            else
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-TrackingTransform.x, TrackingTransform.y), MoveSpeed);
            GetComponent<Animator>().SetBool("P2_isRunning", true);
        }
        else if (transform.position.x > Player1.transform.position.x)
        {


            if (transform.position.x > 0)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-TrackingTransform.x, TrackingTransform.y), MoveSpeed);
            GetComponent<Animator>().SetBool("P2_isRunning", true);
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetBool("P2_isRunning", false);
        }
    }

    public void RandomStrategy()
    {

        if (randomStrategy >= 0 && randomStrategy <= 55)
        {
            rb2d.velocity = new Vector2(MoveSpeed * 40, rb2d.velocity.y);
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            GetComponent<Animator>().SetBool("Player2_isRunning", true);
            RandomStrategytimeleft -= Time.deltaTime;
            if (RandomStrategytimeleft <= 0)
            {
                randomStrategy = Random.Range(0, 110);
                RandomStrategytimeleft = Random.Range(1, 3);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(-MoveSpeed * 40, rb2d.velocity.y);
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            GetComponent<Animator>().SetBool("Player2_isRunning", true);
            RandomStrategytimeleft -= Time.deltaTime;
            if (RandomStrategytimeleft <= 0)
            {
                randomStrategy = Random.Range(0, 110);
                RandomStrategytimeleft = Random.Range(1, 3);
            }
        }

    }

    void OnDrawGizmosSelected() // draws Bullet Detection circle
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, BulletDetectionRaduis);
    }
}
