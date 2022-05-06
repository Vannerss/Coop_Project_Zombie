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

    private void Update()
    {
        playersInGameText.text = $"Players in game: {SessionManager.instance.PlayersInGame}";
    }

    public void Host()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void Client()
    {
        if(SessionManager.instance.PlayersInGame < 5)
        NetworkManager.Singleton.StartClient();
    }
}
