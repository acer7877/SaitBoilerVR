using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

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

    private void Awake()
    {
        instance = this;
        initButtonSetup();
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

        foreach (string key in RightControllerList.Keys)
        {
            RightControllerList[key].SetActive(key == Model);
        }

        RightControllerCurrentModel = Model;

    }


    void initButtonSetup()
    {
        LeftEvent.StartMenuPressed += ResetSence;
    }
    private void ResetSence(object sender, ControllerInteractionEventArgs e)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SoundManager.instance.Play("resetScene");

    }
}
