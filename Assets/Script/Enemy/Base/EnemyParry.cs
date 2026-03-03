using UnityEngine;

public class EnemyParry : MonoBehaviour
{
    public bool CanParry {get;set;}
    public virtual void Parry(){}
    public void SetCanParry(bool CanParry)
    {
        this.CanParry = CanParry;
    }
    public bool GetCanParry()
    {
        return CanParry;
    }
}
