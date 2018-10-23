using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveGenerator : MonoBehaviour, WaveSource {

    public GameObject waveGO;
    public EndMeEvent killer;
    public WaveType type;
    private float _frequency;
    public float frequency()
    {
        return _frequency;
    }
    float time;
    public float freq(float t)
        {
            switch (type)
            {
                case WaveType.SINE:
                    return (Mathf.Sin(t * frequency()) )* (this.transform.position.y/2);
                case WaveType.SAW:
                    return (Mathf.Abs(((t * frequency()) % 2.0f)) - 1.0f) * (this.transform.position.y / 2);
                case WaveType.TRIANGLE:
                    return (Mathf.Abs(((t * frequency()) % 4.0f) - 2.0f) - 1.0f) * (this.transform.position.y / 2);
                case WaveType.SQUARE:
                    return ((t * frequency() / 2) % 1 > .5f ? 1f : -1f) * (this.transform.position.y / 2);
                default:
                    return -10.0f;
            }
        } 

    AudioLink link;

    Text t;
	// Use this for initialization
	void Start () {
        link = this.gameObject.GetComponent<AudioLink>();
        link.connecting.AddListener(Link);
        t = this.gameObject.GetComponentInChildren<Text>();
        killer = new EndMeEvent();
    }

    void Link(GameObject target, WaveSource s)
    {
        float dist = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
        _frequency = dist * 60;
    }
	// Update is called once per frame
	void Update () {
        float dist = 0;
        if(link.connected != null)
        {
            dist = Vector3.Distance(this.gameObject.transform.position, link.connected.transform.position);
            _frequency = dist/2;
        }
        time += Time.deltaTime;
        if (time > 1)
        {
            time = 0.0f;
        }
        if (t != null && !this.gameObject.GetComponent<AudioLink>().lockFreq)
            t.text = (dist * 60).ToString();
    }

    private void OnDestroy()
    {
        killer.Invoke();
    }
    /*
     * AudioSource noiseMaker = connectTo.AddComponent<AudioSource>();
                noiseMaker.clip = source[i];
                noiseMaker.Play();
                noiseMaker.loop = true;
                noiseMaker.dopplerLevel = 0;
                noiseMaker.spatialBlend = 1.0f;
                noiseMaker.pitch = Vector3.Distance(this.gameObject.transform.position, connectTo.transform.position);
                AS.Add(noiseMaker);
                */
}
