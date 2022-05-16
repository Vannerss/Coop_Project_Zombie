using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //singleton
    public static EnemyManager instance;

    private GameManager gameManager;

    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    public delegate void AllZombiesKilled();
    public static event AllZombiesKilled onAllZombiesKilled;

    private float enemyMaxHealth;
    const int defaultZSpawn = 24;
    const float difficultyMultiplier = 0.15f;
    private int currentRound;

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
        bool zombiesSpawned = false;
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            zombiesSpawned = true;
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && zombiesSpawned)
        {
            onAllZombiesKilled.Invoke();
        }
    }

    /* void SpawnNewEnemy(int amount)
     {
         Debug.Log("Spawned");
         //Instantiate(enemyPrefab, spawnPoints[0].transform.position, Quaternion.identity);

         for(int i = amount; i > 0; i--)
         {
             int holder = i;
             if (GameObject.FindGameObjectsWithTag("Enemy").Length < 2)
             {
                 Instantiate(enemyPrefab, spawnPoints[0].transform.position, Quaternion.identity);
             }
             else
             {
                 i = holder + 1;
             }
             Debug.Log(i);
         }
     }*/

    void SpawnZombies()
    {

        switch (gameManager.gameRound)
        {
            case 1:
                Debug.Log("case 1");
                amountToSpawn = Mathf.RoundToInt(maxAmountofZ * 0.2f);
                //Debug.Log(amountToSpawn + "ats");
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case 2:
                amountToSpawn = Mathf.RoundToInt(maxAmountofZ * 0.4f);
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case 3:
                amountToSpawn = Mathf.RoundToInt(maxAmountofZ * 0.6f);
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case 4:
                amountToSpawn = Mathf.RoundToInt(maxAmountofZ * 0.8f);
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case int n when (n >= 5 && n <= 9):
                amountToSpawn = maxAmountofZ;
                StartCoroutine(enemySpawn(amountToSpawn));
                break;
            case int n when (n >= 10):
                amountToSpawn = Mathf.RoundToInt((gameManager.gameRound * difficultyMultiplier) * maxAmountofZ);
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
            yield return new WaitForSeconds(1f);
            int holder = i;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 24)
            {
                Instantiate(enemyPrefab, spawnPoints[0].transform.position, Quaternion.identity);
            }
            else
            {
                i = holder + 1;
            }
            Debug.Log(i);
        }
    }
}
