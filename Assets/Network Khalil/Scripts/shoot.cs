using UnityEngine;
using UnityEngine.EventSystems;

public class shoot : EventTrigger
{
	GameObject player;

	void Update(){
		player = GameObject.FindGameObjectWithTag ("localPlayer");
	}

	public override void OnPointerDown(PointerEventData data)
	{
		player.GetComponent<WizardScriptNet>().shoot();
	}

}