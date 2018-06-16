using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupPlayer : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentsToDisable;
    public RuntimeAnimatorController RedWizardAnim;
	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            foreach(Behaviour beh in componentsToDisable)
            {
                //disable the 2nd Camera and Audio listener
                beh.enabled = false;
            }

            //Make the second player Red
            GetComponent<Animator>().runtimeAnimatorController = RedWizardAnim;
        }
    
        
	}
}
