using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;
    [SerializeField]private float tutorialTimeScale;
    public GameObject image;
    private bool panelActive;
    private int tutorialNum = -1;
    private bool inTutorial;
    public enum TouchType
    {
        TouchLeft,
        TouchRight,
        TouchBoth,
        TouchNone
    }
    private TouchType touchType;
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
    [SerializeField]private Animator animator;
    public void SetStartValue()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        panelActive = false;
        image.SetActive(false);
        tutorialNum = -1;
        inTutorial = true;
    }
    public void PlayTutorial()
    {
        if(tutorialNum == 2)
        {
            inTutorial = false;
            return;
        }
        tutorialNum ++;
        switch(tutorialNum)
        {
            case 0:
                TouchRight();
                break;
            case 1:
                TouchLeft();
                break;
            case 2:
                TouchBoth();
                break;
        }

    }
    public void TouchLeft()
    { 
        if(!panelActive)
        {
            panelActive = true;
            image.SetActive(true);
            Time.timeScale = tutorialTimeScale;
            touchType = TouchType.TouchLeft;
            //let animator not effect by timescale
            animator.SetTrigger("TouchLeft");
        }  
    }
    public void TouchRight()
    {
        if(!panelActive)
        {
            panelActive = true;
            image.SetActive(true);
            Time.timeScale = tutorialTimeScale;
            touchType = TouchType.TouchRight;
            //let animator not effect by timescale
            animator.SetTrigger("TouchRight");
        } 
    }
    public void TouchBoth()
    {
        if(!panelActive)
        {
            panelActive = true;
            image.SetActive(true);
            Time.timeScale = tutorialTimeScale;
            touchType = TouchType.TouchBoth;
            //let animator not effect by timescale
            animator.SetTrigger("TouchBoth");
        } 
    }
    public void CloseCurrentTutorial()
    {
        panelActive = false;
        image.SetActive(false);
        Time.timeScale = 1f;
        if(tutorialNum == 2)
        {
            PlayerController.Instance.SetInTutorial(false);
            return;
        }
    }
    public bool CheckSide(TouchType side)
    {
        if(panelActive)
        {
            if(side == touchType)
            {
                CloseCurrentTutorial();
                return true;
            }
        }
        return false;
    }
}
