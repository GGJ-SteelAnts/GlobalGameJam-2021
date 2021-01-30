using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjectsManager : MonoBehaviour
{
    public enum ObjectType { Object, HideSpikes, ShowSpikes };
    public ObjectType objectType = ObjectType.Object;
    private Animation animation;
    private bool animate = false;

    public bool instaKill = false;
    public float damage = 10f;
    public float damageRate = 5f;
    private float actualDamageRate = 0f;

    void Start()
    {
        animation = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (objectType == ObjectType.HideSpikes)
        {
            if (other.gameObject.tag == "Player" && !animate)
            {
                Debug.Log(other.tag);
                animation["Spikes"].speed = 1f;
                animation["Spikes"].normalizedTime = 0f;
                animation.Play("Spikes");
                animate = true;
            }
        }

        if (objectType == ObjectType.ShowSpikes)
        {
            if (other.gameObject.tag == "Player")
            {
                if (actualDamageRate < Time.time)
                {
                    actualDamageRate = Time.time + damageRate;
                    other.gameObject.GetComponent<PlayerManager>().damage(damage, instaKill);
                }
            }
        }
    }

    public void SpikesBack()
    {
        animate = false;
        animation["Spikes"].speed = -1f;
        animation.Play("Spikes");
    }
}
