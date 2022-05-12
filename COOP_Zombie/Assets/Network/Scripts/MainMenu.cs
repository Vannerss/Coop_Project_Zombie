using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static int HostOrClient;
    //Host = 0
    //Client = 1

        public void Host()
    {
        HostOrClient = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
       
    }

    public void Client()
    {
        if (SessionManager.instance.PlayersInGame < 5)
        {
            HostOrClient = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
