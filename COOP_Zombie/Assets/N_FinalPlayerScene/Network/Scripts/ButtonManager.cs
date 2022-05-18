using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playersInGameText;

    //private NetworkVariable<> playersName = new NetworkVariable<>();

    //[SerializeField]
    // private int 

    public static bool GameIsPaused = false;
    //public GameObject Menu;

    private void Start()
    {
        //Menu.SetActive(true);
        //Time.timeScale = 0f;
        //GameIsPaused = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                //Resume();
            }
            else
            {
                //Pause();
            }
        }

        playersInGameText.text = $"Players in game: {SessionManager.instance.PlayersInGame}";
    }

    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        Resume();
    }

    public void Client()
    {
        if(SessionManager.instance.PlayersInGame < 5)
        NetworkManager.Singleton.StartClient();
        Resume();
    }

    void Resume()
    {
        //Menu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        //Menu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
