using UnityEngine;

public class NPCSetup : MonoBehaviour
{
    public Material[] materialOptions;
    
    void Start()
    {
        int materialChoice = Random.Range(0, materialOptions.Length);
        foreach (var smr in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            smr.material = materialOptions[materialChoice];
        }

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f))
        {
            transform.position = hit.point;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * 100f, Color.red, 100f);
            Debug.LogWarning("Could not find a place to start");
        }
        
        GameObject.Find("GameRunner").GetComponent<GameRunner>().RegisterNPC(GetComponent<NPC>());
    }
}
