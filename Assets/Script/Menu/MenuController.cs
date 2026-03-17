using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Collections;
using System.Runtime;
public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    public enum AnimationTriggerType
    {
        NameIntroStartEnd,
        AnimIntroStartEnd,
        TextHitSoundPlay,
        PanelShowSoundPlay,
        SwordHitSoundPlay,
        WindSoundPlay,
        LeafWalkSoundPlay
    }
    [Header("--Main Menu Panel--")]
    [SerializeField]private Animator animator;
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
    [Header("--Lower Panel--")]
    [SerializeField]private GameObject hintImage;
    private int point;
    [Header("--GameObject Start Place--")]
    [SerializeField]private GameObject gameManager;
    [SerializeField]private GameObject player;
    [SerializeField]private Vector3 playerMidPanelPos;
    [SerializeField]private Vector3 playerUpperPanelPos;
    [SerializeField]private Vector3 playerLowerPanelPos;
    private bool canPlayAnimIntro;
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
        SetStartValue();
    }
    private void SetStartValue()
    {
        SoundAndMusicSetting.Instance.SetStartValue();
        SoundControl.Instance.MainMenuMusicPlay();
        canPlayAnimIntro = false;
        mainMenuPanel.anchoredPosition = hiddenBottomPos;//Game Start at the lowest panel
        hintImage.SetActive(false);//Not show the image ("press anywhere to start")
        animator.SetTrigger("NameIntroStart");//Start to show the game name
    }
    //Set button function
    private void OnEnable()
    {
        startBtn.onClick.AddListener(OnStartBtnClicked);
        settingsBtn.onClick.AddListener(OnSettingsBtnClicked);
        backToMainMenuBtn.onClick.AddListener(OnBackToMainMenuBtnClicked);
        quitBtn.onClick.AddListener(OnQuitBtnClicked);
    }
    //Set start btn func
    private void OnStartBtnClicked()
    {
        ResetPoint();//Reset point to 0
        startBtn.interactable = false;//temp disable btn interact
        settingsBtn.interactable = false;
        quitBtn.interactable = false;
        //set mainmenupanel localscale x to 0 using dotween
        mainMenuBtnPanel.DOScaleX(0, onStartDuration).OnComplete(() =>
        {
            scoreText.gameObject.SetActive(true); //show score record
            EnemySpawnController.Instance.SetStartValue(); // start spawn enemy
            PlayerController.Instance.OnStartBtnClicked(); // Ready player
            PlayerController.Instance.SetCanParry(true); // 
        });

    }
    private void OnSettingsBtnClicked()
    {
        //move setting panel to the center of the screen using dotween
        startBtn.interactable = false;
        settingsBtn.interactable = false;
        quitBtn.interactable = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Join(mainMenuPanel.DOAnchorPos(hiddenTopPos, toSettingDuration));
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
    private void OnQuitBtnClicked()
    {
        Application.Quit();
    }
    //success parry effect
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
    //After Dead action: Calculate point, show btns
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
        mainMenuBtnPanel.DOScaleX(1, onStartDuration)
        .OnComplete(() =>
        {
            startBtn.interactable = true;
            settingsBtn.interactable = true;
            quitBtn.interactable = true;
        });
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        switch(triggerType)
        {
            case AnimationTriggerType.NameIntroStartEnd:
                animator.SetTrigger("NameIntroIdle");
                SpawnGameObject();
                break;
            case AnimationTriggerType.AnimIntroStartEnd:
                OnBackToMainMenuBtnClicked();
                break;
            case AnimationTriggerType.SwordHitSoundPlay:
                SoundControl.Instance.PlayerSwordDeflectSoundPlay();
                break;
            case AnimationTriggerType.WindSoundPlay:
                SoundControl.Instance.WindBlowSoundPlay();
                break;
            case AnimationTriggerType.LeafWalkSoundPlay:
                SoundControl.Instance.LeafWalkSoundPlay();
                break;
            case AnimationTriggerType.PanelShowSoundPlay:
                SoundControl.Instance.PlayerSwordSheathSoundPlay();
                break;
        }
    }
    //Loading and spawn object in mid panel (player, object containt scripts)
    private void SpawnGameObject()
    {
        //Instantiate game object
        Instantiate(gameManager, Vector3.zero, Quaternion.identity);//spawn game manager
        Instantiate(player, new Vector3(0,7,0), Quaternion.identity);//spawn player 
        //Ready sript
        CreateListOfPoint.Instance.SetStartValue();
        EnemySpawnPoints.Instance.SetStartValue();
        EnemySpawnController.Instance.SetStartValue();
        PlayerController.Instance.SetStartValue();
        Tutorial.Instance.SetStartValue();//Set Start up value and action for tutorial
        //UI ready
        scoreText.gameObject.SetActive(false); //Hide the point record
        hintImage.SetActive(true);
        canPlayAnimIntro = true;
    }
    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(2f);
    }
    private void Update()
    {
        if(Input.touchCount>0)
        {
            if(canPlayAnimIntro)
            {
                canPlayAnimIntro = false;
                hintImage.SetActive(false);
                animator.SetTrigger("AnimIntroStart");
            }
        }
    }
}
