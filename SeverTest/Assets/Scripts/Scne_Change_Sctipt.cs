using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class Scne_Change_Sctipt : NetworkBehaviour
{
    
    public void Change_Scene(int sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void Change_Scene_Multyplayer(string sceneName)
    {
        if (IsServer)
        {
            NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
            
    }
    
}
