using System.Collections;
using UnityEngine;

public class GameEffect : MonoBehaviour
{
    [SerializeField]private float duration;
    public static GameEffect Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    /* [SerializeField]private Material hitMaterial;
    [SerializeField]private Material defaultMaterial;
    [SerializeField]private SpriteRenderer spriteRenderer; */
    /* public void HitEffectPlay()
    {
        StopAllCoroutines();
        FlaskEffect();
        FreezeEffectPlay();
        StartCoroutine(ResetEffect());
    } */
    /* public void FlaskEffect()
    {
        spriteRenderer.material = hitMaterial;
    } */
    public void FreezeEffectPlay()
    {
        StopAllCoroutines();
        Time.timeScale = 0f;
        StartCoroutine(FreezeEffectStop());
    }
    public IEnumerator FreezeEffectStop()
    {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
    /* public IEnumerator ResetEffect()
    {
        yield return new WaitForSecondsRealtime(duration);
        spriteRenderer.material = defaultMaterial;
        Time.timeScale = 1f;
    } */
}
