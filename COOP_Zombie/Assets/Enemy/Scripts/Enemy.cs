using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private GameManager gameManager;
    private EnemyManager enemyManager;
    private GameObject[] players;
    private NavMeshAgent Agent;

    private GameObject targetPlayer;

    private void Start()
    {
        gameManager = GameManager.instance;
        enemyManager = EnemyManager.instance;
        players = GameObject.FindGameObjectsWithTag("Player");
        Agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindClosePlayer());
    }

    private void Update()
    {
        Agent.destination = targetPlayer.transform.position;
    }

    private void Attack()
    {

    }

    private void FindClosestPlayerLocation()
    {
        Vector3 closestPlayer = new Vector3();
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        //foreach (Vector3 potentialTarget in playerPositions)
        for(int i = 0; i < players.Length; i++)
        {
            Vector3 directionToTarget = players[i].transform.position - currentPosition;
            float distanceSqrToTarget = directionToTarget.sqrMagnitude;
            if (distanceSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToTarget;
                closestPlayer = players[i].transform.position;
                targetPlayer = players[i];
            }
        }
    }

    IEnumerator FindClosePlayer()
    {
        while (true)
        {
            FindClosestPlayerLocation(); 
            yield return new WaitForSeconds(10.0f);
        }
    }
}

public enum EnemyStates
{
    SlowWalking,
    FastWalking,
    Attacking,
    Died,
}
