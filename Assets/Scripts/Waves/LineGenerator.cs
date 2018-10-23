using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour {

    WaveSource gen;
    LineRenderer line;
    AudioLink link;
    Material linemat;
    Material lineMatFreze;
    bool mat = true;
    public void setup(GameObject target, WaveSource source)
    {
        gen = source;

        if (line != null)
        {
            return;
        }
        line = this.gameObject.AddComponent<LineRenderer>();

        /*AnimationCurve ac = new AnimationCurve();
        for (float i = 0.0f; i <= 1; i += .1f)
        {
            ac.AddKey(i, gen.freq(i)+1);
        }
        line.widthCurve = ac;*/
        line.widthMultiplier = 0.05f;
        line.material = linemat;
    }
    private void Start()
    {
        link = this.gameObject.GetComponent<AudioLink>();
        linemat = GameObject.Find("GLOBAL").GetComponent<GLOBAL>().linemat;
        lineMatFreze = GameObject.Find("GLOBAL").GetComponent<GLOBAL>().lineMatFreze;

        link.connecting.AddListener(setup);
    }

    // Update is called once per frame
    void Update () {
        if (link.connected != null && line != null)
        {
            if (gen == null)
            {
                return;
            }
            line.SetPositions(new Vector3[] {
                this.gameObject.transform.position,
                link.connected.transform.position
            });

            int len = (int)(Vector3.Distance(this.gameObject.transform.position, link.connected.transform.position) * 100);
            Vector3[] points = new Vector3[len];
            Vector3 me = this.gameObject.transform.position;
            Vector3 target = link.connected.transform.position;
            for (int i = 0; i < len; i++)
            {
                points[i] = LerpPercent(me, target, (float)i / (float)len);
            }

            var t = Time.time;
            for (int i = 0; i < len; i++)
            {
                float y = points[i].y + ((gen.freq(((float)i / (float)100) + (Time.time * gen.frequency()))) / 4);
                y = float.IsNaN(y) ? 0 : y;
                points[i] = new Vector3(points[i].x, y, points[i].z);
            }
            line.positionCount = len;
            line.SetPositions(points);
        }
        if(link.connected == null && line != null)
        {
            Destroy(line);
            line = null;
        }

    }

    internal void toggleColor()
    {
        line.material = mat ? lineMatFreze : linemat;
        mat = !mat;
    }

    private void OnDestroy()
    {
        line.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        Destroy(line);
    }

    Vector3 LerpPercent(Vector3 end, Vector3 start, float percent)
    {
        return (start + percent * (end - start));
    }
}
