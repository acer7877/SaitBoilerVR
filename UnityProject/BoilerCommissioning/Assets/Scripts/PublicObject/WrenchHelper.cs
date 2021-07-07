using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    public BValve ValveLogic;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PipeWrench")
        {
            ValveLogic.autoSwitch();
        }
    }
}
