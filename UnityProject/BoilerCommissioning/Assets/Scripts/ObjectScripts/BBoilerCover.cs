using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBoilerCover : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Index")
        {
            SoundManager.instance.Play("di");
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Collider>().isTrigger = false;

        }
    }
}
