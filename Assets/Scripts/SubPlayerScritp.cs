using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlayerScritp : MonoBehaviour
{
    private PlayerManager playerManager;

    void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }

    void Update()
    {
        
    }

    public void StartEatPowerCube()
    {
        playerManager.StartEatPowerCube();
    }

    public void EndEatPowerCube()
    {
        playerManager.EndEatPowerCube();
    }

    public void Die()
    {
        playerManager.Die();
    }
}
