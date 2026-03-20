using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [Header("-----Health-----")]
    [SerializeField]private int maxHitEarnHeart;
    [SerializeField]private Animator playerAnimator;
    [Header("-----Enemy attack des-----")]
    [SerializeField]private Transform enemyBulletDes;
    [Header("-----Parry-----")]
    [SerializeField]private Sprite oriParrySprite;
    [SerializeField]private Sprite teleParrySprite;
    [SerializeField]private SpriteRenderer parrySpriteRenderer;
    [SerializeField]private Transform parryTransform;
    [SerializeField]private PlayerSword playerSwordLeft;
    [SerializeField]private PlayerSword playerSwordRight;
    [SerializeField]private float parryWindow;
    [SerializeField]private float missPunishTime;
    [Header("-----Check Touch-----")]
    [SerializeField]private float bufferTime;
    private bool isWaitingForInput;
    private bool isDead;
    private bool hitLeftSide;
    private bool hitRightSide;
    private bool leftTouch;
    private bool rightTouch;
    private bool canParry = false;
    private bool inTutorial;
    public enum AnimationTriggerType
    {
        ParryLeftAnimEnd,
        ParryRightAnimEnd,
        ParryBothAnimEnd,
        DeadStartAnimEnd,
        DeadAnimIdle,
    }
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
    //Set default value
    public void SetStartValue()
    {
        inTutorial = true;
        Debug.Log("1");
        canParry = false;
        isDead = false;
        parrySpriteRenderer.enabled = false;
        playerAnimator.SetTrigger("RestIdle");
    }
    //Set start value
    public void OnStartBtnClicked()
    {
        parrySpriteRenderer.enabled = true;
        playerAnimator.SetTrigger("RightSideIdle");
        isDead = false;
        canParry = true;
        isWaitingForInput = false;
        leftTouch = false;
        rightTouch = false;
        PlayerHealthController.Instance.SetStartValue();
    }
    //update attack left, right or both by touch input
    void Update()
    {
        if(isDead)return;
        for(int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            if(t.phase == TouchPhase.Began)
            {
                if(t.position.x < Screen.width / 2)
                {
                    leftTouch = true;
                }
                else 
                {
                    rightTouch = true;
                }
            }   
            if(!isWaitingForInput)
            {
                StartCoroutine(ProccessInput());
            }
        }
    }
    private IEnumerator ProccessInput()
    {
        isWaitingForInput = true;
        yield return new WaitForSecondsRealtime(bufferTime);
        if(leftTouch && rightTouch)
        {
            ParryBoth();
        }
        else if (leftTouch)
        {
            ParryLeft();
        }
        else{
            ParryRight();
        }
        leftTouch = false;
        rightTouch = false;
        isWaitingForInput = false;
    }
    //attack left check
    void ParryLeft()
    {
        if(inTutorial)
        {
            if(!Tutorial.Instance.CheckSide(Tutorial.TouchType.TouchLeft))
            {
                return;
            }
        }
        if(canParry)
        {
            Debug.Log("3");
            canParry = false;
            if(playerSwordLeft.ParrySuccess())
            {
                hitLeftSide = true;
                MenuController.Instance.ParryGradientEffect(parryWindow);
                ComboMultiplier.Instance.IncreaseCombo();
            }
            else
            {
                ComboMultiplier.Instance.ComboReset();
            }
            SoundControl.Instance.PlayerSwordSheathSoundPlay();
            playerAnimator.SetTrigger("LeftSideParry");
        }
    }
    //attack right check
    void ParryRight()
    {
        if(inTutorial)
        {
            if(!Tutorial.Instance.CheckSide(Tutorial.TouchType.TouchRight))
            {
                return;
            }
        }
        if(canParry)
        {   
            Debug.Log("5");
            canParry = false;
            if(playerSwordRight.ParrySuccess())
            {
                hitRightSide = true;
                MenuController.Instance.ParryGradientEffect(parryWindow);
                ComboMultiplier.Instance.IncreaseCombo();
            }
            else
            {
                ComboMultiplier.Instance.ComboReset();
            }
            SoundControl.Instance.PlayerSwordSheathSoundPlay();
            playerAnimator.SetTrigger("RightSideParry");
        }
    }
    //attack both check
    private void ParryBoth()
    {
       if(inTutorial)
        {
            if(!Tutorial.Instance.CheckSide(Tutorial.TouchType.TouchBoth))
            {
                return;
            }
        }
        if(canParry)
        {        
            Debug.Log("7");    
            canParry = false;
            if(playerSwordLeft.ParrySuccess() && playerSwordRight.ParrySuccess())
            {
                hitLeftSide = true;
                hitRightSide = true;
                MenuController.Instance.ParryGradientEffect(parryWindow);
                ComboMultiplier.Instance.IncreaseCombo();
            }
            else
            {
                ComboMultiplier.Instance.ComboReset();
            }
            SoundControl.Instance.PlayerSwordSheathSoundPlay();
            playerAnimator.SetTrigger("BothSideParry");
        }
    }
    //Return the destination point for enemy projectiles
    public Vector3 GetDesPoint()
    {
        return enemyBulletDes.position;
    }
    //teleport parry effect on and off
    public void TeleParryOn()
    {
        SoundControl.Instance.PlayerDeflectTingSoundPlay();
        parrySpriteRenderer.sprite = teleParrySprite;
        Vector3 localScale = parryTransform.localScale;
        localScale.x *= 2f;
        localScale.y *= 2f;
        parryTransform.localScale = localScale;
        //using DG, make it scale back to original in parryWindow time
        parryTransform.DOScale(1f, parryWindow).OnComplete(()=>
        {
            TeleParryOff();
        });
    }
    //teleport parry effect off
    public void TeleParryOff()
    {
        parrySpriteRenderer.sprite = oriParrySprite;
    }
    //Animation event trigger function, called by animation event
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        if(triggerType == AnimationTriggerType.DeadStartAnimEnd)
        {
            playerAnimator.SetTrigger("DeadIdle");
        }
        if(triggerType == AnimationTriggerType.ParryRightAnimEnd)
        {
            Debug.Log("5-1");
            if(hitRightSide)
            {
                hitRightSide = false;
                canParry = true;
                playerAnimator.SetTrigger("RightSideIdle");
            }
            else
            {         
                StartCoroutine(ParryMissPunish("Right"));
            }
            
        }
        if(triggerType == AnimationTriggerType.ParryLeftAnimEnd)
        {
            Debug.Log("3-1");
            if(hitLeftSide)
            {
                hitLeftSide = false;
                canParry = true;
                playerAnimator.SetTrigger("LeftSideIdle");
            }
            else
            {
                StartCoroutine(ParryMissPunish("Left"));
            }
            
        }
        if(triggerType == AnimationTriggerType.ParryBothAnimEnd)
        {
            Debug.Log("7-1");
            if(hitLeftSide && hitRightSide)
            {
                hitLeftSide = false;
                hitRightSide = false;
                canParry = true;
                playerAnimator.SetTrigger("BothSideIdle");
            }
            else
            {
                StartCoroutine(ParryMissPunish("Both"));
            }
        }
    }
    //parry miss punish
    private IEnumerator ParryMissPunish(string side)
    {
        canParry= false;
        if(side == "Left")
        {
            playerAnimator.SetTrigger("LeftSideMiss");
        }
        else if(side == "Right")
        {
            playerAnimator.SetTrigger("RightSideMiss");
        }
        else
        {
            playerAnimator.SetTrigger("BothSideMiss");
        }
        yield return new WaitForSeconds(missPunishTime);
        Debug.Log("Finish punish");
        canParry = true;
        if(side == "Left")
        {
            playerAnimator.SetTrigger("LeftSideIdle");
        }
        else if(side == "Right")
        {
            playerAnimator.SetTrigger("RightSideIdle");
        }
        else
        {
            playerAnimator.SetTrigger("BothSideIdle");
        }
    }
    //Player gain heart
    public void GainHeart()
    {
        PlayerHealthController.Instance.GainHeart();
    }
    //player dead function
    public void Dead()
    {
        if(!isDead)
        {
            StopAllCoroutines();
            isDead = true;
            Debug.Log("11");
            canParry = false;
            EnemySpawnController.Instance.SetCanSpawn(false);
            EnemySpawnController.Instance.ClearList();
            parrySpriteRenderer.enabled = false;
            MenuController.Instance.OnDeadStart();
            playerAnimator.SetTrigger("DeadStart");
        }
    }
    //Get parry window time
    public float GetParryWindow()
    {
        return parryWindow;
    }    
    public void SetCanParry(bool value)
    {
        canParry = value;
    }
    public int GetMaxHitEarnHeart()
    {
        return maxHitEarnHeart;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyATKCollider"))
        {
            PlayerHealthController.Instance.PlayerHurt();
            collision.GetComponent<EnemyParry>().EnemyATKHitPlayer();;
        }
    }
    public bool GetInTutorial()
    {
        return inTutorial;
    }
    public void SetInTutorial(bool value)
    {
        inTutorial = value;
    }
}
