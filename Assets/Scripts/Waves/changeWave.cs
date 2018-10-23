using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FrequencyChangeEvent : UnityEvent<float, float> { }
public class HighPassChangeEvent : UnityEvent<float> { }
public class LowPassChangeEvent : UnityEvent<float> { }
public class EndMeEvent : UnityEvent { }
public class changeWave : MonoBehaviour  {
    //public FrequencyChangeEvent updateFreq = new FrequencyChangeEvent();
    public AudioSource source;
    public AudioHighPassFilter high;
    public AudioLowPassFilter low;
    // Use this for initialization
    public AudioLink origionalSpawn;
    public HighPass highPass;
    public LowPass lowPass;
	void Start () {
        //updateFreq.AddListener(UpdateFreq);
	}
    public void KillListen(WaveGenerator gen)
    {
        gen.killer.AddListener(end);
    }
    void end()
    {
        Destroy(this.gameObject);
    }
    public void listenFreq(AudioLink l)
    {
        l.updateFreq.AddListener(UpdateFreq);
    }
	public void listenHigh(HighPass l)
    {
        l.updateHigh.AddListener(UpdateHigh);
    }

    private void UpdateHigh(float cuttoff)
    {
        high = this.gameObject.GetComponent<AudioHighPassFilter>();
        if (high == null)
        {
            return;
        }
        high.cutoffFrequency = cuttoff;
    }

    public void listenLow(LowPass l)
    {
        l.updateLow.AddListener(UpdateLow);
    }

    private void UpdateLow(float cuttoff)
    {
        low = this.gameObject.GetComponent<AudioLowPassFilter>();
        if (low == null)
        {
            return;
        }
        low.cutoffFrequency = cuttoff;
    }

    public void UpdateFreq(float f, float v)
    {
        source = this.gameObject.GetComponent<AudioSource>();

        if (source == null)
        {
            //Debug.Log("No source");
            return;
        }
        //Debug.Log("Setting pitch to " + f + " from " + source.pitch);
        //Debug.Log(source.gameObject.name); 
        source.pitch = f;
        source.volume = v;
    }



}
