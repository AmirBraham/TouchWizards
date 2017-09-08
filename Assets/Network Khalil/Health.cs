using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour {
	public GameObject GameOver;
	public const int maxHealth = 100;
	GameObject canvas;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth ;

	Slider healthBar;

	void Start(){
		healthBar = GameObject.FindGameObjectWithTag (LayerMask.LayerToName (gameObject.layer) + "HealthBar").GetComponent<Slider>();
		canvas = GameObject.Find ("UI");
		currentHealth = maxHealth;
		healthBar.value = currentHealth;
	}
	public void TakeDamage(int amount)
	{
		if (!isServer)
			return;

		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Debug.Log("Dead!");
			RpcShowGameOver ();
		}
	}

	void OnChangeHealth (int health)
	{
		healthBar.value = health;
	}
	[ClientRpc]
	void RpcShowGameOver()
	{
		if (isLocalPlayer)
		{
			Destroy(GameObject.Find ("GameOn"));
			GameObject gameover = Instantiate(GameOver,GameOver.transform);
			gameover.transform.SetParent(canvas.transform,false);
			if (GameObject.Find("GameOverText") != null)
			{
				GameObject.Find("GameOverText").GetComponent<Text>().text = currentHealth > 0 ? "You Win!" : "You Lose!";
			}
		}
	}
}