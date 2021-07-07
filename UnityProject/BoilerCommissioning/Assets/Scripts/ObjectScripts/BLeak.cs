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

    protected void Awake()
    {
        m_isOn = false;
        MeshRenderer m = GetComponent<MeshRenderer>();
        m.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PipeWrench" && m_isOn)
        {
            setOn(false);
            ObjectManager.instance.Action(name, BChecker.eCheckAction.ECA_Fix);
        }
    }

    public void setOn( bool isOn)
    {
        m_isOn = isOn;
        if (m_isOn)
            Effect.Play();
        else
            Effect.Stop();
    }

}
