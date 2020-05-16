using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static RoomManager;

public class JoinPrivateGameUI : MonoBehaviour {

    TMP_InputField privateGameCodeField;
    Button joinGameButton;

    void Start() {

        privateGameCodeField = GetComponentInChildren<TMP_InputField>();
        joinGameButton = GetComponentInChildren<Button>();

        joinGameButton.onClick.AddListener(delegate {
            roomManager.JoinRoom(privateGameCodeField.text);
        });

    }

}
