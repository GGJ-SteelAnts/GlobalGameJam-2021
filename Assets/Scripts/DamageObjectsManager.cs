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
    public Transform fromPosition;
    public Transform toPosition;
    private bool back = false;
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
                if (!animation.isPlaying)
                {
                    animation.Play("SawA"); 
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

        if (objectType == ObjectType.RidingSaw)
        {
            if (fromPosition != null && toPosition != null)
            {
                if (this.transform.localPosition == fromPosition.localPosition)
                {
                    back = true;
                } 
                else if (this.transform.localPosition == toPosition.localPosition)
                {
                    back = false;
                }

                if (back)
                {
                    this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, toPosition.localPosition, 5 * Time.deltaTime);
                } 
                else
                {
                    this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, fromPosition.localPosition, 5 * Time.deltaTime);
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
