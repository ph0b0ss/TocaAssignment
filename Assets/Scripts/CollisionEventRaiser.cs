using TocaAssignment;
using UnityEngine;

public class CollisionEventRaiser : MonoBehaviour
{
    public GameEvent onCollision;
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("NPC"))
        {
            onCollision.Raise();
        }
    }
}