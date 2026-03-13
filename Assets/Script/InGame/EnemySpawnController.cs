using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemySpawnController : MonoBehaviour
{
    public static EnemySpawnController Instance;
    [SerializeField]private float spawnDelay;
    [SerializeField]private GameObject[] onAirEnemyPrefabs;
    [SerializeField]private GameObject[] onGroundEnemyPrefabs;
    public List<GameObject> enemyOnField;
    private GameObject enemy;
    private GameObject enemy1;
    private GameObject enemy2;
    Vector3 scale;
    Vector3 scale1;
    Vector3 scale2;
    private bool canSpawn;
    public enum SpawnPos
    {
        RHigh,
        RLow,
        LHigh,
        LLow,
        HighBoth,
        LowBoth
    }
    public EnemySpawnFlow[] spawnFlow;
    private int indexFlow;

    private float spawnCount;
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
    private void Start()
    {
        canSpawn = false;
    }
    public void SetStartValue()
    {
        spawnCount = spawnDelay;
        indexFlow = 0;
        ClearList();
        canSpawn = true;
    }
    private void FixedUpdate()
    {
        if(!canSpawn)return;
        if(spawnCount <= 0)
        {
            enemyOnField.RemoveAll(e => e == null);
            if(enemyOnField.Count == 0)
            {
                SpawnEnemy(spawnFlow[indexFlow]);
                indexFlow++;
                if(indexFlow == spawnFlow.Length)
                {
                    indexFlow = 0;
                }
                spawnCount = spawnDelay;
            }
        }
        else
        {
            spawnCount -= Time.fixedDeltaTime;
        }
    }
    private void SpawnEnemy(EnemySpawnFlow enemySpawnFlow)
    {
        switch(enemySpawnFlow.spawnPos)
        {
            case SpawnPos.RHigh:
                enemy = Instantiate(enemySpawnFlow.enemy, EnemySpawnPoints.Instance.rHighStartPoint.position, Quaternion.identity);
                scale = enemy.transform.localScale;
                scale.x *= -1;
                enemy.transform.localScale = scale;
                enemy.GetComponent<Enemy>().SetStartValue();
                enemyOnField.Add(enemy);
                break;
            case SpawnPos.RLow:
                enemy = Instantiate(enemySpawnFlow.enemy, EnemySpawnPoints.Instance.rLowStartPoint.position, Quaternion.identity);
                scale = enemy.transform.localScale;
                scale.x *= -1;
                enemy.transform.localScale = scale;
                enemy.GetComponent<Enemy>().SetStartValue();
                enemyOnField.Add(enemy);
                break;
            case SpawnPos.LHigh:
                enemy = Instantiate(enemySpawnFlow.enemy, EnemySpawnPoints.Instance.lHighStartPoint.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().SetStartValue();
                enemyOnField.Add(enemy);
                break;
            case SpawnPos.LLow:
                enemy = Instantiate(enemySpawnFlow.enemy, EnemySpawnPoints.Instance.lLowStartPoint.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().SetStartValue();
                enemyOnField.Add(enemy);
                break;
            case SpawnPos.HighBoth:
                enemy1 = Instantiate(enemySpawnFlow.enemy, EnemySpawnPoints.Instance.rHighStartPoint.position, Quaternion.identity);
                scale = enemy1.transform.localScale;
                scale.x *= -1;
                enemy1.transform.localScale = scale;
                enemy2 = Instantiate(enemySpawnFlow.enemy, EnemySpawnPoints.Instance.lHighStartPoint.position, Quaternion.identity);
                enemy1.GetComponent<Enemy>().SetStartValue();
                enemy2.GetComponent<Enemy>().SetStartValue();
                enemyOnField.Add(enemy1);
                enemyOnField.Add(enemy2);
                break;
            case SpawnPos.LowBoth:
                enemy1 = Instantiate(enemySpawnFlow.enemy, EnemySpawnPoints.Instance.rLowStartPoint.position, Quaternion.identity);
                scale = enemy1.transform.localScale;
                scale.x *= -1;
                enemy1.transform.localScale = scale;
                enemy2 = Instantiate(enemySpawnFlow.enemy, EnemySpawnPoints.Instance.lLowStartPoint.position, Quaternion.identity);
                enemy1.GetComponent<Enemy>().SetStartValue();
                enemy2.GetComponent<Enemy>().SetStartValue();
                enemyOnField.Add(enemy1);
                enemyOnField.Add(enemy2);
                break;
        }
    }
    public void ClearList()
    {
        for(int i = 0; i < enemyOnField.Count; i++)
        {
            if(enemyOnField[i] != null)
            {
                Destroy(enemyOnField[i]);
            }
        }
        enemyOnField.Clear();
    }
    public void SetCanSpawn(bool value)
    {
        canSpawn = value;
    }
}
[System.Serializable]
public class EnemySpawnFlow
{
    public EnemySpawnController.SpawnPos spawnPos;
    public GameObject enemy;
}