using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField]private int maxHitEarnHeart;
    [SerializeField]private Animator playerAnimator;
    [SerializeField]private Transform enemyBulletDes;
    [SerializeField]private Sprite oriParrySprite;
    [SerializeField]private Sprite teleParrySprite;
    [SerializeField]private SpriteRenderer parrySpriteRenderer;
    [SerializeField]private Transform parryTransform;
    [SerializeField]private PlayerSword playerSwordLeft;
    [SerializeField]private PlayerSword playerSwordRight;
    [SerializeField]private float parryWindow;
    [SerializeField]private float missPunishTime;
    public List<float> listX;
    private bool isDead;
    private bool hitLeftSide;
    private bool hitRightSide;
    private bool leftTouch;
    private bool rightTouch;
    private bool canParry = false;
    public enum AnimationTriggerType
    {
        ATKLeftAnimEnd,
        ATKRightAnimEnd,
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
    public void Start()
    {
        canParry = false;
        isDead = false;
        parrySpriteRenderer.enabled = false;
        playerAnimator.SetTrigger("RestIdle");   
        SetStartPosition();
    }
    //Spawn start Position
    public void SetStartPosition()
    {
        CreateListOfPoint.Instance.CreateBottomCenterPoint();
        transform.position = CreateListOfPoint.Instance.GetBottomCenterPoint();
    }
    //Set start value
    public void SetStartValue()
    {
        parrySpriteRenderer.enabled = true;
        playerAnimator.SetTrigger("RightSideIdle");
        isDead = false;
        canParry = true;
        PlayerHealthController.Instance.SetStartValue();
    }
    //update attack left, right or both by touch input
    void FixedUpdate()
    {
        if(isDead || !canParry)return;
        leftTouch = false;
        rightTouch = false;
        if(Input.touchCount>0)
        {
            foreach(Touch t in Input.touches)
            {
                if(t.position.x < Screen.width /2)
                {
                    leftTouch = true;
                }
                else if(t.position.x >= Screen.width /2)
                {
                    rightTouch = true;
                }
            }
            if(leftTouch && rightTouch )
            {
                ParryBoth();
            }
            else if(leftTouch)
            {
                ParryLeft();
            }
            else if(rightTouch)
            {
                ParryRight();
            }
        }
    }
    //attack left check
    void ParryLeft()
    {
        if(canParry)
        {
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
            playerAnimator.SetTrigger("ATKLeftSide");
        }
    }
    //attack right check
    void ParryRight()
    {
        if(canParry)
        {   
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
            playerAnimator.SetTrigger("ATKRightSide");
        }
    }
    //attack both check
    private void ParryBoth()
    {
        if(canParry)
        {            
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
            playerAnimator.SetTrigger("ParryBothSide");
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
        if(triggerType == AnimationTriggerType.ATKRightAnimEnd)
        {
            if(hitRightSide)
            {
                hitRightSide = false;
                canParry = true;
                playerAnimator.SetTrigger("RightSideIdle");
            }
            else
            {
                canParry = false;                
                StartCoroutine(ParryMissPunish("Right"));
            }
            
        }
        if(triggerType == AnimationTriggerType.ATKLeftAnimEnd)
        {
            if(hitLeftSide)
            {
                hitLeftSide = false;
                canParry = true;
                playerAnimator.SetTrigger("LeftSideIdle");
            }
            else
            {
                canParry = false;
                StartCoroutine(ParryMissPunish("Left"));
            }
            
        }
        if(triggerType == AnimationTriggerType.ParryBothAnimEnd)
        {
            canParry = true;
            playerAnimator.SetTrigger("BothSideIdle");
        }
    }
    //parry miss punish
    private IEnumerator ParryMissPunish(string side)
    {
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
        canParry = true;
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
}
