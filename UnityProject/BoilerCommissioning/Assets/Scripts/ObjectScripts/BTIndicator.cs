using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using TMPro;

public class BTIndicator : BGameObject
{
    int Num, TargetNum;
    public TMP_Text m_screen;
    //For the object in hand, we need a component in the boiler system as target
    BTIndicator linked;
    protected override void Awake()
    {
        base.Awake();
        VRTKIO.isGrabbable = false;
        Num = 0;
        TargetNum = 0;
        //m_screen = GameObject.Find("Number") as TextMeshPro;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        VRTKIO.InteractableObjectTouched += CheckTmpIdicator;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        VRTKIO.InteractableObjectTouched -= CheckTmpIdicator;
    }
    private void Start()
    {
        m_screen.SetText("0");
    }

    void CheckTmpIdicator(object sender, InteractableObjectEventArgs e)
    {
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie
            && ControllerManager.instance.GetRightHandMode() == "Glass")
        {//for now only do the check in steps and when right hand is magnifying glass
            ControllerManager.instance.SetLeftHandModel_tmp("Temp.Indicator");
            linked = ControllerManager.instance.LeftControllerList["Temp.Indicator"].GetComponent<BTIndicator>();
            //linked.linked = this;

            BChecker bc = StepManager.instance.GetCheckerByNameFromCurStep(BGetName());
            if (bc != null && bc.arg > 0)
            {
                StarrCounting(bc.arg);
            }
        }
    }

    public void setNum(int n)
    {
        Num = n;
        m_screen.text = n.ToString() + "°F";
        if (linked != null)
            linked.Num = n;
    }

    public void setTNum(int n)
    {
        TargetNum = n;
        //m_screen.text = n.ToString() + "°F";
        if (linked != null)
            linked.TargetNum=n;
    }



    void StarrCounting(int target)
    {
        setNum((int)((float)target * 0.8f));
        setTNum(target);
    }
    private void Update()
    {
        if (TargetNum > 0)
        {
            if (Num < TargetNum)
            {
                //ControllerManager.instance.SetLeftIndicatorTxt((++Num).ToString());
                linked.setNum(++Num);
            }
            if (Num >= TargetNum)
            {
                setTNum(0);
                ObjectManager.instance.Action(BGetName(), BChecker.eCheckAction.ECA_Indicator_Num);
            }
        }
    }

}
