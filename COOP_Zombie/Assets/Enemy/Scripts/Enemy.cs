using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3[] playerPositions;

    public GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        if (gameManager.playerOne.isSet == true)
        {
            playerPositions[0] = gameManager.playerOne.Value.position;
            Debug.Log("hi");
        }
        if (gameManager.playerTwo.isSet == true)
        {
            playerPositions[1] = gameManager.playerTwo.Value.position;
        }
        if (gameManager.playerThree.isSet == true)
        {
            playerPositions[2] = gameManager.playerThree.Value.position;
        }
        if (gameManager.playerFour.isSet == true)
        {
            playerPositions[3] = gameManager.playerFour.Value.position;
        }
        if (gameManager.playerFive.isSet == true)
        {
            playerPositions[4] = gameManager.playerFive.Value.position;
        }

        PrintPositions();
    }

    void PrintPositions()
    {
        Debug.Log(playerPositions[0]);
    }

    private Vector3 FindClosestPlayerLocation()
    {
        Vector3 closestPlayer = new Vector3();
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Vector3 potentialTarget in playerPositions)
        {
            if (potentialTarget != null)
            {
                Vector3 directionToTarget = potentialTarget - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    closestPlayer = potentialTarget;
                }
            }
        }

        return closestPlayer;
    }
}

public enum EnemyStates
{
    SlowWalking,
    FastWalking,
    Attacking,
    Died,
}
