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
        SetStage(EnumStage.Welcome);
    }

    //For different stage
    public enum EnumStage { Welcome, Intrudce, Operatie }
    public void SetStage(EnumStage eTargetStage)
    {
        switch (eTargetStage)
        {
            case EnumStage.Operatie:
                break;
            case EnumStage.Intrudce:
                ControllerManager.instance.SetLeftHandModel("Notepad");
                ControllerManager.instance.SetRightHandModel("Hand");
                SetNotepadContext("Intruducing Mode", "√ Point Anything with right hand to see the indtrduction.<sprite=0>");

                break;
            default://including Welcome and unknow situation
                ControllerManager.instance.SetLeftHandModel("Hand");
                ControllerManager.instance.SetRightHandModel("Hand");
                break;
        }
    }


    //set text to notepad on the left hand
    public void SetNotepadContext(string Title, string Context)
    {
        GameObject NP_Title = GameObject.Find("Title_np");
        NP_Title.GetComponent<TMP_Text>().text = Title;

        GameObject NP_Context = GameObject.Find("Context_np");
        NP_Context.GetComponent<TMP_Text>().text = Context;

    }
}
