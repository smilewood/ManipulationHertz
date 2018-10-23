using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HTC.UnityPlugin.Vive;
using UnityEngine.EventSystems;

public class Linker : MonoBehaviour
{
   // public string Wave;

    public GameObject source;
    public GameObject destination;
    public Color normalC;
    private bool haptic;
    LineRenderer lineR, lineL;
    float htimer = 0f;
    // Update is called once per frame

    private void Start()
    {
        lineR = GameObject.Find("GLOBAL").GetComponent<GLOBAL>().showLineR;
        lineL = GameObject.Find("GLOBAL").GetComponent<GLOBAL>().showLineL;

        normalC = lineR.material.color;
    }
    void Update()
    {
        //ViveInput.GetPressDownEx(HandRole.RightHand, ControllerButton.Grip)

        //this: start from
        //connectTo: destination
        //s: Wavesource
        //this.GetComponent<AudioLink>().MakeLink(connectTo, s);
        
    }
    public virtual void OnBeginDrag(GameObject begin)
    {
        Debug.Log("Begin Link on " + begin.name);
        if (begin.GetComponent<AudioLink>() != null || begin.GetComponent<TimeConnection>() != null)
        {
            source = begin;
        }
        
        lineR.material.color = Color.red;
        lineL.material.color = Color.red;

    }

    public virtual void OnEndDrag(GameObject end)
    {
        if (source == null)
            return;
        Debug.Log("end drag on " + end.name);
        if (end.GetComponent<Timing>() != null || end.GetComponent<WaveGenerator>() != null)
        {
            source = null;
            return;
        }
        if(source.GetComponent<TimeConnection>() != null && end.GetComponent<AudioMixer>() != null)
        {
            source.GetComponent<TimeConnection>().connect(end.GetComponent<AudioMixer>());
            return;
        }

        AudioLink al = source.GetComponent<AudioLink>();
        WaveSource s = null;
        if (source.GetComponent<WaveGenerator>() != null)
        {
            s = source.GetComponent<WaveGenerator>();
        }
        else if (source.GetComponent<AudioMixer>() != null)
        {
            Debug.Log("AudioMixer");
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
        al.MakeLink(end, s, al);
        lineL.material.color = normalC;
        lineR.material.color = normalC;

        source = null;
        return;


    }

    public void OnDrag(PointerEventData eventData)
    {
        //Draw a line from the object the drag started with to here. 
    }
}
