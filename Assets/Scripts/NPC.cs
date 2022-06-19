using TocaAssignment;
using UnityEditor;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private ParticleSystem pickupParticles;
    public GameEvent pickupGameEvent;

    void Awake()
    {
        pickupParticles = GetComponentInChildren<ParticleSystem>();
    }
    public void SetParticlesActive(bool isActive)
    {
        pickupParticles.gameObject.SetActive(isActive);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!GameObject.Find("GameRunner").GetComponent<GameRunner>().isOnAssignment)
            {
                GameObject.Find("GameRunner").GetComponent<GameRunner>().SetLocationForNPC(this);
                GameObject.Find("GameRunner").GetComponent<GameRunner>().isOnAssignment = true;
                GameObject.Find("GameRunner").GetComponent<GameRunner>().SetAllNPCParticles(false);
                Destroy(gameObject);
                pickupGameEvent.Raise();
            }
        }
    }
}
