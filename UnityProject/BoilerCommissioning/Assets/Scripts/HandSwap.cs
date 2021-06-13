using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandSwap : MonoBehaviour
{

    public VRTK_ControllerEvents HandEvents;
    public List<GameObject> Tools;
    int _index = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (HandEvents != null)
        {
            HandEvents.ButtonTwoPressed += ToggleHands;
        }
    }

    void OnDisable()
    {
        if (HandEvents != null)
        {
            HandEvents.ButtonTwoPressed -= ToggleHands;
        }
    }

    void ToggleHands(object sender, ControllerInteractionEventArgs e)
    {
        if (_index >= Tools.Count)
            _index = 0;
        ActivateObject(_index++);
    }

    private void Start()
    {
        ActivateObject(_index++);
    }

    void ActivateObject(int index)
    {
        for (int i = 0; i < Tools.Count; i++)
        {
            Tools[i].SetActive(index == i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public interface HandAction
{
    void Action1();
}

public class LightSaberAction : HandAction
{
    Vector3 unuseScale = Vector3.zero;
    Vector3 useScale = Vector3.one;
    public GameObject objectToScale;
    public float scaleSpeed = 1f;

    public void Action1()
    {

    }



    protected virtual void ForceScale(Vector3 targetScale)
    {
        if (objectToScale != null)
        {
            objectToScale.transform.localScale = targetScale;
        }
    }
}
