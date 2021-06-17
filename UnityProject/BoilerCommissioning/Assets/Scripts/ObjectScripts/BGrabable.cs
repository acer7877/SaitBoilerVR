using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGrabable : BGameObject
{
    protected override void Awake()
    {
        base.Awake();
        VRTKIO.isGrabbable = true;
    }
}
