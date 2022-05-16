using UnityEngine;

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

    public TransformAnchorSO playerOne = default;
    public TransformAnchorSO playerTwo = default;
    public TransformAnchorSO playerThree = default;
    public TransformAnchorSO playerFour = default;
    public TransformAnchorSO playerFive = default;

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
        UpdateNumberOfPlayersTransforms(numberOfPlayers);
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

    public void UpdateNumberOfPlayersTransforms(int numOfPlayers)
    {
        switch (numOfPlayers)
        {
            case 1:
                playerOne.Provide(players[0].transform);
                break;
            case 2:
                playerOne.Provide(players[0].transform);
                playerTwo.Provide(players[1].transform);
                break;
            case 3:
                playerOne.Provide(players[0].transform);
                playerTwo.Provide(players[1].transform);
                playerThree.Provide(players[2].transform);
                break;
            case 4:
                playerOne.Provide(players[0].transform);
                playerTwo.Provide(players[1].transform);
                playerThree.Provide(players[2].transform);
                playerFour.Provide(players[3].transform);
                break;
            case 5:
                playerOne.Provide(players[0].transform);
                playerTwo.Provide(players[1].transform);
                playerThree.Provide(players[2].transform);
                playerFour.Provide(players[3].transform);
                playerFive.Provide(players[4].transform);
                break;
        }
    }

    //--EventListener--
    private void ChangeRound() //When all zombies are killed change to WaitRoundState
    {
        UpdateGameState(GameStates.WaitRound);
    }
}


