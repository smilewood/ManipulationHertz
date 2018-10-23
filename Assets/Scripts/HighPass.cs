using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighPass : MonoBehaviour, WaveSource
{

    WaveSource generator;
    public float cuttoffFreq;
    public List<GameObject> players;
    Text t;
    public HighPassChangeEvent updateHigh;


    // Use this for initialization
    void Start()
    {
        updateHigh = new HighPassChangeEvent();
        cuttoffFreq = 0f;
        t = this.gameObject.GetComponentInChildren<Text>();
    }

    public float freq(float t)
    {

        if (generator.freq(t) > cuttoffFreq/500)
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
            AudioHighPassFilter filter;
            if (go.GetComponent<AudioHighPassFilter>() == null)
            {
                filter = go.AddComponent<AudioHighPassFilter>();
                go.GetComponent<changeWave>().listenHigh(this);
            }
            else
            {
                filter = go.GetComponent<AudioHighPassFilter>();
            }
            filter.cutoffFrequency = cuttoffFreq;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (t != null)
            t.text = this.cuttoffFreq.ToString();
        this.cuttoffFreq = this.gameObject.transform.position.y * 1000;
        this.updateHigh.Invoke(cuttoffFreq);
    }
}