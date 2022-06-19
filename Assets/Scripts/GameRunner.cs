using System;
using System.Collections;
using System.Collections.Generic;
using TocaAssignment;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameRunner : MonoBehaviour
{
    private List<NPC> NPCs = new List<NPC>();
    private List<Location> locations = new List<Location>();

    public bool isOnAssignment = false;
    public Location targetDestination;
    public GameEvent gameStartEvent;

    public void Start()
    {
        gameStartEvent.Raise();
    }

    public void RegisterNPC(NPC npc)
    {
        NPCs.Add(npc);
        npc.SetParticlesActive(true);
    }

    public void RegisterLocation(Location location)
    {
        locations.Add(location);
    }

    public void SetLocationForNPC(NPC npc)
    {
        targetDestination = locations[Random.Range(0, locations.Count)];
        targetDestination.SetActiveLocation(true);
    }

    public void SetAllNPCParticles(bool particlesOn)
    {
        foreach (var npc in NPCs)
        {
            if(npc != null && npc.gameObject != null)
            {
                npc.SetParticlesActive(particlesOn);
            }
        }
    }
}