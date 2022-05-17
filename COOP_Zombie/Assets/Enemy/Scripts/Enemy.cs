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
    private Animation anim;
    private GameObject targetPlayer;

    private float attackCD = 0f;

    private void Start()
    {
        gameManager = GameManager.instance;
        enemyManager = EnemyManager.instance;
        players = GameObject.FindGameObjectsWithTag("Player");
        anim = GetComponent<Animation>();
        Agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindClosePlayer());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, .8f);
    }

    private void Update()
    {
        if(attackCD > 0f)
        {
            attackCD -= Time.deltaTime;
        }

        Agent.destination = targetPlayer.transform.position;

        if(Vector3.Distance(this.transform.position, targetPlayer.transform.position) <= 0.8f && attackCD <= 0f)
        {
            Attack();
            Debug.Log("InRange");
        }
    }

    private void Attack()
    {
        anim.Play("Enemy_Attack");
        attackCD = 1.5f;
        Debug.Log("Attacked");
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
