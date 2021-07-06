using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using TMPro;
using System;

public class BClock : BGameObject
{
    protected bool IsTime;
    public TMP_Text m_screen;
    //For the object in hand, we need a component in the boiler system as target
    BClock linked;

    protected override void Awake()
    {
        base.Awake();
        VRTKIO.isGrabbable = false;
        IsTime = true;
        //m_screen = GameObject.Find("Number") as TextMeshPro;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        VRTKIO.InteractableObjectTouched += CheckClock;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        VRTKIO.InteractableObjectTouched -= CheckClock;
    }
    private void Start()
    {
        m_screen.SetText("0");
    }

    void CheckClock(object sender, InteractableObjectEventArgs e)
    {
        if (StageManager.instance.CurrentStage != StageManager.EnumStage.Intrudce
            && ControllerManager.instance.GetRightHandMode() == "Glass")
        {//for now only do the check in steps and when right hand is magnifying glass
            ControllerManager.instance.SetLeftHandModel_tmp("Clock");
            linked = ControllerManager.instance.LeftControllerList["Clock"].GetComponent<BClock>();
            linked.linked = this;

            BChecker bc = StepManager.instance.GetCheckerByNameFromCurStep(BGetName());
            if (bc != null && bc.arg > 0)
            {
                linked.IsTime = IsTime;
            }
        }
    }

    private void Update()
    {
        string output;
        if(IsTime)
            output = DateTime.Now.ToLongTimeString().ToString();
        else
            output = DateTime.Now.ToShortDateString().ToString();
        m_screen.text = output;
    }

    public void ChangeMode()
    {
        IsTime = !IsTime;
    }

}
