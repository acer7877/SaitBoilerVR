using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BGrabable : BGameObject
{
    protected override void Awake()
    {
        base.Awake();
        VRTKIO.isGrabbable = true;
        HideIdle();

    }

    public void HideIdle()
    {
        string IdleOneName = name + "_Idle";
        GameObject Target = GameObject.Find(IdleOneName);
        if (Target == null) 
            return;
        Target.transform.SetGlobalScale(new Vector3(0, 0, 0));
    }
    public void ShowIdle()
    {
        string IdleOneName = name + "_Idle";
        GameObject Target = GameObject.Find(IdleOneName);
        if (Target == null)
            return;
        Target.transform.SetGlobalScale(new Vector3(1, 1, 1));
    }

}
