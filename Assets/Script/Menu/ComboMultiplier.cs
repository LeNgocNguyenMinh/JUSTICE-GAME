using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ComboMultiplier : MonoBehaviour
{
    public static ComboMultiplier Instance;
    [SerializeField]private Sprite[] deflectSuccessSprite;
    [SerializeField]private Sprite[] deflectFailSprite;
    [SerializeField]private Image popUpObject;
    private int comboCount = 0;
    [SerializeField]private float comboTimer = 2f;
    [SerializeField]private GameObject popUpText;
    [SerializeField]private RectTransform rectTransform;
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
    public void IncreaseCombo()
    {
        comboCount++;
        if(comboCount == PlayerController.Instance.GetMaxHitEarnHeart())
        {
            comboCount = 0;
            PlayerController.Instance.GainHeart();
        }
        MenuController.Instance.AddPoint();
        UpdateComboDisplay();
    }
    private void UpdateComboDisplay()
    {
        int index = Random.Range(0, deflectSuccessSprite.Length);
        PopUpSprite(deflectSuccessSprite[index]);
    }
    public void ComboReset()
    {
        comboCount = 0;
        int index = Random.Range(0, deflectFailSprite.Length);
        PopUpSprite(deflectFailSprite[index]);
    }
    public void PopUpText(string text, float showDuration, float hideDuration, float heightFloat)
    {
        GameObject tmpObj = Instantiate(popUpText, rectTransform);
        RectTransform tmpRect =  tmpObj.GetComponent<RectTransform>();
        TextMeshProUGUI tmpText = tmpObj.GetComponentInChildren<TextMeshProUGUI>();
        tmpRect.rotation = Quaternion.Euler(0, 0, Random.Range(-45f, 45f));
        /* popUpText.alpha = 1f; */
        tmpText.text = text;
        tmpRect.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        seq.Append(tmpRect.DOAnchorPosY(tmpRect.anchoredPosition.y + heightFloat, showDuration)
            .SetEase(Ease.OutQuad));
        seq.Join(tmpRect.DOScale(2f, showDuration).SetEase(Ease.OutBack));
        seq.OnComplete(()=>
        {
            tmpRect.DOScale(0f, hideDuration).SetEase(Ease.OutBack).OnComplete(()=>
            {
                Destroy(tmpObj);
            });
        });
    }
    public void PopUpSprite(Sprite sprite)
    {
        GameObject tmpObj = Instantiate(popUpObject.gameObject, rectTransform);
        Image tmpImage = tmpObj.GetComponent<Image>();
        tmpImage.sprite = sprite;
        RectTransform tmpRect = tmpObj.GetComponent<RectTransform>();   /* 
        tmpRect.rotation = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)); */
        tmpRect.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        seq.Append(tmpRect.DOAnchorPosY(tmpRect.anchoredPosition.y + 2f, .1f)
            .SetEase(Ease.OutQuad));
        seq.Join(tmpRect.DOScale(2f, .5f).SetEase(Ease.OutBack));
        seq.OnComplete(()=>
        {
            tmpRect.DOScale(0f, .1f).SetEase(Ease.OutBack).OnComplete(()=>
            {
                Destroy(tmpObj);
            });
        });
    }
}
