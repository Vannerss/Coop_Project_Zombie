using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SessionManager : Singleton<SessionManager>
{
    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();
    public static bool GameIsPaused = false;
    public GameObject Menu;

    public int PlayersInGame
    {
        get
        {
            return playersInGame.Value;
        }
    }

    private void Start()
    {
        if (MainMenu.Hostbool == true)
            Host();
        else if (MainMenu.Clientbool == true)
            Client();
        else
            Debug.Log("ERROR");
        

        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
                playersInGame.Value++;
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
                playersInGame.Value--;
        };
    }

    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        playersInGame.Value = 1;
    }

    public static void Client()
    {
        if (SessionManager.instance.PlayersInGame < 5)
        {
            Debug.Log("Client");
            NetworkManager.Singleton.StartClient();
        }
    }
    #region Functions
    private void AddClient(ulong id)
    {
        if (IsServer)
            playersInGame.Value++;
    }

    private void RemoveClient(ulong id)
    {
        if (IsServer)
            playersInGame.Value--;
    }
    #endregion
}
