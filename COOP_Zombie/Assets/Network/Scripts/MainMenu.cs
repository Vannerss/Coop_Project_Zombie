using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static int HostOrClient;
    public static bool Hostbool = false;
    public static bool Clientbool = false;
    //Host = 0
    //Client = 1

    public void Host()
    {
        Hostbool = true;
        HostOrClient = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
       
    }

    public void Client()
    {
        if (SessionManager.instance.PlayersInGame < 5)
        {
            Debug.Log("Entered client button");
            Clientbool = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            HostOrClient = 1;
            SessionManager.Client();
           
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
