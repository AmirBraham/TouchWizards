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
    bool notMove;
    float lastxPos;
    void Start()
    {
        notMove=false;
        lastxPos = transform.position.x;
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
        InvokeRepeating("UpdateStrategy",0,Random.Range(1.0f,3.0f));
        timeLeft = Random.Range(10, 30);
        randomStrategy = Random.Range(0, 110);
        RandomStrategytimeleft = Random.Range(5, 20);
    }
    void UpdateStrategy(){
        random = Random.Range(0, 110);
        CancelInvoke();
        InvokeRepeating("UpdateStrategy",Random.Range(3.0f,5.0f),Random.Range(1.0f,4.0f));
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
        PlayerAnimation();

    }

    public void Shield()
    {
        if (DetectedEnemyBullet)
        {
            if (Random.Range(0,100)>50){
                if (BulletInterval <= 0.02f || first)
                    {
                        GameObject BulletClone = Instantiate(Bullet, ShootingPoint.transform.position, Quaternion.Euler(0, 0, -90f)) as GameObject;
                        Shot = true;
                        BulletInterval = BulletIntervalDefault;
                        first = false;
                    }                
            }
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
            FollowStrategy();
        }
        else if (random > 60 && random < 100)
        {
            RunningStrategy();
        }
        else
        {
            RandomStrategy();
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
        }
        else if (transform.position.x > Player1.transform.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, TrackingTransform, MoveSpeed);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    public void RunningStrategy()
    {
        if (transform.position.x < Player1.transform.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-TrackingTransform.x, TrackingTransform.y), MoveSpeed);
        }
        else if (transform.position.x > Player1.transform.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-TrackingTransform.x, TrackingTransform.y), MoveSpeed);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    public void RandomStrategy()
    {       
        if (randomStrategy >= 0 && randomStrategy <= 55)
        {
            rb2d.velocity = new Vector2(MoveSpeed * 40, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(-MoveSpeed * 40, rb2d.velocity.y);
        }

    }
    void PlayerAnimation(){
        if (transform.position.x > lastxPos){
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            GetComponent<Animator>().SetBool("P2_isRunning", true);
            notMove=false;
            
        }else if(transform.position.x < lastxPos){
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            GetComponent<Animator>().SetBool("P2_isRunning", true);
            notMove=false;
 
        }else {
            if(notMove){
                GetComponent<Animator>().SetBool("P2_isRunning", false); 
                notMove=false;
            }else{
                notMove=true;  
            }
        }
        lastxPos = transform.position.x;
        
    }

    void OnDrawGizmosSelected() // draws Bullet Detection circle
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, BulletDetectionRaduis);
    }
}
