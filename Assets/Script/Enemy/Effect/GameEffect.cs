using System.Collections;
using UnityEngine;

public class GameEffect : MonoBehaviour
{
    [SerializeField]private float duration;
    [SerializeField]private ParticleSystem[] fireWorkList;
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
    public void FireWorkPlay()
    {
        foreach(ParticleSystem fireWork in fireWorkList)
        {
            fireWork.Play();
        }
    }
}
