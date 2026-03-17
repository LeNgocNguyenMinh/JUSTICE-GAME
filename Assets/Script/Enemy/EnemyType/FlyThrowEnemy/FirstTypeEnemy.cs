using UnityEngine;
using DG.Tweening;

public class FirstTypeEnemy : Enemy
{
    [SerializeField]private Transform throwPoint;
    [SerializeField]private GameObject projectilePrefab;
    [SerializeField]private float normalBulletSpeed;
    [SerializeField]private float parryBulletSpeed;
    [SerializeField]private Transform parryDesPos;
    [SerializeField]private float flySpeed;
    [SerializeField]private GameObject deadPart;
    [SerializeField]private GameEffect gameEffect;
    private Vector3 endPos;
    private Vector3 direct;
    public override void SetStartValue()
    {
        if(transform.localScale.x > 0)
        {
            endPos = EnemySpawnPoints.Instance.lHighDesPoint.position;
            direct = (EnemySpawnPoints.Instance.lHighDesPoint.position - EnemySpawnPoints.Instance.lHighStartPoint.position).normalized;
        }
        else{
            endPos = EnemySpawnPoints.Instance.rHighDesPoint.position;
            direct = (EnemySpawnPoints.Instance.rHighDesPoint.position - EnemySpawnPoints.Instance.rHighStartPoint.position).normalized;
        }
        RB.linearVelocity = direct * flySpeed;
        StateMachine.Initialize(WalkState);
    }
    public override void Walk()
    {
        if(Vector3.Distance(endPos, transform.position) <= 0.1)
        {
            RB.linearVelocity = Vector2.zero;
            StateMachine.ChangeState(AttackState);
        }
    }
    public override void Attack()
    {
        BulletCollider projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity).GetComponentInChildren<BulletCollider>();
        projectile.SetStartValue(normalBulletSpeed, parryBulletSpeed, PlayerController.Instance.GetDesPoint(), parryDesPos.position);
    }
}
