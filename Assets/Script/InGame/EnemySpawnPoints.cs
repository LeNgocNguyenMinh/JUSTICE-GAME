using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    public static EnemySpawnPoints Instance;
    public Transform rHSPoint;
    public Transform rHDPoint;
    public Transform lHSPoint;
    public Transform lHDPoint;
    public Transform rLSPoint;
    public Transform lLSPoint;   
    public Transform rLDPoint;
    public Transform lLDPoint;
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
    public void SetStartValue()
    {
        SetLHDPoint();
        SetRHDPoint();
        SetLHSPoint();
        SetRHSPoint();
        SetLLSPoint();
        SetRLSPoint();
        SetLLDPoint();
        SetRLDPoint();

    }
    public void SetRHSPoint()
    {
        float x = CreateListOfPoint.Instance.camWidth + 2f;
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, 0);
        rHSPoint.position = worldPos;
    }
    public void SetLHSPoint()
    {
        float x = -(CreateListOfPoint.Instance.camWidth + 2f);
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, 0);
        lHSPoint.position = worldPos;
    }
    public void SetRLSPoint()
    {
        float x = CreateListOfPoint.Instance.camWidth + 2f;
        float y = -3f;
        Vector3 worldPos = new Vector3(x, y, 0);
        rLSPoint.position = worldPos;
    }
    public void SetLLSPoint()
    {
        float x = -(CreateListOfPoint.Instance.camWidth + 2f);
        float y = -3f;
        Vector3 worldPos = new Vector3(x, y, 0);
        lLSPoint.position = worldPos;
    }
    public void SetRHDPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[3];
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, 0);
        rHDPoint.position = worldPos;
    }
    public void SetLHDPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[0];
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, 0);
        lHDPoint.position = worldPos;
    }
    public void SetRLDPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[3];
        float y = -3f;
        Vector3 worldPos = new Vector3(x, y, 0);
        rLDPoint.position = worldPos;
    }
    public void SetLLDPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[0];
        float y = -3f;
        Vector3 worldPos = new Vector3(x, y, 0);
        lLDPoint.position = worldPos;
    }

}