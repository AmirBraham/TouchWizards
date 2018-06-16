using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour {
	public GameObject GameOverText;
	public const int maxHealth = 100;
	GameObject canvas;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth ;
	[SyncVar(hook = "GameStateChange")]
	public bool gameOn ;

	Slider healthBar;

	void Start(){
		gameOn = true;
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
			gameOn = false;
		}
	}

	void OnChangeHealth (int health)
	{
		healthBar.value = health;
	}
	void GameStateChange(bool state)
	{
		if(state)
			return;
		GameObject.FindGameObjectWithTag ("NetworkManager").GetComponent<UIManager>().showGameOver();
		GameOverText = GameObject.Find("UI").transform.Find( "GameOver" ).transform.Find("GameOverText").gameObject;
		if(GameOverText.GetComponent<Text>()!=null){
			GameOverText.GetComponent<Text>().text = !isLocalPlayer  ? "You Win!" : "You Lose!";
		}
	}
}
