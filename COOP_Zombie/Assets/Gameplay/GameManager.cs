using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Gameplay,
    WaitRound,
}

public class GameManager : MonoBehaviour
{
    //-----Singleton-----
    public static GameManager instance;

    public GameStates gameState;

    public delegate void RoundStarted();
    public static event RoundStarted onRoundStarted;

    private GameObject[] players;
    public int numberOfPlayers = 1;
    public int gameRound = 0;

    private float betweenRoundTimer = 0.0f;

    private void OnEnable()
    {
        EnemyManager.onAllZombiesKilled += ChangeRound;
    }

    private void OnDisable()
    {
        EnemyManager.onAllZombiesKilled -= ChangeRound;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        numberOfPlayers = players.Length;
        UpdateGameState(GameStates.WaitRound);
        //SceneManager.LoadScene("Assets/Scenes/EnvironmentLayout.unity", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (betweenRoundTimer >= 0f)
        {
            betweenRoundTimer -= Time.deltaTime;
            Debug.Log(betweenRoundTimer);
            if (betweenRoundTimer <= 0f)
            {
                UpdateGameState(GameStates.Gameplay);
            }
        }
    }

    public void UpdateGameState(GameStates newState)
    {
        gameState = newState;
        Debug.Log(gameState);
        switch (newState)
        {
            case GameStates.Gameplay:
                gameRound += 1;
                Debug.Log("Round: " + gameRound);
                onRoundStarted.Invoke();
                break;
            case GameStates.WaitRound:
                betweenRoundTimer = 5.0f;
                break;
        }
    }

    private void LoadMap()
    {
        SceneManager.LoadScene("Environment Layout", LoadSceneMode.Additive);
    }


    //--EventListener--
    private void ChangeRound() //When all zombies are killed change to WaitRoundState
    {
        UpdateGameState(GameStates.WaitRound);
    }
}


