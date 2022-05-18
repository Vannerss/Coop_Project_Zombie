using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundAndZombieCounter : MonoBehaviour
{
    private GameManager gameManager;
    public Text roundCounter;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        roundCounter.text = "Round: " + gameManager.gameRound.ToString();
    }
}
