using Unity.Netcode;
using UnityEngine;

public class testrpcCalls : MonoBehaviour
{
    public NetworkVariable<int> number;

    [ServerRpc]
    public void playercheck()
    {
        number.Value += 1;
        Debug.Log(number);
    }

}
