using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class RadyButtonSeversend : NetworkBehaviour
{

public static RadyButtonSeversend Instance { get; private set; }

public int scene;

public event EventHandler OnReadyChanged;


private Dictionary<ulong, bool> playerReadyDictionary;


private void Awake() {
    Instance = this;

    playerReadyDictionary = new Dictionary<ulong, bool>();
}


public void SetPlayerReady() {
    SetPlayerReadyServerRpc();
}

[ServerRpc(RequireOwnership = false)]
private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default) {
    SetPlayerReadyClientRpc(serverRpcParams.Receive.SenderClientId);

    playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;

    bool allClientsReady = true;
    foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds) {
        if (!playerReadyDictionary.ContainsKey(clientId) || !playerReadyDictionary[clientId]) {
            // This player is NOT ready
            allClientsReady = false;
            break;
        }
    }

    if (allClientsReady)
    {

        SceneManager.LoadScene(scene);

    }
}

[ClientRpc]
private void SetPlayerReadyClientRpc(ulong clientId) {
    playerReadyDictionary[clientId] = true;

    OnReadyChanged?.Invoke(this, EventArgs.Empty);
}


public bool IsPlayerReady(ulong clientId) {
    return playerReadyDictionary.ContainsKey(clientId) && playerReadyDictionary[clientId];
}

}