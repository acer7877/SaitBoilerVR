using System.Collections;
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

        m_isOn = false;
        //valve registed at valve-body, it's a BGameObject
        //ObjectManager.instance.RegistGameObject(getParentName(), gameObject.transform.parent.gameObject);
    }

    protected void OnEnable()
    {
        rotator.MaxLimitReached += valveOffByPlayer;
        rotator.MinLimitReached += valveOnByPlayer;

    }

    protected void OnDisable()
    {
        rotator.MaxLimitReached -= valveOffByPlayer;
        rotator.MinLimitReached -= valveOnByPlayer;
    }


    void valveOnByPlayer(object sender, ControllableEventArgs e)
    {
        m_isOn = true;
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie)
        {
            ObjectManager.instance.Action(getParentName(), BChecker.eCheckAction.ECA_Valve_on);
        }

        if (StageManager.instance.CurrentStage == StageManager.EnumStage.OpenWorld)
        {
            OpenWorldMgr.instance.OperateValve(getParentName(), m_isOn);
        }
    }

    void valveOffByPlayer(object sender, ControllableEventArgs e)
    {
        m_isOn = false;
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie)
        {
            ObjectManager.instance.Action(getParentName(), BChecker.eCheckAction.ECA_Valve_off);
        }

        if (StageManager.instance.CurrentStage == StageManager.EnumStage.OpenWorld)
        {
            OpenWorldMgr.instance.OperateValve(getParentName(), m_isOn);
        }
    }


    string getParentName()
    {
        GameObject parentGameObject = transform.parent.gameObject;
        parentGameObject = parentGameObject.transform.parent.gameObject;
        //Debug.LogError(">>>>>>>>>>" + parentGameObject.name);
        return parentGameObject.name;
    }

    public bool isOn()
    { return m_isOn; }

    public void autoSwitch()
    {
        //if (autoMoveTarget > -998f)//if switching, return
        //    return;

        if (m_isOn)
        {
            rotator.SetAngleTarget(rotator.angleLimits.maximum);
            valveOffByPlayer(null, new ControllableEventArgs());
        }
        else
        {
            rotator.SetAngleTarget(rotator.angleLimits.minimum);
            valveOnByPlayer(null, new ControllableEventArgs());
        }

        //rotator.SetAngleTargetWithStepValue(20);
    }

}
