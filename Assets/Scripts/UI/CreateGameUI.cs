using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static EventSystem;

public class CreateGameUI : MonoBehaviour {

    TMP_InputField newGameNameField;
    Button createNewGameButton;

    void Start() {

        newGameNameField = GetComponentInChildren<TMP_InputField>();
        createNewGameButton = GetComponentInChildren<Button>();

        createNewGameButton.onClick.AddListener(delegate {
            eventSystem.CreateRoomEvent(newGameNameField.text); //possibly add some sort of room validity check
        });
    }

}
