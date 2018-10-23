using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioLinkPassForwordEvent : UnityEvent<AudioLink> { }
public class AudioLinkConnectEvent<GameObject, WaveSource> : UnityEvent<GameObject, WaveSource> { }
public class AudioLink : MonoBehaviour {
    public AudioLinkConnectEvent<GameObject, WaveSource> connecting = new AudioLinkConnectEvent<GameObject, WaveSource>();
    public WaveSource gen;
    public GameObject genGO;
    private List<GameObject> players;
    public GameObject connected;
    private changeWave callback;
    public FrequencyChangeEvent updateFreq;
    public bool lockFreq = false;
    public AudioLinkPassForwordEvent updateLink;
    LineRenderer lr;
    // Use this for initialization
    void Start () {
        players = new List<GameObject>();
        updateFreq = new FrequencyChangeEvent();
        updateLink = new AudioLinkPassForwordEvent();
        updateLink.AddListener(UpdateLink);
    }

    public void toggleLock()
    {
        lockFreq = !lockFreq;
        this.GetComponent<LineGenerator>().toggleColor();
    }

    float dist;

    // Update is called once per frame
    void Update () {
        if (connected == null)
            return;
        if (this.gameObject.GetComponent<WaveGenerator>() != null)
        {
            if (lockFreq == false)
                dist = Vector3.Distance(this.gameObject.transform.position, connected.transform.position);
            float vol = this.gameObject.transform.position.y*2;
            //Debug.Log("callback with " + dist);
            updateFreq.Invoke(dist, vol);
        }
        
    }

    public void UpdateLink(AudioLink origin)
    {
        if (connected == null || this == origin)
            return;
        this.MakeLink(connected, gen, origin);
    }

    public void MakeLink(GameObject connectTo, WaveSource waveSource, AudioLink origin)
    {
        if (this.gameObject.GetComponent<WaveGenerator>() != null)
        {
            gen = this.gameObject.GetComponent<WaveGenerator>();
            genGO = this.gameObject;
            //Debug.Log("Adding from WaveGenerator\n" + this.gameObject.GetComponent<WaveGenerator>().waveGO);
            
            GameObject waveform = Instantiate(this.gameObject.GetComponent<WaveGenerator>().waveGO, this.transform);
            waveform.GetComponent<AudioPathTrack>().addStep(this.gameObject);
            waveform.GetComponent<changeWave>().KillListen(this.gameObject.GetComponent<WaveGenerator>());
            waveform.GetComponent<changeWave>().origionalSpawn = this;
            //Debug.Log(waveform);
            players.Add(waveform);

            
        }
        else if (this.gameObject.GetComponent<AudioMixer>() != null)
        {
           // Debug.Log("Adding from AudioMixer\n" + this.gameObject.GetComponent<AudioMixer>().players);
            gen = this.gameObject.GetComponent<AudioMixer>();
            genGO = this.gameObject;
            players = this.gameObject.GetComponent<AudioMixer>().players;
            foreach (GameObject pl in players)
            {
                pl.GetComponent<AudioPathTrack>().addStep(genGO);
            }
        }
        else if (this.gameObject.GetComponent<HighPass>() != null)
        {
            gen = this.gameObject.GetComponent<HighPass>();
            genGO = this.gameObject;

            players = this.gameObject.GetComponent<HighPass>().players;
            foreach (GameObject pl in players)
            {
                pl.GetComponent<AudioPathTrack>().addStep(genGO);
            }
        }
        else if (this.gameObject.GetComponent<LowPass>() != null)
        {
            gen = this.gameObject.GetComponent<LowPass>();
            genGO = this.gameObject;

            players = this.gameObject.GetComponent<LowPass>().players;
            foreach (GameObject pl in players)
            {
                pl.GetComponent<AudioPathTrack>().addStep(genGO);
            }
        }

        connected = connectTo;
        if (connected.GetComponent<AudioOut>() != null)
        {
            //Debug.Log("Players has " + players.Count + " GameObjects");
            for (int i = 0; i < players.Count; ++i)
            {
                players[i].GetComponent<changeWave>().listenFreq(players[i].GetComponent<changeWave>().origionalSpawn);
                players[i].GetComponent<AudioSource>().Play();
                
            }
        }
        else if (connected.GetComponent<AudioMixer>() != null)
        {

            //Debug.Log("Players has " + players.Count + " GameObjects");
            AudioMixer dest = connected.GetComponent<AudioMixer>();
            dest.NewConnection(gen, players);
        }
        else if (connected.GetComponent<HighPass>())
        {
            connected.GetComponent<HighPass>().NewConnection(gen, players);
        }
        else if (connected.GetComponent<LowPass>())
        {
            connected.GetComponent<LowPass>().NewConnection(gen, players);
        }
        foreach (GameObject p in players)
        {
            p.transform.parent = connectTo.transform;
            p.transform.localPosition = Vector3.zero;
        }
        if(connected.GetComponent<AudioLink>() != null)
            connected.GetComponent<AudioLink>().updateLink.Invoke(origin);
        connecting.Invoke(connectTo, waveSource);
        dist = Vector3.Distance(this.gameObject.transform.position, connected.transform.position);

    }

    private void OnDestroy()
    {
        if (connected.GetComponent<AudioMixer>() != null)
        {
            connected.GetComponent<AudioMixer>().RemoveSource(gen);
        }
        foreach (GameObject pl in this.players)
        {
            if (pl == null || pl.GetComponent<AudioPathTrack>() == null)
            {
                continue;
            }
            GameObject newParent = pl.GetComponent<AudioPathTrack>().cutPath(genGO);
            if (newParent == null)
                continue;
            pl.transform.parent = newParent.transform;
            pl.transform.localPosition = Vector3.zero;
            pl.GetComponent<AudioSource>().Pause();
        }
        
    }
}
