using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using TMPro;

public class BAquaStat : BGameObject
{
    int Num,TargetNum;
    public TMP_Text m_screen;
    //For the object in hand, we need a component in the boiler system as target
    BAquaStat linked;

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
        VRTKIO.InteractableObjectTouched += CheckAquaStat;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        VRTKIO.InteractableObjectTouched -= CheckAquaStat;
    }
    private void Start()
    {
        m_screen.SetText("0");
    }

    void CheckAquaStat(object sender, InteractableObjectEventArgs e)
    {
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie
            && ControllerManager.instance.GetRightHandMode() == "Glass")
        {//for now only do the check in steps and when right hand is magnifying glass
            ControllerManager.instance.SetLeftHandModel_tmp("AquaStat");
            linked = ControllerManager.instance.LeftControllerList["AquaStat"].GetComponent<BAquaStat>();
            linked.linked = this;

            BChecker bc = StepManager.instance.GetCheckerByNameFromCurStep(BGetName());
            if (bc != null && bc.arg > 0)
            {
                TargetNum = bc.arg;
                linked.TargetNum = TargetNum;
            }
        }
    }

    private void Update()
    {
        if (TargetNum > 0)
        {
            if (Num == TargetNum)
            {
                ObjectManager.instance.Action(BGetName(), BChecker.eCheckAction.ECA_AquaStat_Num);
                TargetNum = 0;
            }
        }
    }

    public void NumAdd(int num = 10)
    {
        NumAddLinked(num);
        linked.NumAddLinked(num);
    }

    public void NumAddLinked(int num = 10)
    {
        Num += num;
        m_screen.text = Num.ToString();
        if (TargetNum != 0 && Num == TargetNum)
        {
            ObjectManager.instance.Action(BGetName(), BChecker.eCheckAction.ECA_AquaStat_Num);
            TargetNum = 0;
        }
    }

}
