using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BCabinet : BGameObject
{
    //protected virtual void OnEnable()
    //{
    //    base.OnEnable();
    //    BoxCollider c = GetComponent<BoxCollider>();
    //}

    //Everything put into this Cabinet will disappare
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(">>>>>" + other.name);
        if(other.name == "Bleach" || other.name == "PaintCan" || other.name == "AerosolCan (1)")
        {
            GameObject target = other.gameObject;
            target.SetActive(false);
            ObjectManager.instance.Action(target.name, BChecker.eCheckAction.ECA_Object_put_away);
        }    
    }
}
