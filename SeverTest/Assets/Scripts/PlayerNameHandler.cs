using Unity.Netcode;
using UnityEngine;

public class PlayerNameHandler : NetworkBehaviour
{
    public static string LocalPlayerName = "Player"; // Default local name before setting

    private NetworkVariable<string> playerName = new NetworkVariable<string>(
        "RandomThinkingButton", 
        NetworkVariableReadPermission.Everyone, 
        NetworkVariableWritePermission.Owner
    );

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            SetPlayerName(LocalPlayerName);
        }
    }

    [ServerRpc]
    private void SetPlayerNameServerRpc(string newName)
    {
        playerName.Value = newName;
    }

    public void SetPlayerName(string newName)
    {
        if (IsOwner)
        {
            SetPlayerNameServerRpc(newName);
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Return)) // Example key to confirm name change
        {
            SetPlayerName(LocalPlayerName);
        }
    }
}
