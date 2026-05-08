using UnityEngine;

public abstract class Player : MonoBehaviour, IPlayer
{    
    public Vector3 GetPosition => transform.position;

    public abstract void Initialize();

    #region Interface
    #region IDamageable
    public abstract void SetDie();
    public abstract void TakeDamage(float damage);
    #endregion
    #endregion   
    #region General
    public abstract void GetEXP(float exp);
    protected abstract void OnLevelUp();
    protected abstract void Move();
    #endregion

    #region Unity LifeCycles
    protected abstract void OnAwake();
    protected abstract void OnEnabled();
    protected abstract void OnDisabled();
    protected abstract void OnStart();
    protected abstract void OnUpdate();

    void Awake() => OnAwake();
    void OnEnable() => OnEnabled();
    void OnDisable() => OnDisabled();
    void Start() => OnStart();
    void Update() => OnUpdate();  
    #endregion
}
