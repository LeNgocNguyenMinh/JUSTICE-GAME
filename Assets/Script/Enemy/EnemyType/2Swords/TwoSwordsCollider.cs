using UnityEngine;

public class TwoSwordsCollider : EnemyParry
{
    [SerializeField]private ThirdTypeEnemy thirdTypeEnemy;
    [SerializeField]private EnemyHealthController enemyHealthController;
    public void SetStartValue()
    {
    }
    public override void Parry()
    {
        enemyHealthController.EnemyHurt();
        thirdTypeEnemy.FallBack();
    }  
    public override void EnemyATKHitPlayer()
    {
        thirdTypeEnemy.FallBack();
    }
}
