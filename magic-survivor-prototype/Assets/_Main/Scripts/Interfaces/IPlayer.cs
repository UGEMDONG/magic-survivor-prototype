using UnityEngine;

public interface IPlayer : IDamageable
{
    public void Initialize();
    public Transform transform { get; }
}
