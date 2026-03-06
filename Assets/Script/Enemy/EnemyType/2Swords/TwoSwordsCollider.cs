using UnityEngine;

public class TwoSwordsCollider : EnemyParry
{
    [SerializeField]private SecondTypeEnemy secondTypeEnemy;
    [SerializeField]private EnemyHealthController enemyHealthController;
    public void SetStartValue()
    {
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            PlayerHealthController.Instance.PlayerHurt();
        }
        if(collider.CompareTag("RightCollider") && secondTypeEnemy.transform.localScale.x > 0)
        {
            secondTypeEnemy.ReachDes();
        }
        if(collider.CompareTag("LeftCollider") && secondTypeEnemy.transform.localScale.x < 0)
        {
            secondTypeEnemy.ReachDes();
        }
    }
    public override void Parry()
    {
        enemyHealthController.EnemyHurt();
    }  

}
