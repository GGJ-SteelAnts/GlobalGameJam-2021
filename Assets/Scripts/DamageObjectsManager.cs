using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjectsManager : MonoBehaviour
{
    public enum ObjectType { Object, HideSpikes, ShowSpikes, StaticSaw, RidingSaw };
    public ObjectType objectType = ObjectType.Object;
    private Animation animation;
    private bool animate = false;

    public bool instaKill = false;
    public float damage = 10f;
    public float damageRate = 5f;
    private float actualDamageRate = 0f;
    private bool interact = false;
    private PlayerManager playerManager;

    void Start()
    {
        animation = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interact)
        {
            if (objectType == ObjectType.HideSpikes)
            {
                if (!animate)
                {
                    animation["Spikes"].speed = 1f;
                    animation["Spikes"].normalizedTime = 0f;
                    animation.Play("Spikes");
                    animate = true;
                }
            }

            if (objectType == ObjectType.ShowSpikes)
            {
                if (actualDamageRate < Time.time)
                {
                    actualDamageRate = Time.time + damageRate;
                    playerManager.damage(damage, instaKill);
                }
            }

            if (objectType == ObjectType.StaticSaw)
            {
                Debug.Log(!animation.isPlaying);
                if (!animation.isPlaying)
                {
                    animation.Play("Saw");
                }
                if (actualDamageRate < Time.time)
                {
                    actualDamageRate = Time.time + damageRate;
                    playerManager.damage(damage, instaKill);
                }
            }

            if (objectType == ObjectType.RidingSaw)
            {
                if (actualDamageRate < Time.time)
                {
                    actualDamageRate = Time.time + damageRate;
                    playerManager.damage(damage, instaKill);
                }
            }
        }
    }

    public void damagePlayer(PlayerManager playerManager)
    {
        if (actualDamageRate < Time.time)
        {
            actualDamageRate = Time.time + damageRate;
            playerManager.damage(damage, instaKill);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interact = true;
            playerManager = other.gameObject.GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interact = false;
            playerManager = null;
        }
    }

    public void SpikesBack()
    {
        animate = false;
        animation["Spikes"].speed = -1f;
        animation.Play("Spikes");
    }
}
