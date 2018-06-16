using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : EventTrigger
{
	GameObject player;

	void Update(){
		player = GameObject.FindGameObjectWithTag ("localPlayer");
	}

	public override void OnPointerDown(PointerEventData data)
	{
		WizardScriptNet.isHoldingRB = true;
	}

	public override void OnPointerUp(PointerEventData data)
	{
		WizardScriptNet.isHoldingRB = false;
	}
		
}