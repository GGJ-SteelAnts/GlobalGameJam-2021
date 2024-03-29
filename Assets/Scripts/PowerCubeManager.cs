﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerCubeManager : MonoBehaviour
{
    public enum PowerType {Nothing, Bigger, Faster, Jumper, Artefact, DubleJump, PushPull, Dash, Ladder};
    public PowerType powerType = PowerType.Nothing;
    public float powerTime = 5f;
    public float powerUnit = 10f;
    public string nextSceneName;
    private MeshRenderer meshRenderer;

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
        else if (powerType == PowerType.Artefact || powerType == PowerType.DubleJump || powerType == PowerType.PushPull || powerType == PowerType.Dash || powerType == PowerType.Ladder)
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            meshRenderer.materials[0].DisableKeyword("_EMISSION");
            meshRenderer.materials[1].SetFloat("_Outline", 0.0f);
            meshRenderer.materials[1].SetColor("_OutlineColor", new Color(0.5276349f, 0.5566038f, 0.118147f));
        }

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (powerType == PowerType.Artefact)
            {
                meshRenderer.materials[0].EnableKeyword("_EMISSION");
                meshRenderer.materials[0].SetColor("_EmissionColor", new Color(0.2735849f, 0.2018939f, 0.09162514f) * 0.8f);
                meshRenderer.materials[1].SetFloat("_Outline", 0.04f);
            }
            else if (powerType == PowerType.DubleJump || powerType == PowerType.PushPull || powerType == PowerType.Dash || powerType == PowerType.Ladder)
            {
                meshRenderer.materials[1].SetFloat("_Outline",0.6f);
                meshRenderer.materials[0].EnableKeyword("_EMISSION");
                meshRenderer.materials[0].SetColor("_EmissionColor", new Color(0.2735849f, 0.2018939f, 0.09162514f) * 0.8f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (powerType == PowerType.Artefact || powerType == PowerType.DubleJump || powerType == PowerType.PushPull || powerType == PowerType.Dash || powerType == PowerType.Ladder)
            {
                meshRenderer.materials[0].DisableKeyword("_EMISSION");
                meshRenderer.materials[1].SetFloat("_Outline", 0.0f);
            }
        }
    }
}
