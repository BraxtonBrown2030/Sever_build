using UnityEngine;
using UnityEngine.UI;

public class NameInputUI : MonoBehaviour
{   
    public InputField nameInputField;
    public Button confirmButton;

    private void Start()
    {
        confirmButton.onClick.AddListener(SetPlayerName);
    }

    private void SetPlayerName()
    {
        if (!string.IsNullOrEmpty(nameInputField.text))
        {
            PlayerNameHandler.LocalPlayerName = nameInputField.text;
        }
    }
}
