using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Collections;
using TMPro;
using UnityEngine;

public class PlayerHUD : NetworkBehaviour
{
    private NetworkVariable<FixedString32Bytes> playersName = new NetworkVariable<FixedString32Bytes>();
    private bool overlaySet = false;
    public int totalplayers;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            playersName.Value = $"Player {OwnerClientId + 1}";
            setTotalPlayers(); 
            Debug.LogError("Raised to " + playersName.Value) ;
        }
    }

    public void SetOverlay()
    {
        var localPlayerOverlay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        localPlayerOverlay.text = playersName.Value.ToString();

        overlaySet = true; 
    }

    private void Update()
    {
        if(!overlaySet && !string.IsNullOrEmpty(playersName.Value.ToString()))
            SetOverlay();
    }

    private void setTotalPlayers()
    {
        totalplayers++;
    }

    public int getTotalPlayers()
    {
        return totalplayers;
    }
}
