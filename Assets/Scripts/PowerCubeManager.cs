using System.Collections;
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
        if (powerType == PowerType.DubleJump)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.cyan;
        }
        if (powerType == PowerType.PushPull)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
        }
        if (powerType == PowerType.Dash)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
        if (powerType == PowerType.Ladder)
        {
            GetComponentInChildren<MeshRenderer>().material.color = Color.black;
        }
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
                meshRenderer.materials[1].SetFloat("_Outline", 0.4f);
            }
            else if (powerType == PowerType.DubleJump || powerType == PowerType.PushPull || powerType == PowerType.Dash || powerType == PowerType.Ladder)
            {
                meshRenderer.materials[1].SetFloat("_Outline", 0.01f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (powerType == PowerType.Artefact || powerType == PowerType.DubleJump || powerType == PowerType.PushPull || powerType == PowerType.Dash || powerType == PowerType.Ladder)
            {
                meshRenderer.materials[1].SetFloat("_Outline", 0.0f);
            }
        }
    }
}
