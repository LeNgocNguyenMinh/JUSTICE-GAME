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
            endPos = EnemySpawnPoints.Instance.lLDPoint.position;
            direct = (EnemySpawnPoints.Instance.lLDPoint.position - EnemySpawnPoints.Instance.lLSPoint.position).normalized;
        }
        else{
            endPos = EnemySpawnPoints.Instance.rLDPoint.position;
            direct = (EnemySpawnPoints.Instance.rLDPoint.position - EnemySpawnPoints.Instance.rLSPoint.position).normalized;
        }
        RB.linearVelocity = direct * speed;
        StateMachine.Initialize(WalkState);
    }
    public override void Walk()
    {
        float distance = Vector3.Distance(endPos, transform.position);
        bool passedDestination = false;
        if (transform.localScale.x > 0) 
        {
            passedDestination = transform.position.x >= endPos.x;
        }
            
        else 
        {
            passedDestination = transform.position.x <= endPos.x;
        }
        if (distance <= 0.1f || passedDestination) 
        {
            transform.position = endPos; // Khớp vị trí chính xác vào đích
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
