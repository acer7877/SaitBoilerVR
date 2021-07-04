using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using TMPro;

public class BIndicator : BGameObject
{
    int Num,TargetNum;
    public TMP_Text m_screen;

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
        VRTKIO.InteractableObjectTouched += CheckIndicator;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        VRTKIO.InteractableObjectTouched -= CheckIndicator;
    }
    private void Start()
    {
        m_screen.SetText("000");
    }

    void CheckIndicator(object sender, InteractableObjectEventArgs e)
    {
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie
            && ControllerManager.instance.GetRightHandMode() == "Glass")
        {//for now only do the check in steps and when right hand is magnifying glass
            ControllerManager.instance.SetLeftHandModel_tmp("Indicator");
            //ControllerManager.instance.SetLeftIndicatorTxt("---");

            BChecker bc = StepManager.instance.GetCheckerByNameFromCurStep(BGetName());
            if (bc != null && bc.arg > 0)
            {
                StarrCounting(bc.arg);
            }
            //ObjectManager.instance.Action(name, BChecker.eCheckAction.ECA_Object_on);
        }
    }

    //make the number change to a target on the indicator monitor
    void StarrCounting(int target)
    {
        Num = (int)((float)target * 0.8f);
        TargetNum = target;
    }
    private void Update()
    {
        if (TargetNum > 0)
        {
            if (Num < TargetNum)
            {
                ControllerManager.instance.SetLeftIndicatorTxt((++Num).ToString());
            }
            else
            {
                TargetNum = 0;
                ObjectManager.instance.Action(BGetName(), BChecker.eCheckAction.ECA_Indicator_Num);
            }
        }
    }


}
