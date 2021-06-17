﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables;
using VRTK.Controllables.ArtificialBased;
using static VRTK.Controllables.VRTK_BaseControllable;

public class BValve : MonoBehaviour
{
    public GameObject Hinge;
    VRTK_ArtificialRotator rotator;
    bool m_isOn;

    protected void Awake()
    {
        //base.Awake();
        //VRTKIO.isGrabbable = false;

        if (Hinge == null)
            Hinge = GameObject.Find("Hinge");
        rotator = GetComponent<VRTK_ArtificialRotator>();
        //rotator = BAddComponent<VRTK_ArtificialRotator>(gameObject);
        //rotator.operateAxis = OperatingAxis.zAxis;
        //rotator.angleLimits.minimum = 0;
        //rotator.angleLimits.maximum = 90;
        //rotator.hingePoint = Hinge.transform;
        //rotator.minMaxThresholdAngle = 5;

        m_isOn = false;
    }

    protected void OnEnable()
    {
        rotator.MaxLimitReached += valveOnByPlayer;
        rotator.MinLimitReached += valveOffByPlayer;

    }

    protected void OnDisable()
    {
        rotator.MaxLimitReached -= valveOnByPlayer;
        rotator.MinLimitReached -= valveOffByPlayer;
    }


    void valveOnByPlayer(object sender, ControllableEventArgs e)
    {
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie)
        {
            m_isOn = true;
            ObjectManager.instance.Action(name, BChecker.eCheckAction.ECA_Valve_on);
        }
    }

    void valveOffByPlayer(object sender, ControllableEventArgs e)
    {
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie)
        {
            m_isOn = false;
            ObjectManager.instance.Action(name, BChecker.eCheckAction.ECA_Valve_off);
        }
    }
}
