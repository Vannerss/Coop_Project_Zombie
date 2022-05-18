using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //singleton
    public static EnemyManager instance;

    private GameManager gameManager;

    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    public delegate void AllZombiesKilled();
    public static event AllZombiesKilled onAllZombiesKilled;

    public int enemyMaxHealth = 100;
    const int defaultZSpawn = 24;
    const float difficultyMultiplier = 0.15f;
    private int currentRound;

    bool zombiesSpawned = false;
    int amountToSpawn;
    int maxAmountofZ;

    private void OnEnable()
    {
        GameManager.onRoundStarted += SpawnZombies;
    }

    private void OnDisable()
    {
        GameManager.onRoundStarted -= SpawnZombies;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        gameManager = GameManager.instance;
        maxAmountofZ = defaultZSpawn + ((gameManager.gameRound - 1) * 6);
    }

    private void Update()
    {
        currentRound = gameManager.gameRound;
        
        
        //if(currentRound >= 7 && GameObject.FindGameObjectsWithTag("Enemy").Length <= 5)
        //{

        //}
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && zombiesSpawned == false)
        {
            Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
            zombiesSpawned = true;
        }
        else if (zombiesSpawned == true)
        {
            CheckIfAllDead();
        }
    }

    void CheckIfAllDead()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Debug.Log("AllDead");
            zombiesSpawned = false;
            onAllZombiesKilled.Invoke();
        }
    }

    void SpawnZombies()
    {
        switch (gameManager.gameRound)
        {
            case 1:
                Debug.Log("case 1");
                amountToSpawn = Mathf.RoundToInt(maxAmountofZ * 0.2f);
                enemyMaxHealth += 100;
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case 2:
                amountToSpawn = Mathf.RoundToInt(maxAmountofZ * 0.4f);
                enemyMaxHealth += 100;
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case 3:
                amountToSpawn = Mathf.RoundToInt(maxAmountofZ * 0.6f);
                enemyMaxHealth += 100;
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case 4:
                
                amountToSpawn = Mathf.RoundToInt(maxAmountofZ * 0.8f);
                enemyMaxHealth += 100;
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case int n when (n >= 5 && n <= 9):
                amountToSpawn = maxAmountofZ;
                enemyMaxHealth += 100;
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case int n when (n >= 10):
                amountToSpawn = Mathf.RoundToInt((gameManager.gameRound * difficultyMultiplier) * maxAmountofZ);
                enemyMaxHealth += Mathf.RoundToInt(enemyMaxHealth * 0.10f);
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            default:
                break;
        }
    }

    IEnumerator enemySpawn(int amount)
    {
        Debug.Log("IEnum");
        for (int i = amount; i >= 0; i--)
        {
            Debug.Log("IEnum 4 loop");
            yield return new WaitForSeconds(.1f);
            int holder = i;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 24)
            {
                InstanceEnemy();
                //Instantiate(enemyPrefab, spawnPoints[0].transform.position, Quaternion.identity);
            }
            else
            {
                i = holder + 1;
            }
            Debug.Log(i);
        }
    }

    private void InstanceEnemy()
    {
        int randomZombie = Random.Range(0, 5);
        int randomSpawnLoc = Random.Range(0, spawnPoints.Length);

        Instantiate(enemyPrefabs[randomZombie], spawnPoints[randomSpawnLoc].transform.position, Quaternion.identity);
    }
}
