using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowPass : MonoBehaviour, WaveSource {

    WaveSource generator;
    public float cuttoffFreq;
    public List<GameObject> players;
    Text t;
    public LowPassChangeEvent updateLow;

    // Use this for initialization
    void Start () {
        updateLow = new LowPassChangeEvent();
        cuttoffFreq = 0f;
        t = this.gameObject.GetComponentInChildren<Text>();
    }

    public float freq(float t)
    {
        
        if (generator.freq(t) < cuttoffFreq/500)
        {
            return generator.freq(t);
        }
        else
        {
            return cuttoffFreq/500;
        }
    }

    public float frequency()
    {
        return generator.frequency();
    }

    public void NewConnection(WaveSource gen, List<GameObject> c)
    {
        generator = gen;
        players = c;
        foreach (GameObject go in players)
        {
            AudioLowPassFilter filter;
            if (go.GetComponent<AudioLowPassFilter>() == null)
            {
                filter = go.AddComponent<AudioLowPassFilter>();
                filter.GetComponent<changeWave>().listenLow(this);
            }
            else
            {
                filter = go.GetComponent<AudioLowPassFilter>();
            }
            filter.cutoffFrequency = cuttoffFreq;
        }
    }

    // Update is called once per frame
    void Update () {
        if (t != null)
            t.text = this.cuttoffFreq.ToString();
        this.cuttoffFreq = this.gameObject.transform.position.y * 1000;
        this.updateLow.Invoke(cuttoffFreq);
    }
}
