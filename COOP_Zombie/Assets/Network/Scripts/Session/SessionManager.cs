using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SessionManager : Singleton<SessionManager>
{
    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();

    public int PlayersInGame
    {
        get
        {
            return playersInGame.Value;
        }
    }

    private void Start()
    {
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
