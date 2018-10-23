using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioMixer : MonoBehaviour, WaveSource {
    public MuteThisEvent mute;
    List<WaveSource> generators;
    public List<GameObject> players;
    public bool on = true;
    public void Start()
    {
        mute = new MuteThisEvent();
        generators = new List<WaveSource>();
        players = new List<GameObject>();
        toggle(true);
    }

    public float freq(float t)
    {
        if (!on)
            return 0f;
        float sum = 0;
        foreach (WaveSource source in generators)
        {
            if (source == null)
                continue;
            sum += source.freq(t);
        }
        return sum / (float)generators.Count;
    }

    public void toggle(bool on)
    {
        if (this.on != on)
        {
            mute.Invoke(!on);
            this.on = on;
        }
        
    }

    public float frequency()
    {
        if (!on)
        {
            return 0f;
        }
        float sum = 0.0f;
        foreach (WaveSource s in generators)
        {
            sum += s.frequency();
        }
        return sum / generators.Count;
    }

    public void NewConnection(WaveSource gen, List<GameObject> sources)
    {
        generators.Add(gen);
        foreach (GameObject g in sources)
        {
            players.Add(g);
            g.transform.parent = this.transform;
            g.transform.position = Vector3.zero;
            g.GetComponent<RememberTheTime>().link(this);
        }
          
    }

    public void RemoveSource(WaveSource gen)
    {
        this.generators.Remove(gen);
    }
}
