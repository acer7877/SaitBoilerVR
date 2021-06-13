using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Welcome-stage, Intrudction button press
    public void GoIntruducing()
    {
        StageManager.instance.SetStage(StageManager.EnumStage.Intrudce);
    }

    //Waterfall operation steps button press
    public void GoOperationSteps()
    {
        throw new System.Exception("Unfinished function");
    }

    //open world button press
    public void GoOpenWorld()
    {
        throw new System.Exception("Unfinished function");
    }

}
