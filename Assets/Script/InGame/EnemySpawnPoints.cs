using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    public static EnemySpawnPoints Instance;
    public Transform rHightStartPoint;
    public Transform rHightDesPoint;
    public Transform lHightStartPoint;
    public Transform lHightDesPoint;
    public Transform rLowStartPoint;
    public Transform lLowStartPoint;   
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
        SetLHightDesPoint();
        SetRHightDesPoint();
        SetLHightStartPoint();
        SetRHightStartPoint();
        SetLLowStartPoint();
        SetRLowStartPoint();

    }
    public void SetRHightStartPoint()
    {
        float x = CreateListOfPoint.Instance.camWidth + 2f;
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        rHightStartPoint.position = worldPos;
    }
    public void SetLHightStartPoint()
    {
        float x = -(CreateListOfPoint.Instance.camWidth + 2f);
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        lHightStartPoint.position = worldPos;
    }
    public void SetRLowStartPoint()
    {
        float x = CreateListOfPoint.Instance.camWidth + 2f;
        float y = -3f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        rLowStartPoint.position = worldPos;
    }
    public void SetLLowStartPoint()
    {
        float x = -(CreateListOfPoint.Instance.camWidth + 2f);
        float y = -3f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        lLowStartPoint.position = worldPos;
    }
    public void SetRHightDesPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[3];
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        rHightDesPoint.position = worldPos;
    }
    public void SetLHightDesPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[0];
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        lHightDesPoint.position = worldPos;
    }

}