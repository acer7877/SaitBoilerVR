using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPump : BGameObject
{

    public bool IsOn;
    public AudioSource audio;
    protected override void Awake()
    {
        base.Awake();
        VRTKIO.isGrabbable = false;
        IsOn = true;
        audio = GetComponent<AudioSource>();
    }


    public Vector3 shakeRate = new Vector3(0.0003f, 0.0003f, 0.003f);
    public float shakeTime = 0.5f;
    public float shakeDertaTime = 0.1f;
    public void Shake()
    {
        StartCoroutine(Shake_Coroutine());
    }

    public IEnumerator Shake_Coroutine()
    {
        var oriPosition = gameObject.transform.position;
        for (float i = 0; i < shakeTime; i += shakeDertaTime)
        {
            gameObject.transform.position = oriPosition +
                Random.Range(-shakeRate.x, shakeRate.x) * Vector3.right +
                Random.Range(-shakeRate.y, shakeRate.y) * Vector3.up +
                Random.Range(-shakeRate.z, shakeRate.z) * Vector3.forward;
            yield return new WaitForSeconds(shakeDertaTime);
        }
        gameObject.transform.position = oriPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsOn)
        {
            Shake();
            if (!audio.isPlaying)
                audio.Play();
        }
        else
            audio.Stop();
    }


}
