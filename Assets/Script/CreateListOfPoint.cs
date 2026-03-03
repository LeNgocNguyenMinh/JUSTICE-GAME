using UnityEngine;
using System.Collections.Generic;

public class CreateListOfPoint : MonoBehaviour
{
    public List<float> list4PointX;
    public List<float> list2PointX;
    public List<float> list4PointY;
    public List<float> list2PointY;
    public Vector3 bottomCenterPoint;
    public float camWidth;
    public float camHeight;
    public static CreateListOfPoint Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    public void SetStartValue()
    {
        CalculateCamWidthHeight();
        CreateList4PointX();
        CreateList2PointX();
        CreateList4PointY();
        CreateBottomCenterPoint();
    }
    public void CalculateCamWidthHeight()
    {
        camHeight = 2f * Camera.main.orthographicSize;
        camWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
    }
    public void CreateList4PointX()
    {
        list4PointX = new List<float>();
        float sectionWidth = Screen.width / 4f;
        for (int i = 0; i < 4; i++)
        {
            float centerX = sectionWidth * (i + 0.5f);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(centerX, 0, Mathf.Abs(Camera.main.transform.position.z))
        );
            list4PointX.Add(worldPos.x);
        }
    }
    public void CreateList2PointX()
    {
        list2PointX = new List<float>();
        float sectionWidth = Screen.width / 2f;
        for (int i = 0; i < 2; i++)
        {
            float centerX = sectionWidth * (i + 0.5f);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(centerX, 0, Mathf.Abs(Camera.main.transform.position.z))
        );
            list2PointX.Add(worldPos.x);
        }
    }
    public void CreateList4PointY()
    {
        list4PointY = new List<float>();
        float sectionHeight = Screen.height / 4f;
        for (int i = 0; i < 4; i++)
        {
            float centerY = sectionHeight * (i + 0.5f);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(0, centerY, Mathf.Abs(Camera.main.transform.position.z))
        );
            list4PointY.Add(worldPos.y);
        }
    }
//Creater 1 bottom center point 
    public void CreateBottomCenterPoint()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width/2, 0, Mathf.Abs(Camera.main.transform.position.z))
        );
        bottomCenterPoint = new Vector3(worldPos.x, -3f, 0);
    }
    public Vector3 GetBottomCenterPoint()
    {
        return bottomCenterPoint;
    }
    public List<float> GetList4PointY()
    {
        return list4PointY;
    }
    public List<float> GetList2PointX()
    {
        return list2PointX;
    }
    public List<float> GetList4PointX()
    {
        return list4PointX;
    }
}
