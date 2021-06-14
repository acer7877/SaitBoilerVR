using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //this is a sigleton
    private ObjectManager() { }
    public static ObjectManager instance;
    private void Awake()
    {
        instance = this;
    }

    Dictionary<string, GameObject> m_ObjectList;
    public void RegistGameObject(string name, GameObject GOTarget)
    {
        m_ObjectList[name] = GOTarget;
    }

    public void UnregistGameObject(string name)
    {
        m_ObjectList.Remove(name);
    }



}
