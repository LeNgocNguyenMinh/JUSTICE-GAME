using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [field: SerializeField]public int MaxHit { get; set; }
    [field: SerializeField]private GameObject DeadPart { get; set; }
    [field: SerializeField]private GameObject MainBody { get; set; }
    public int CurrentHit { get; set; }
    public bool IsDead { get; set; }
    private void Start()
    {
        CurrentHit = 0;
        IsDead = false;
    }
    public void EnemyHurt()
    {
        CurrentHit++;
        SoundControl.Instance.HitSoundPlay();
        if(CurrentHit >= MaxHit && !IsDead)
        {
            IsDead = true;
            EnemyDead();
        }
    }
    public void EnemyDead()
    {
        GameObject tmp = Instantiate(DeadPart, transform.position, Quaternion.identity);
        if(transform.localScale.x < 0)
        {
            Vector3 scale = tmp.transform.localScale;
            scale.x *= -1;
            tmp.transform.localScale = scale;
        }
        tmp.GetComponent<DeadEffect>().OnDead();
        Destroy(MainBody);
    }
    
}
