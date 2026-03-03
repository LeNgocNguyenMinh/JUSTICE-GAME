using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;
    [SerializeField]private int maxHealth;
    private int currentHealth;
    [SerializeField]private GameObject heart;
    [SerializeField]private Transform parentTransform;
    [SerializeField]private Sprite normalHeartSprite;
    [SerializeField]private Sprite brokenHeartSprite;
    public List<HeartStatus> hearts = new List<HeartStatus>();
    private bool isDead;
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
    public void SetStartValue()
    {
        ClearList();
        hearts.Clear();
        isDead = false;
        currentHealth = maxHealth;
        for(int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heart, parentTransform);
            newHeart.GetComponent<Image>().sprite = normalHeartSprite;
            HeartStatus heartStatus = new HeartStatus();
            heartStatus.heartImage = newHeart;
            heartStatus.isBreak = false;
            hearts.Add(heartStatus);
        }
    }
    public void PlayerHurt()
    {
        PlayerEffect.Instance.HurtEffect();
        for(int i = maxHealth - 1; i >= 0; i--)
        {
            if(hearts[i].isBreak == false)
            {
                hearts[i].heartImage.GetComponent<Image>().sprite = brokenHeartSprite;
                hearts[i].isBreak = true;
                break;
            }
        }
        currentHealth--;
        if(currentHealth <= 0)
        {
            PlayerController.Instance.Dead();
            return;
        }
        SoundControl.Instance.HitSoundPlay();
    }
    public void GainHeart()
    {
        for(int i = 0; i < maxHealth; i++)
        {
            if(hearts[i].isBreak == true)
            {
                currentHealth++;
                hearts[i].heartImage.GetComponent<Image>().sprite = normalHeartSprite;
                hearts[i].isBreak = false;
                break;
            }
        }
    }
    public void ClearList()
    {
        for(int i = 0; i < hearts.Count; i++)
        {
            Destroy(hearts[i].heartImage);
        }
        hearts.Clear();
    }
}
[System.Serializable]
public class HeartStatus
{
    public GameObject heartImage;
    public bool isBreak;
}