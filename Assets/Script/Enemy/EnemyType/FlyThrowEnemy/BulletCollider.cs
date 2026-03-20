using UnityEngine;
using DG.Tweening;

public class BulletCollider : EnemyParry
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Transform bulletRotate;
    [SerializeField]private float rotateSpeed;
    [SerializeField]private GameObject mainBulletObject;
    private float parryFlySpeed;
    private Vector3 parryDesPos;
    private Vector3 direct;
    public void SetStartValue(float normalFlySpeed, float parryFlySpeed, Vector3 attackDesPos, Vector3 parryDesPos)
    {
        this.parryFlySpeed = parryFlySpeed;
        this.parryDesPos = parryDesPos;
        direct = (attackDesPos - transform.position).normalized;
        rb.linearVelocity = direct * normalFlySpeed;
        bulletRotate.DORotate(new Vector3(0, 0, 360), rotateSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
    public void DestroyBullet()
    {
        Destroy(mainBulletObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("LLSPoint") || collider.CompareTag("RLSPoint") || collider.CompareTag("GroundCollider"))
        {
            Debug.Log("Destroy");
            DestroyBullet();
        }
    }
    public override void Parry()
    {
        direct = (parryDesPos - transform.position).normalized;
        rb.linearVelocity = direct * parryFlySpeed;
    }   
    public override void EnemyATKHitPlayer()
    {
        DestroyBullet();
    }
}
