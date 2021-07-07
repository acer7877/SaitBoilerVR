using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //this is a sigleton
    private ObjectManager() { }
    public static ObjectManager instance;

    public GameObject PipeInFloor;
    private void Awake()
    {
        instance = this;
        PipeInFloor.SetActive(false);
    }

    Dictionary<string, GameObject> m_ObjectList;
    public void RegistGameObject(string name, GameObject GOTarget)
    {
        if (m_ObjectList == null)
            m_ObjectList = new Dictionary<string, GameObject>();
        m_ObjectList[name] = GOTarget;
    }

    public void UnregistGameObject(string name)
    {
        m_ObjectList.Remove(name);
    }

    //When some interactions hanppen, call this function to change the data and do some logic
    public void Action(string actorName, BChecker.eCheckAction actionType)
    {
        StepManager.instance.Action(actorName, actionType);
    }

    public GameObject getGOByName(string name)
    {
        return m_ObjectList.ContainsKey(name) ? m_ObjectList[name] : null;
    }
}
