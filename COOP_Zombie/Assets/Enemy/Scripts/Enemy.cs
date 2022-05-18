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

    private float attackCD = 0f;
    private int enemyHealth;

    private void Start()
    {
        gameManager = GameManager.instance;
        enemyManager = EnemyManager.instance;
        players = GameObject.FindGameObjectsWithTag("Player");
        Agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindClosePlayer());
        enemyHealth = enemyManager.enemyMaxHealth;
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
    }

    public void DamageEnemy()
    {
        enemyHealth -= 62;
        Debug.Log(this.gameObject.name + " health: " + enemyHealth);
        if(enemyHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer();
        }
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
