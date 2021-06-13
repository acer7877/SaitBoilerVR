using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    //this is a sigleton
    private ControllerManager() { }
    public static ControllerManager instance;
    
    [SerializeField]
    public StringGO_Dictionary LeftControllerList;
    [SerializeField]
    public StringGO_Dictionary RightControllerList;

    private string LeftControllerCurrentModel, RightControllerCurrentModel;

    private void Awake()
    {
        instance = this;
    }

    //set Hand model
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

        foreach (string key in LeftControllerList.Keys)
        {
            RightControllerList[key].SetActive(key == Model);
        }

        RightControllerCurrentModel = Model;

    }
}
