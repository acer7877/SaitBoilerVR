using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BCODector : BGameObject
{
    GameObject m_light;
    bool m_isWorking;
    private void Awake()
    {
        gameObject.AddComponent<VRTK_InteractableObject>();
        VRTK_InteractableObject v = GetComponent<VRTK_InteractableObject>();
        v.InteractableObjectTouched += CheckDetector;
        m_light = GameObject.Find("GreenLight");
    }

    private void Start()
    {
        m_isWorking = false;
        if (m_light != null)
            m_light.SetActive(false);
    }

    void CheckDetector(object sender, InteractableObjectEventArgs e)
    {
        m_isWorking = true;
        if (m_light != null)
            m_light.SetActive(true);

        ObjectManager.instance.Action(name, BChecker.eCheckAction.ECA_Object_on);
    }

}
