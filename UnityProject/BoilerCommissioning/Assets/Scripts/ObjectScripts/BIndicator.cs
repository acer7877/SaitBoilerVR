using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using TMPro;

public class BIndicator : BGameObject
{
    int Num;
    public TMP_Text m_screen;

    protected override void Awake()
    {
        base.Awake();
        VRTKIO.isGrabbable = false;
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
        {
            ControllerManager.instance.SetLeftHandModel_tmp("Indicator");
            ControllerManager.instance.SetLeftIndicatorTxt("111'F");
            //ObjectManager.instance.Action(name, BChecker.eCheckAction.ECA_Object_on);
        }
    }

}
