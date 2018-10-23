using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {
    public float delay;
    public GameObject connectTo;
    public GameObject source;
    WaveSource s;
	// Use this for initialization
	void Start () {
        s = null;
        if (source.GetComponent<WaveGenerator>() != null)
        {
            s = source.GetComponent<WaveGenerator>();
        }
        else if (source.GetComponent<AudioMixer>() != null)
        {
            s = source.GetComponent<AudioMixer>();
        }
        else if (source.GetComponent<HighPass>() != null)
        {
            s = source.GetComponent<HighPass>();
        }
        else if (source.GetComponent<LowPass>() != null)
        {
            s = source.GetComponent<LowPass>();
        }
        Invoke("run", delay);
	}
	void run()
    {
        AudioLink al = this.GetComponent<AudioLink>();
        al.MakeLink(connectTo, s, al);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
