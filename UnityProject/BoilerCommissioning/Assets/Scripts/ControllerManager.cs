using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
using TMPro;

public class ControllerManager : MonoBehaviour
{
    //this is a sigleton
    private ControllerManager() { }
    public static ControllerManager instance;

    public VRTK_ControllerEvents LeftEvent;
    public VRTK_ControllerEvents RightEvent;

    [SerializeField]
    public StringGO_Dictionary LeftControllerList;
    [SerializeField]
    public StringGO_Dictionary RightControllerList;

    private string LeftControllerCurrentModel, RightControllerCurrentModel;
    private string LCM_tmp, RCM_tmp;

    private void Awake()
    {
        instance = this;
        initButtonSetup();
        LCM_tmp = "";
        RCM_tmp = "";
    }

    //set Hand model
    public void SetLeftHandModel_tmp(string Model = "")
    {
        if (Model == "")
        {
            if (LCM_tmp != "")
                SetLeftHandModel(LCM_tmp);
        }
        else
        {
            if(LeftControllerCurrentModel == "Hand" || LeftControllerCurrentModel=="Notepad")
            LCM_tmp = LeftControllerCurrentModel;
            SetLeftHandModel(Model);
        }
    }
    public void SetRightHandModel_tmp(string Model = "")
    {
        if (Model == "")
        {
            if (RCM_tmp != "")
                SetRightHandModel(RCM_tmp);
        }
        else
        {
            RCM_tmp = RightControllerCurrentModel;
            SetRightHandModel(Model);
        }
    }
    public void SetLeftHandModel(string Model)
    {
        //Already setted
        if (LeftControllerCurrentModel == Model)
            return;

        if (!LeftControllerList.ContainsKey(Model))
            return;

        foreach(string key in LeftControllerList.Keys)
        {
            LeftControllerList[key].SetActive(key == Model);
        }

        LeftControllerCurrentModel = Model;

    }

    public void SetRightHandModel(string Model)
    {
        //Already setted
        if (RightControllerCurrentModel == Model)
            return;

        if (!RightControllerList.ContainsKey(Model))
            return;

        foreach (string key in RightControllerList.Keys)
        {
            RightControllerList[key].SetActive(key == Model);
        }

        RightControllerCurrentModel = Model;

    }


    void initButtonSetup()
    {
        //Reset scene
        LeftEvent.StartMenuPressed += ResetSence;
        //Show Hint
        LeftEvent.ButtonOnePressed += CreateHint;
        LeftEvent.ButtonOneReleased += DeleteHint;

        LeftEvent.ButtonTwoPressed += ShowRightHand_MagnifyGlass;
        LeftEvent.ButtonTwoReleased += ShowRightHand_Hand;
        //Right hand change
        //RightEvent.TriggerPressed += ShowRightHand_MagnifyGlass;
        //RightEvent.TriggerReleased += ShowRightHand_Hand;

    }
    private void ResetSence(object sender, ControllerInteractionEventArgs e)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SoundManager.instance.Play("resetScene");

    }

    private void CreateHint(object sender, ControllerInteractionEventArgs e)
    {
        StepManager.instance.CreateHint();

        //for test
        //OpenWorldMgr.instance.OperateValve("BV-13", true);
        //BoilerLogic.Helper.DrawLine(new Vector3(0, 0, 0), new Vector3(10, 10, 10));
    }

    private void DeleteHint(object sender, ControllerInteractionEventArgs e)
    {
        StepManager.instance.DeleteHint();
    }

    public string GetRightHandMode()
    {
        return RightControllerCurrentModel;
    }
    private void ShowRightHand_MagnifyGlass(object sender, ControllerInteractionEventArgs e)
    {
        if (LCM_tmp != "")//everytime go to glass model, reset left hand
            SetLeftHandModel_tmp();
        SetRightHandModel("Glass");
    }
    private void ShowRightHand_Hand(object sender, ControllerInteractionEventArgs e)
    {
        //if(RightControllerCurrentModel == "Glass")
        //{//if close glass on right, reset left hand model
        //    SetLeftHandModel_tmp();
        //}
        SetRightHandModel("Hand");
    }


    public void SetLeftIndicatorTxt(string txt)
    {
        LeftControllerList["Temp.Indicator"].GetComponentInChildren<TMP_Text>().text = txt;
    }
}
