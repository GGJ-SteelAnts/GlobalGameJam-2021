using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDamageObjectScript : MonoBehaviour
{
    private DamageObjectsManager damageObjectManager;

    void Start()
    {
        damageObjectManager = GetComponentInParent<DamageObjectsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (damageObjectManager.objectType == DamageObjectsManager.ObjectType.HideSpikes)
        {
            if (other.gameObject.tag == "Player")
            {
                damageObjectManager.damagePlayer(other.gameObject.GetComponent<PlayerManager>());
            }
        }
    }

}
