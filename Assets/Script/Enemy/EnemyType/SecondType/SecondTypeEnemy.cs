using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

public class SecondTypeEnemy : Enemy
{
    [SerializeField]private float runSpeed;
    [SerializeField]private GameObject deadPart;
    [SerializeField]private GameObject slash;
    [SerializeField]private Transform slashPos;
    [SerializeField]private LongStickCollider longStickCollider;
    private Vector3 direct;
    public override void SetStartValue()
    {
        direct = (PlayerController.Instance.transform.position - transform.position).normalized;
        longStickCollider.SetStartValue();
        StateMachine.Initialize(WalkState);
    }
    public override void Walk()
    {
        RB.linearVelocity = direct * runSpeed;
    }
    public void ReachDes()
    {
        RB.linearVelocity = Vector2.zero;
        Destroy(gameObject);
    }
    
}
