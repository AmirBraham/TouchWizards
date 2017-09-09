using UnityEngine;
using UnityEngine.EventSystems;

public class Castshield : EventTrigger
{
	GameObject player;

	void Update(){
		player = GameObject.FindGameObjectWithTag ("localPlayer");
	}

	public override void OnPointerDown(PointerEventData data)
	{
		player.GetComponent<WizardScriptNet>().castShield();
	}

}