using UnityEngine;
using UnityEngine.UI;

public class ReadyButtonTest : MonoBehaviour
{
    [SerializeField] private Button readyButton;
    
    private void Awake() {
        readyButton.onClick.AddListener(() => {
            RadyButtonSeversend.Instance.SetPlayerReady();
        });
    }

}
