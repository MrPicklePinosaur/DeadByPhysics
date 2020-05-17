using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EventSystem;
using static RoomManager;
using static MainMenuManager;

public class CreateGameUI : MonoBehaviour {

    TMP_InputField newGameNameField;
    Button createNewGameButton;

    void Start() {

        newGameNameField = GetComponentInChildren<TMP_InputField>();
        createNewGameButton = GetComponentInChildren<Button>();

        createNewGameButton.onClick.AddListener(delegate {
            roomManager.CreateNewRoom(newGameNameField.text);
        });

    }


}
