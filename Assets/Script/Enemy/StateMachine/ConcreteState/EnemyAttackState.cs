using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.Animator.SetTrigger("Attack");
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        {
            if(triggerType == Enemy.AnimationTriggerType.AttackTrigger)
            {
                enemy.Attack();
            }
            if(triggerType == Enemy.AnimationTriggerType.AttackAnimFinish)
            {
                stateMachine.ChangeState(enemy.IdleState);
            }
        }
    }
}
