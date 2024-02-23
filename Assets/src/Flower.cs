using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{

    int randomRotation;
    void Start()
    {
        randomRotation = Random.Range(-5, 5);
    }
    void Update()
    {
        // float loudness = detector.GetLoudnessFromMicrophone() * loudnessSenibility;
        // if (loudness > 0.5)
        // {
        //     GetComponent<Rigidbody>().useGravity = true;
        // }

        // if i grab the object stop the rotation
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f)
        {
            randomRotation = 0;
        }
        else
            transform.Rotate(0, randomRotation * Time.deltaTime, 0);
        // Rotation on y axis
        // be sure to capitalize Rotate or you'll get errors
    }

}
