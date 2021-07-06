using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using TMPro;

public class BTIndicator : BGameObject
{
    int Num;
    public TMP_Text m_screen;
    //For the object in hand, we need a component in the boiler system as target
    BTIndicator linked;
    protected override void Awake()
    {
        base.Awake();
        VRTKIO.isGrabbable = false;
        Num = 0;
        //m_screen = GameObject.Find("Number") as TextMeshPro;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        VRTKIO.InteractableObjectTouched += CheckPressureIdicator;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        VRTKIO.InteractableObjectTouched -= CheckPressureIdicator;
    }
    private void Start()
    {
        m_screen.SetText("0");
    }

    void CheckPressureIdicator(object sender, InteractableObjectEventArgs e)
    {
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie
            && ControllerManager.instance.GetRightHandMode() == "Glass")
        {//for now only do the check in steps and when right hand is magnifying glass
            ControllerManager.instance.SetLeftHandModel_tmp("Temp.Indicator");
            linked = ControllerManager.instance.LeftControllerList["Temp.Indicator"].GetComponent<BTIndicator>();
            linked.linked = this;
        }
    }

    public void setNum(int n)
    {
        Num = n;
        m_screen.text = n.ToString() + "PSI";
        if (linked != null)
            linked.setNum(n);
    }
}
