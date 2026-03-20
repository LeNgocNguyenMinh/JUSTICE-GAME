using UnityEngine;

public class LongStickCollider : EnemyParry
{
    [SerializeField]private SecondTypeEnemy secondTypeEnemy;
    [SerializeField]private EnemyHealthController enemyHealthController;
    public void SetStartValue()
    {
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("RLSPoint") && secondTypeEnemy.transform.localScale.x > 0)
        {
            secondTypeEnemy.ReachDes();
        }
        if(collider.CompareTag("LLSPoint") && secondTypeEnemy.transform.localScale.x < 0)
        {
            secondTypeEnemy.ReachDes();
        }
    }
    public override void Parry()
    {
        enemyHealthController.EnemyHurt();
    }  
    public override void EnemyATKHitPlayer()
    {
    }
}
