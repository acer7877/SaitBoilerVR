using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BCODector : BGameObject
{
    GameObject m_light;
    bool m_isWorking;

    protected override void Awake()
    {
        base.Awake();
        VRTKIO.isGrabbable = false;

        m_light = GameObject.Find("GreenLight");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        VRTKIO.InteractableObjectTouched += CheckDetector;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        VRTKIO.InteractableObjectTouched -= CheckDetector;
    }
    private void Start()
    {
        m_isWorking = false;
        if (m_light != null)
            m_light.SetActive(false);
    }

    void CheckDetector(object sender, InteractableObjectEventArgs e)
    {
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie)
        {
            m_isWorking = true;
            if (m_light != null)
                m_light.SetActive(true);

            ObjectManager.instance.Action(name, BChecker.eCheckAction.ECA_Object_on);
        }
    }

}
