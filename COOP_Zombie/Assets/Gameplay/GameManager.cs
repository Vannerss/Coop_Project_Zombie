using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //-----Singleton-----
    public static GameManager instance;

    public GameStates gameState;

    public TransformAnchorSO playerOne = default;
    public TransformAnchorSO playerTwo = default;
    public TransformAnchorSO playerThree = default;
    public TransformAnchorSO playerFour = default;
    public TransformAnchorSO playerFive = default;

    private GameObject[] players;
    private int numberOfPlayers;


    public int gameRound;

    private void OnDisable()
    {
        playerOne.Unset();
        playerTwo.Unset();
        playerThree.Unset();
        playerFour.Unset();
        playerFive.Unset();
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        numberOfPlayers = players.Length;
        UpdateNumberOfPlayersTransforms(numberOfPlayers);
    }

    public void UpdateGameState(GameStates newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameStates.Gameplay:
                break;
            case GameStates.WaitRound:
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
}

public enum GameStates
{
    Gameplay,
    WaitRound,
}
