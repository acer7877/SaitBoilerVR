using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Project:      Sait Boil VR System
//Creater:      simaonwang7877@gmail.com
//Usage:        Managing stage as a FMS(Welcome - intruducing - operating)


public class StageManager : MonoBehaviour
{
    //this is a sigleton
    private StageManager() { }
    public static StageManager instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        m_CurStage = EnumStage.Pause;
        SetStage(EnumStage.Welcome);
    }

    //For different stage
    public enum EnumStage { Pause, Welcome, Intrudce, Operatie }
    EnumStage m_CurStage;
    public EnumStage CurrentStage
    {
        get { return m_CurStage; }
    }
    public void SetStage(EnumStage eTargetStage)
    {
        if (m_CurStage == eTargetStage)
            return;//nothing to change!

        m_CurStage = eTargetStage;

        GameObject WelcomeUI;

        switch (eTargetStage)
        {
            case EnumStage.Operatie:
                //controllers
                ControllerManager.instance.SetLeftHandModel("Notepad");
                ControllerManager.instance.SetRightHandModel("Hand");

                //init steps
                //NotepadManager.instance.SetNotepadContext("Stage_Operation");
                StepManager.instance.StartStepFromBeginning();

                //hide welcomeUI
                WelcomeUI = GameObject.Find("WelcomeUI");
                if (WelcomeUI != null) WelcomeUI.SetActive(false);
                break;
            case EnumStage.Intrudce:
                ControllerManager.instance.SetLeftHandModel("Notepad");
                ControllerManager.instance.SetRightHandModel("Hand");
                NotepadManager.instance.SetNotepadContext("Stage_Introduction");
                WelcomeUI = GameObject.Find("WelcomeUI");
                if (WelcomeUI != null) WelcomeUI.SetActive(false);
                break;
            default://including Welcome and unknow situation
                ControllerManager.instance.SetLeftHandModel("Hand");
                ControllerManager.instance.SetRightHandModel("Hand");
                WelcomeUI = GameObject.Find("WelcomeUI");
                if (WelcomeUI != null) WelcomeUI.SetActive(true);
                break;
        }
    }


}
