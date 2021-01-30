using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public enum ObjectType {Nothing, PushPull, Drag};
    public ObjectType objectType = ObjectType.Nothing;
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;
    private PlayerManager playerManager;
    private bool interact = false;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.materials[1].SetFloat ("_Outline", 0.0f);
        meshRenderer.materials[1].SetColor("_OutlineColor", new Color(0.5276349f, 0.5566038f, 0.118147f));
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interact)
        {
            if (objectType == ObjectType.PushPull)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    if (playerManager.GetPushPullObject() == null && playerManager.activeAbility.Count > 0 && playerManager.activeAbility[0] == 2)
                    {
                        playerManager.SetPushPullObject(this.gameObject);
                        meshRenderer.materials[1].SetColor("_OutlineColor", new Color(0.5568628f, 0.3785397f, 0.1176471f));
                    }
                    else
                    {
                        playerManager.RemovePushPullObject();
                        meshRenderer.materials[1].SetColor("_OutlineColor", new Color(0.5276349f, 0.5566038f, 0.118147f));
                        playerManager = null;
                    }
                }
            }
        } 
        else
        {
            if (playerManager != null && playerManager.GetPushPullObject() == null)
            {
                meshRenderer.materials[1].SetColor("_OutlineColor", new Color(0.5276349f, 0.5566038f, 0.118147f));
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (playerManager != null)
                {
                    playerManager.RemovePushPullObject();
                    playerManager = null;
                }
            }
        } 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 hit = collision.contacts[0].normal;
            if (hit.x != 0 && hit.y == 0)
            {
                playerManager = collision.gameObject.GetComponent<PlayerManager>();
                if (playerManager.activeAbility.Count > 0 && playerManager.activeAbility[0] == 2)
                {
                    meshRenderer.materials[1].SetFloat("_Outline", 0.1f);
                } 
                else
                {
                    meshRenderer.materials[1].SetFloat("_Outline", 0.0f);
                }
                interact = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            meshRenderer.materials[1].SetFloat("_Outline", 0.0f);
            interact = false;
        }
    }
}
