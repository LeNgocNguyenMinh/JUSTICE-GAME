using UnityEngine;
using System.Collections;

public class ThirdTypeEnemy : Enemy
{
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
        StateMachine.Initialize(WalkState);
    }

    public override void Walk()
    {
        RB.linearVelocity = direct * speed;
        if(Vector3.Distance(endPos, transform.position) <= 0.01)
        {
            StateMachine.ChangeState(AttackState);
        }
    }  
    public void FallBack()
    {
        //enemy go -direct in 0.2s then go direct
        RB.linearVelocity = Vector3.zero;
        RB.linearVelocity = -direct * speed;
        StartCoroutine(FallBackCoroutine());
    }
    private IEnumerator FallBackCoroutine()
    {
        yield return new WaitForSeconds(1f);
        RB.linearVelocity = direct * speed;
    }
}
