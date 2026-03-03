using UnityEngine;
using System.Collections;

public class PlayerSword : MonoBehaviour
{
    [SerializeField]private PlayerController playerController;
    [SerializeField]private GameObject parryParticle;
    [SerializeField]private Transform parryParticlePos;
    private bool canParry = false;
    private float currentparryTime = 0f;
    private EnemyParry currentEnemyParry;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("EnemyATKCollider"))
        {
            canParry = true;
            playerController.TeleParryOn();
            Debug.Log("Bullet hit sword");
            currentEnemyParry = collider.GetComponent<EnemyParry>();
            StartCoroutine(ParryWindowRun());
        }
    }
    public bool ParrySuccess()
    {
        if(canParry)
        {
            SoundControl.Instance.PlayerSwordDeflectSoundPlay();
            currentEnemyParry.Parry();
            GameEffect.Instance.FreezeEffectPlay();
            GameObject tmp = Instantiate(parryParticle, parryParticlePos.position, Quaternion.identity);
            Destroy(tmp, .4f);
        }
        return canParry;
    }
    private IEnumerator ParryWindowRun()
    {
        yield return new WaitForSeconds(playerController.GetParryWindow());
        canParry = false;
        currentEnemyParry = null;
        playerController.TeleParryOff();
    }
}
