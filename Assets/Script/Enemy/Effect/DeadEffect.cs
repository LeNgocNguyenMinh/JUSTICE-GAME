using UnityEngine;
using System.Collections;
public class DeadEffect : MonoBehaviour
{
    [SerializeField] private DeadRB[] deadRB;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float stayTime;
    
    public void OnDead()
    {
        for(int i = 0; i < deadRB.Length; i++)
        {
            Throw(deadRB[i].rotate, deadRB[i].rb);
        }
        StartCoroutine(DestroyAllObject());
    }
    private IEnumerator DestroyAllObject()
    {
        yield return new WaitForSeconds(stayTime);
        Destroy(gameObject);
    }
    public void Throw(float angleDeg, Rigidbody2D rb)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        float directionMul = transform.localScale.x > 0 ? 1 : -1;
        Vector2 dir = new Vector2(
            Mathf.Cos(rad)*directionMul,
            Mathf.Sin(rad)
        );

        rb.linearVelocity = dir.normalized * speed;
    }
}
[System.Serializable]
public class DeadRB
{
    public Rigidbody2D rb;
    public float rotate;
}