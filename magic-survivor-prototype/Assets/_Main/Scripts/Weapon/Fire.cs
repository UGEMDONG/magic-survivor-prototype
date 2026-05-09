using UnityEngine;

public class Fire : AttackObjectBase
{
    protected override void Start()
    {
        base.Start();
        lifeTime = 1f;
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
