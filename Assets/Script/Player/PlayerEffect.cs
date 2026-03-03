using UnityEngine;
using System.Collections;

public class PlayerEffect : MonoBehaviour
{
    
    public static PlayerEffect Instance;
    [SerializeField]private float duration;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private Material flaskMaterial;
    [SerializeField]private Material oriMaterial;
    [SerializeField]private GameObject hurtPartical;
    [SerializeField]private Transform hurtPos;
    void Awake()
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
    public void HurtEffect()
    {
        StopAllCoroutines();
        spriteRenderer.material = flaskMaterial;
        GameObject tmp = Instantiate(hurtPartical, hurtPos.position, Quaternion.identity);
        GameEffect.Instance.FreezeEffectPlay();
        Destroy(tmp, .4f);
        StartCoroutine(BackToOriMaterial());
    }
    private IEnumerator BackToOriMaterial()
    {
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = oriMaterial;
    }
}
