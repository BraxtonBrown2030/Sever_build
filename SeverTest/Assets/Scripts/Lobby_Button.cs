using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Lobby_Button : NetworkBehaviour
{
       public Button readyButton;
       private NetworkVariable<int> readyPlayerCount = new NetworkVariable<int>(0);
       public bool isReady = false;
       public string sceneName;
   
       private void Start()
       {
           if (readyButton != null)
           {
               readyButton.onClick.AddListener(OnReadyButtonClicked);
               UpdateButtonVisual();
           }
           else
           {
               Debug.LogError("Ready Button is not assigned in the Inspector.");
           }
       }
   
       private void OnReadyButtonClicked()
       {
           if (IsClient)
           {
               isReady = !isReady;
               UpdateButtonVisual();
               SubmitReadyServerRpc();
           }
       }
   
       [ServerRpc(RequireOwnership = false)]
       private void SubmitReadyServerRpc(ServerRpcParams rpcParams = default)
       {
           readyPlayerCount.Value += isReady ? 1 : -1;
           CheckAllPlayersReady();
       }
   
       private void CheckAllPlayersReady()
       {
           if (readyPlayerCount.Value == NetworkManager.Singleton.ConnectedClients.Count)
           {
               ChangeSceneForAllPlayers();
           }
       }
   
       private void ChangeSceneForAllPlayers()
       {
           NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
       }
   
       private void UpdateButtonVisual()
       {
           if (readyButton != null)
           {
               TMP_Text buttonText = readyButton.GetComponentInChildren<TMP_Text>();
               if (buttonText != null)
               {
                   ColorBlock colors = readyButton.colors;
                   if (isReady)
                   {
                       colors.normalColor = Color.green;
                       buttonText.text = "Ready";
                       Debug.Log("Player is ready");
                   }
                   else
                   {
                       colors.normalColor = Color.red;
                       buttonText.text = "Not Ready";
                       Debug.Log("Player is not ready");
                   }
                   readyButton.colors = colors;
               }
               else
               {
                   Debug.LogError("TMP_Text component is not found in the Ready Button.");
               }
           }
           else
           {
               Debug.LogError("Ready Button is not assigned in the Inspector.");
           }
       }
   
       public void change_isreay()
       {
           isReady = !isReady;
           UpdateButtonVisual();
           SubmitReadyServerRpc();
       }
   
       public void quit_game()
       {
           Application.Quit();
       }
   }