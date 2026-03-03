using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField]public Rigidbody2D RB { get; set; }
    [field: SerializeField]public EnemyHealthController HealthController { get; set; }
    [field: Header(" Enemy Setting")]
    [field: SerializeField]public float IdleTime { get; set; }
    [field: SerializeField]public int AttackNum { get; set; }
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyDieState DieState { get; set; }
    public EnemyWalkState WalkState { get; set; } 
    public EnemyAfterPlayerDeadState AfterPlayerDeadState { get; set; }
    [field: SerializeField]public Animator Animator { get; set; }
    public bool EnemyInPlayerDeadState;
    private void Awake()
    {
        EnemyInPlayerDeadState = false;
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        DieState = new EnemyDieState(this, StateMachine);
        WalkState = new EnemyWalkState(this, StateMachine);
        AfterPlayerDeadState = new EnemyAfterPlayerDeadState(this, StateMachine);
    }
    public virtual void SetStartValue(){}
    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }
    public enum AnimationTriggerType
    {
        WalkAnimFinish,
        DeadAnimFinish,
        AttackAnimFinish,
        IdleAnimFinish,
        AttackTrigger,
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }
    public virtual void Attack(){}
    public virtual void Walk(){}
}
