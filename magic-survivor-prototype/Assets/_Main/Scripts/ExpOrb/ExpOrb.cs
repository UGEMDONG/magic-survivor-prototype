using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    [SerializeField] private MagnetMovement move;
    private IExpReceiver expReceiver;
    [SerializeField] private ExpOrbSpawner spawner;
    [SerializeField]private SpriteRenderer sr;

    private int expLevel = 1;
    [SerializeField] float[] expValues = { 10f, 20f, 30f };
    Color[] orbColors = { new Color(0.498f, 0.490f, 1f), new Color(0.945f, 1f, 0.490f), new Color(0.973f, 0.490f, 1f) };
    [SerializeField] GameObject player;

    public void MoveToTarget() => move.MoveToTarget();

    public void SetExpLevel(int level)
    {
        expLevel = level;
        sr.color = orbColors[expLevel - 1];
    }

    public void Start()
    {
        expReceiver = player.GetComponent<IExpReceiver>();
        move.SetTarget(player.transform);
    }

    public void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 10f)
        {
            MoveToTarget();
            if (Vector2.Distance(transform.position, player.transform.position) < 0.1f)
            {
                expReceiver.TakeExp(expValues[expLevel - 1]);
                spawner.ReturnToPool(gameObject);
            }
        }
    }
}