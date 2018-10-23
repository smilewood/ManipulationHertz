using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.Pointer3D;

public class WaveCreation : MonoBehaviour {
    public string Wave;

    public GameObject sin;
    public GameObject saw;
    public GameObject tri;
    public GameObject square;
    

    public GameObject highfilter;
    public GameObject lowfilter;
    public GameObject mixer;
    public GameObject output;
    public GameObject timer;

    public GameObject camera;
    public GameObject rhand;
    public GameObject lhand;

    private bool mixerCheck;

    private void Start()
    {
        mixerCheck = true;
    }

    // Update is called once per frame
    void Update () {
        bool check  = ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Pad);
        float check2 = ViveInput.GetTriggerValue(HandRole.RightHand);

        if (check && check2 < 0.5)
        {
            Vector2 vector = ViveInput.GetPadPressAxisEx(HandRole.RightHand);
            Vector3 position = rhand.transform.position;
            Quaternion rotation = rhand.transform.rotation;
            rotation = camera.transform.rotation;
            Vector3 foward = rhand.transform.forward;
            foward = foward;
            position += foward;


            if (vector != Vector2.zero)
            {
                //Debug.Log(vector);
                if (vector.y > 0.5)
                {
                    Debug.Log("Sin");
                    GameObject cube = Instantiate(sin, position, rotation);
                    cube.GetComponentInChildren<faceCamera>().camera = camera;
                }
                if (vector.y < -0.5)
                {
                    Debug.Log("Saw");
                    GameObject cube = Instantiate(saw, position, rotation);
                    cube.GetComponentInChildren<faceCamera>().camera = camera;
                }
                if (vector.x > 0.5)
                {
                    Debug.Log("Square");
                    GameObject cube = Instantiate(square, position, rotation);
                    cube.GetComponentInChildren<faceCamera>().camera = camera;
                }
                if (vector.x < -0.5)
                {
                    Debug.Log("Triangle");
                    GameObject cube = Instantiate(tri, position, rotation);
                    cube.GetComponentInChildren<faceCamera>().camera = camera;
                }

            }


        }


        bool check3 = ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Pad);
        float check4 = ViveInput.GetTriggerValue(HandRole.LeftHand);

        if (check3 && check4 < 0.5)
        {
            Vector2 vector = ViveInput.GetPadPressAxisEx(HandRole.LeftHand);
            Vector3 position = lhand.transform.position;
            Quaternion rotation = lhand.transform.rotation;
            rotation = camera.transform.rotation;
            Vector3 foward = lhand.transform.forward;
            foward = foward * (float)1.5;
            position += foward;


            if (vector != Vector2.zero)
            {
                Debug.Log(vector);
                if (vector.y > 0.5)
                {
                    if (mixerCheck)
                    {
                        Debug.Log("High");
                        GameObject cube = Instantiate(highfilter, position, rotation);
                        cube.GetComponentInChildren<faceCamera>().camera = camera;
                        mixerCheck = false;
                    }
                    else
                    {
                        Debug.Log("Low");
                        GameObject cube = Instantiate(lowfilter, position, rotation);
                        cube.GetComponentInChildren<faceCamera>().camera = camera;
                        mixerCheck = true;
                    }
                   
                }
                if (vector.y < -0.5)
                {
                    Debug.Log("Time");
                    GameObject cube = Instantiate(timer, position, rotation);   
                }
                if (vector.x > 0.5)
                {
                    Debug.Log("Mixer");
                    GameObject cube = Instantiate(mixer, position, rotation);
                }
                if (vector.x < -0.5)
                {
                    Debug.Log("Output");
                    GameObject cube = Instantiate(output, position, rotation);
                }

            }

        }

    }
}
