using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float idleCount;
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        idleCount = enemy.IdleTime;
        enemy.Animator.SetTrigger("Idle");
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(idleCount <= 0)
        {
            stateMachine.ChangeState(enemy.AttackState);
        }
        else
        {
            idleCount -= Time.deltaTime;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        if(triggerType == Enemy.AnimationTriggerType.IdleAnimFinish)
        {
        }
    }
}
