using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class HandleTouch : MonoBehaviour, IPointerUpHandler// required interface when using the OnPointerUp method.
{
    string mHandleTouchParent;

    string WizardName;
    public void setHandleTouchParent ( string b  ) {
        mHandleTouchParent = b;
    }
 public void setName (string wizardName) {
    WizardName = wizardName;
 }  
public string getName() {
    return WizardName;
}

    public string getHandleTouchParent () {
        return mHandleTouchParent;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        
         Debug.Log("The mouse click was released by " + mHandleTouchParent);
        }
    }
