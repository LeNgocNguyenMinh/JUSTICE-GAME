using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;
    public GameObject image;

    [SerializeField]private Animator animator;
    public void SetStartValue()
    {
        image.SetActive(false);
    }
    public void TouchLeft()
    {
        image.SetActive(true);
        animator.SetTrigger("TouchLeft");
    }
    public void TouchRight()
    {
        image.SetActive(true);
        animator.SetTrigger("TouchRight");
    }
    public void TouchBoth()
    {
        image.SetActive(true);
        animator.SetTrigger("TouchBoth");
    }
    public void CloseTutorial()
    {
        image.SetActive(false);
    }
}
