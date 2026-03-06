using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

public class ThirdTypeEnemy : Enemy
{
    [SerializeField]private GameObject deadPart;
    [SerializeField]private float speed;
    private Vector3 endPos;
    private Vector3 direct;
    public override void SetStartValue()
    {
        if(transform.localScale.x > 0)
        {
            endPos = EnemySpawnPoints.Instance.lLowDesPoint.position;
            direct = (EnemySpawnPoints.Instance.lLowDesPoint.position - EnemySpawnPoints.Instance.lLowStartPoint.position).normalized;
        }
        else{
            endPos = EnemySpawnPoints.Instance.rLowDesPoint.position;
            direct = (EnemySpawnPoints.Instance.rLowDesPoint.position - EnemySpawnPoints.Instance.rLowStartPoint.position).normalized;
        }
        RB.linearVelocity = direct * speed;
        StateMachine.Initialize(WalkState);
    }
    public override void Walk()
    {
        if(Vector3.Distance(endPos, transform.position) <= 0.01)
        {
            RB.linearVelocity = Vector2.zero;
            StateMachine.ChangeState(AttackState);
        }
    }
    public override void Attack()
    {
        base.Attack();
        
    }
}
