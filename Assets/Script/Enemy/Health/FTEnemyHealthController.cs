using UnityEngine;

public class FTEnemyHealthController : EnemyHealthController
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("EnemyATKCollider")) 
        {
            EnemyHurt();
            collider.GetComponentInChildren<BulletCollider>().DestroyBullet();
        }
    }
}
