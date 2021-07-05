using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BComBtn : MonoBehaviour
{
    public List<GameObject> InteractWith;
    
    [Serializable]
    public sealed class ComBtnEvent : UnityEvent<object> { }
    public ComBtnEvent OnClick = new ComBtnEvent();

    //float lastTriggerTime;
    private void OnTriggerEnter(Collider other)
    {
        if (InteractWith.Contains(other.gameObject))
        {
            OnClick.Invoke(other);
        }
    }
}
