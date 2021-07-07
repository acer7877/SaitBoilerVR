using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBoiler : BGameObject
{
    public GameObject DarkLight;
    public GameObject RedLight;
    public GameObject GreenLight;

    public GameObject Flame;

    private void Awake()
    {
        base.Awake();
        Flame.SetActive(false);
        setStateOff();
    }

    public void setStateOff()
    {
        DarkLight.SetActive(true);
        RedLight.SetActive(false);
        GreenLight.SetActive(false);
    }
    public void setStateNotWorking()
    {
        DarkLight.SetActive(false);
        RedLight.SetActive(true);
        GreenLight.SetActive(false);
    }
    public void setStateWorking()
    {
        DarkLight.SetActive(false);
        RedLight.SetActive(false);
        GreenLight.SetActive(true);
        Flame.SetActive(true);
    }

    //turn it on with the swith on the wall
    public void pressBtn()
    {
        setStateNotWorking();
        ObjectManager.instance.Action(name, BChecker.eCheckAction.ECA_Swith_boiler_on);
    }
}
