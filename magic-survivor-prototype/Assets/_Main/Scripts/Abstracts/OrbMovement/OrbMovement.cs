using UnityEngine;

public abstract class OrbMovement : MonoBehaviour
{
    protected Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    public abstract void MoveToTarget();
}
