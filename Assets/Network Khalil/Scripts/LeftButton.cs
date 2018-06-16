using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : EventTrigger
{
	GameObject player;

	void Update(){
		player = GameObject.FindGameObjectWithTag ("localPlayer");
	}

	public override void OnPointerDown(PointerEventData data)
	{
		WizardScriptNet.isHoldingLB = true;
	}

	public override void OnPointerUp(PointerEventData data)
	{
		WizardScriptNet.isHoldingLB = false;
	}

}