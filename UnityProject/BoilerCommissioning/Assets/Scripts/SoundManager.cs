using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //this is a sigleton
    private SoundManager() { }
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
        InitAudioClips();
    }



    public AudioSource BAudioSource;
    public Dictionary<string, AudioClip> m_audioClips;//这里我要给主角添加跳跃的音效

    private void InitAudioClips()
    {
        if (BAudioSource == null)
            BAudioSource = gameObject.AddComponent<AudioSource>();
        BAudioSource.playOnAwake = false;
        m_audioClips = new Dictionary<string, AudioClip>();
        m_audioClips.Add("check", Resources.Load<AudioClip>("sound/ding"));
        m_audioClips.Add("step", Resources.Load<AudioClip>("sound/correct"));
        m_audioClips.Add("finish", Resources.Load<AudioClip>("sound/victory"));
        m_audioClips.Add("dingdon", Resources.Load<AudioClip>("sound/doorbell"));
        m_audioClips.Add("di", Resources.Load<AudioClip>("sound/di"));
        m_audioClips.Add("valve", Resources.Load<AudioClip>("sound/ValveSound"));
    }

    public void Play(string target)
    {
        if(!m_audioClips.ContainsKey(target))
        {
            Debug.LogError("Bad sound target! <" + target + ">");
            return;
        }

        BAudioSource.clip = m_audioClips[target];
        BAudioSource.Play();
    }
}
