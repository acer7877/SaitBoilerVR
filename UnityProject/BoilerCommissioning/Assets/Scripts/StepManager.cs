using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class checker
{
    public string targetGO;
    public string action;
}
public class Step
{
    //public 
}

public class StepManager : MonoBehaviour
{
    //this is a sigleton
    private StepManager() { }
    public static StepManager instance;

    private void Awake()
    {
        instance = this;
    }
}
