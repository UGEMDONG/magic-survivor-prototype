using UnityEngine;

public abstract class ExpOrb : MonoBehaviour, IExpOrbMove
{
    protected Transform target;
    protected float moveSpeed = 5f;
    protected float[] expValue = { 10, 20, 30 };
    public float playerExp;
    [SerializeField] protected int orbLevel;

    // 구슬의 타겟을 설정하는 메서드
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // 구슬이 플레이어를 향해 이동하는 기능을 위한 메서드
    public void MoveToTarget()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        }
    }

    // 플레이어에게 경험치를 주는 기능을 위한 메서드
    public void GiveExpToPlayer()
    {
        playerExp += expValue[orbLevel - 1];
        Destroy(gameObject);
    }

    // 일정 시간 지나면 구슬이 사라지는 기능을 위한 메서드
    public void DistroyOrb()
    {
        Destroy(gameObject);
    }
}
