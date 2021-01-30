using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCubeManager : MonoBehaviour
{
    public enum PowerType {Nothing, Bigger, Faster, Jumper};
    public PowerType powerType = PowerType.Nothing;
    public float powerTime = 5f;
    public float powerUnit = 10f;

    void Start()
    {
        if (powerType == PowerType.Bigger) {
            GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        } 
        else if (powerType == PowerType.Faster)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
        else if (powerType == PowerType.Jumper)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        }

    }

    void Update()
    {
        
    }
}
