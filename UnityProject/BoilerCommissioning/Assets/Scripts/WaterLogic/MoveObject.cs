using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 from, to;
    public bool isArrived;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = from;
        isArrived = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isArrived)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(from, to, timer / OpenWorldMgr.instance.OpenWorldFrame);
        }
        isArrived = Vector3.Distance(to, transform.position) < 0.00001f;
    }
}
