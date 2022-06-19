using System;
using System.Collections;
using System.Collections.Generic;
using TocaAssignment;
using UnityEngine;

public class Location : MonoBehaviour
{
    private ParticleSystem pickupParticles;
    public GameEvent onNPCDelivery;
    
    void Start()
    {
        GameObject.Find("GameRunner").GetComponent<GameRunner>().RegisterLocation(this);
        pickupParticles = GetComponentInChildren<ParticleSystem>();
        pickupParticles.gameObject.SetActive(false);
    }

    public void SetActiveLocation(bool isActive)
    {
        pickupParticles.gameObject.SetActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (GameObject.Find("GameRunner").GetComponent<GameRunner>().isOnAssignment)
            {
                if (GameObject.Find("GameRunner").GetComponent<GameRunner>().targetDestination == this)
                {
                    GameObject.Find("GameRunner").GetComponent<GameRunner>().isOnAssignment = false;
                    pickupParticles.gameObject.SetActive(false);
                    GameObject.Find("GameRunner").GetComponent<GameRunner>().SetAllNPCParticles(true);
                    onNPCDelivery.Raise();
                }
            }
        }
    }
}
