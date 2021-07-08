using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables;
using VRTK.Controllables.ArtificialBased;
using static VRTK.Controllables.VRTK_BaseControllable;

public class BLeak : MonoBehaviour
{
    bool m_isOn;
    public ParticleSystem Effect;
    public AudioSource Sound;

    protected void Awake()
    {
        setOn(false);
        MeshRenderer m = GetComponent<MeshRenderer>();
        m.enabled = false;
        Sound.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PipeWrench" && m_isOn)
        {
            setOn(false);
            ObjectManager.instance.Action(name, BChecker.eCheckAction.ECA_Fix);
        }
    }

    public void setOn(bool isOn)
    {
        m_isOn = isOn;
        if (m_isOn)
        {
            Effect.Play();
            Sound.Play();
        }
        else
        {
            Effect.Stop();
            Sound.Stop();
        }
    }

}
