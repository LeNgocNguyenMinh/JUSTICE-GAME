using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Collections;
public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    [Header("--Main Menu Panel--")]
    [SerializeField] private RectTransform mainMenuPanel;
    [SerializeField] private RectTransform mainMenuBtnPanel;
    [SerializeField] private Image mainMenuBG;
    [SerializeField] private Gradient parryGradient;
    [Header("--Buttons--")]
    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button quitBtn;
    [Header("--Setting Panel--")]
    [SerializeField] private Button backToMainMenuBtn;
    [SerializeField] private float onStartDuration;
    [SerializeField] private float toSettingDuration;
    [SerializeField] private float toRestartDuration;

    [SerializeField] private Vector3 hiddenTopPos;
    [SerializeField] private Vector3 hiddenBottomPos;
    [Header("--Score--")]
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]private RectTransform scoreRect;

    private int point;
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
    private void Start()
    {
        scoreText.gameObject.SetActive(false); 
        Tutorial.Instance.SetStartValue();
    }
    private void OnEnable()
    {
        startBtn.onClick.AddListener(OnStartBtnClicked);
        settingsBtn.onClick.AddListener(OnSettingsBtnClicked);
        backToMainMenuBtn.onClick.AddListener(OnBackToMainMenuBtnClicked);
        quitBtn.onClick.AddListener(OnQuitBtnClicked);
    }
    private void OnStartBtnClicked()
    {
        ResetPoint();
        startBtn.interactable = false;
        settingsBtn.interactable = false;
        quitBtn.interactable = false;
        //set mainmenupanel localscale x to 0 using dotween
        mainMenuBtnPanel.DOScaleX(0, onStartDuration).OnComplete(() =>
        {
            scoreText.gameObject.SetActive(true); 
            EnemySpawnController.Instance.SetStartValue();
            PlayerController.Instance.SetStartValue();
            PlayerController.Instance.SetCanParry(true);
        });

    }
    private void OnSettingsBtnClicked()
    {
        //move setting panel to the center of the screen using dotween
        startBtn.interactable = false;
        settingsBtn.interactable = false;
        quitBtn.interactable = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Join(mainMenuPanel.DOAnchorPos(hiddenBottomPos, toSettingDuration));
        //add player transform y move to -15 
        sequence.Join(PlayerController.Instance.transform.DOLocalMoveY(-13, toSettingDuration));
    }
    private void OnBackToMainMenuBtnClicked()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(mainMenuPanel.DOAnchorPos(Vector2.zero, toSettingDuration));
        sequence.Join(PlayerController.Instance.transform.DOLocalMoveY(-3, toSettingDuration));
        sequence.OnComplete(() =>
        {
            startBtn.interactable = true;
            settingsBtn.interactable = true;
            quitBtn.interactable = true;
        });
    }
    public void GameSummary()
    { 
        mainMenuBtnPanel.DOScaleX(1, onStartDuration)
        .OnComplete(() =>
        {
            startBtn.interactable = true;
            settingsBtn.interactable = true;
            quitBtn.interactable = true;
        });      
    }
    private void OnQuitBtnClicked()
    {
        Application.Quit();
    }
    public void ParryGradientEffect(float duration)
    {
        mainMenuBG.DOKill();

        float location = 1f;

        DOTween.To(() => location, x =>
        {
            location = x;
            mainMenuBG.color = parryGradient.Evaluate(location);
        }, 0f, duration)
        .SetEase(Ease.Linear);
    }
    public void AddPoint()
    {
        point++;
        scoreText.text = "" + point;
    }
    public void ResetPoint()
    {
        point = 0;
        scoreText.text = "0";
    }
    public void OnDeadStart()
    {
        mainMenuBG.DOKill();
        mainMenuBG.color = parryGradient.Evaluate(1f);
        scoreText.gameObject.SetActive(false);
        float location = 1f;

        DOTween.To(() => location, x =>
        {
            location = x;
            mainMenuBG.color = parryGradient.Evaluate(location);
        }, 0f, 2f)
        .SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(CalculatePoint());
        });
    }
    private IEnumerator CalculatePoint()
    {
        scoreText.gameObject.SetActive(true);
        int calNum = -1;
        while(calNum < point)
        {
            calNum++;
            scoreText.text = "" + calNum;
            scoreRect.DOKill();
            scoreRect.localScale = Vector3.one;
            scoreRect.DOScale(2f, .05f)
            .SetLoops(2, LoopType.Yoyo);
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(1f);
        GameSummary();
    }
}
