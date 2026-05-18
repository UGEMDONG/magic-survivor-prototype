using UnityEngine;

public class MagnetMovement : OrbMovement
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float magnetRadius = 5f;

     public override void MoveToTarget()
    {
        if (target == null) return;
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= magnetRadius)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}