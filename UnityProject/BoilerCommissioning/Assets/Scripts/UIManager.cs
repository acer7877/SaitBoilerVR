using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //this is a sigleton
    private UIManager() { }
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }



    public GameObject UIRoot;

    //Welcome-stage, Intrudction button press
    public void GoIntruducing()
    {
        StageManager.instance.SetStage(StageManager.EnumStage.Intrudce);
    }

    //Waterfall operation steps button press
    public void GoOperationSteps()
    {
        StageManager.instance.SetStage(StageManager.EnumStage.Operatie);
    }

    //open world button press
    public void GoOpenWorld()
    {
        StageManager.instance.SetStage(StageManager.EnumStage.OpenWorld);
    }

    //Go next step in operation mode
    public void NextStep()
    {
        StepManager.instance.nextStep();
    }
}
