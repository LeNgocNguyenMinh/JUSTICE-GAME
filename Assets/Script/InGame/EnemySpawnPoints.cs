using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    public static EnemySpawnPoints Instance;
    public Transform rHighStartPoint;
    public Transform rHighDesPoint;
    public Transform lHighStartPoint;
    public Transform lHighDesPoint;
    public Transform rLowStartPoint;
    public Transform lLowStartPoint;   
    public Transform rLowDesPoint;
    public Transform lLowDesPoint;
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
        SetLHighDesPoint();
        SetRHighDesPoint();
        SetLHighStartPoint();
        SetRHighStartPoint();
        SetLLowStartPoint();
        SetRLowStartPoint();
        SetLLowDesPoint();
        SetRLowDesPoint();

    }
    public void SetRHighStartPoint()
    {
        float x = CreateListOfPoint.Instance.camWidth + 2f;
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        rHighStartPoint.position = worldPos;
    }
    public void SetLHighStartPoint()
    {
        float x = -(CreateListOfPoint.Instance.camWidth + 2f);
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        lHighStartPoint.position = worldPos;
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
    public void SetRHighDesPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[3];
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        rHighDesPoint.position = worldPos;
    }
    public void SetLHighDesPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[0];
        float y = 1f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        lHighDesPoint.position = worldPos;
    }
    public void SetRLowDesPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[3];
        float y = -3f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        rLowDesPoint.position = worldPos;
    }
    public void SetLLowDesPoint()
    {
        float x = CreateListOfPoint.Instance.GetList4PointX()[0];
        float y = -3f;
        Vector3 worldPos = new Vector3(x, y, Mathf.Abs(Camera.main.transform.position.z));
        lLowDesPoint.position = worldPos;
    }

}