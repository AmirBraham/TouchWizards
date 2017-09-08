using UnityEngine;
using UnityEngine.Networking;

public class WizardScriptNet : NetworkBehaviour
{
	public static int MoveSpeed ;
	public Transform ShootingPoint;
	public GameObject BulletP1;
	public GameObject BulletP2;
	public GameObject ShieldP1;
	public GameObject ShieldP2;
	public GameObject healthbarP1;
	public GameObject healthbarP2;
	public RuntimeAnimatorController RedWizardAnim;
	Rigidbody2D rb2d;
	public static bool isHoldingRB;
	public static bool isHoldingLB;
	int dir;
	public GameObject canvas;
	void Start(){
		PlayerPrefs.DeleteAll ();
		dir = isServer ? 1 : -1;
		if (!isLocalPlayer) {
			gameObject.layer = !isServer ? LayerMask.NameToLayer ("P1") : LayerMask.NameToLayer ("P2");
		} else {
			gameObject.layer = isServer ? LayerMask.NameToLayer("P1") : LayerMask.NameToLayer("P2"); 
		}
		MoveSpeed = 4;
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		canvas= gameObject.transform.Find("Canvas").gameObject;
		GameObject healthBar;
		if(!isServer){
			if (isLocalPlayer) {
				healthBar = Instantiate(healthbarP2, healthbarP1.transform,false);
				healthBar.transform.SetParent(canvas.transform,false);
			} else {
				healthBar = Instantiate(healthbarP1, healthbarP2.transform,false);
				healthBar.transform.SetParent(canvas.transform,false);
			}

		}else if(isServer){
			if(isLocalPlayer){
				healthBar = Instantiate(healthbarP1, healthbarP1.transform,false);
				healthBar.transform.SetParent(canvas.transform,false);

			}else{
				healthBar = Instantiate(healthbarP2, healthbarP2.transform,false);
				healthBar.transform.SetParent(canvas.transform,false);
			}
		}

	}
	void Update ()
	{
		if (!isLocalPlayer) {
			if (isServer) {
				transform.rotation = Quaternion.Euler (0, 0, 180);
				GetComponent<Animator>().runtimeAnimatorController = RedWizardAnim;
			}
			string AnimatorVar = LayerMask.LayerToName(gameObject.layer);
			if (Mathf.Abs(rb2d.velocity.x) < MoveSpeed/2) {
				GetComponent<Animator>().SetBool(AnimatorVar+"_isRunning", false);
			} else {
				GetComponent<Animator>().SetBool(AnimatorVar+"_isRunning", true);
				if (rb2d.velocity.x < 0) {
					transform.localScale = new Vector3 (dir*Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
				} else {
					transform.localScale = new Vector3 (-dir*Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
				
				}
			}

			return;
		}

		Loop();
		gameObject.tag = "localPlayer";
		Movement ();

		if (!isServer)
		{
			GetComponent<Animator>().runtimeAnimatorController = RedWizardAnim;
			transform.rotation = Quaternion.Euler(0, 0, 180);
			GameObject.FindGameObjectWithTag ("camera").transform.rotation = Quaternion.Euler (0,0,180);
		}

		if (Input.GetKeyDown(KeyCode.Space))
			Cmdshoot(dir);
	}

	[Command]
	void Cmdshoot(int direction)
	{
		GameObject Bullet = direction == 1 ? BulletP1 : BulletP2;
		GameObject _bullet = Instantiate(Bullet, ShootingPoint.position, Quaternion.Euler(0,0,90*direction));
		_bullet.GetComponent<Rigidbody2D>().velocity = _bullet.transform.right * 10;
		NetworkServer.Spawn(_bullet);
	}

	[Command]
	void CmdcastShield(int direction)
	{
		GameObject Shield= direction == 1 ? ShieldP1 : ShieldP2;
		GameObject _shield = Instantiate(Shield, ShootingPoint.position, Quaternion.identity);
		NetworkServer.Spawn(_shield);
	}
	public void shoot(){
		Cmdshoot(dir);
	}
	public void castShield(){
		CmdcastShield (dir);
	}
	void Loop()
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
	void Movement()
	{
		int dir = isServer ? 1 : -1;

		if (Input.GetAxis ("Horizontal") >0) {
			isHoldingRB = true;
			isHoldingLB = false;
		} else if (Input.GetAxis ("Horizontal")<0) {
			isHoldingLB = true;
			isHoldingRB = false;
		}

		string AnimatorVar = LayerMask.LayerToName(gameObject.layer);
		if (isHoldingRB)
		{
			rb2d.velocity = new Vector2(MoveSpeed*dir, rb2d.velocity.y);
			transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			GetComponent<Animator>().SetBool(AnimatorVar+"_isRunning", true);
		}
		else if (isHoldingLB)
		{
			rb2d.velocity = new Vector2(-MoveSpeed*dir, rb2d.velocity.y);
			transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
			GetComponent<Animator>().SetBool(AnimatorVar+"_isRunning", true);
		}
		else
		{
			rb2d.velocity = new Vector2(0, 0);
			GetComponent<Animator>().SetBool(AnimatorVar+"_isRunning", false);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		gameObject.GetComponent<Health> ().TakeDamage (10);
	}

}