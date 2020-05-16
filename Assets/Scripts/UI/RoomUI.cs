using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using static RoomManager;


public class RoomUI : MonoBehaviour {

    TMP_Text roomNameText;
    VerticalLayoutGroup connectedPlayers;
    Button readyButton;

    private void Start() {
        roomNameText = GetComponentInChildren<TMP_Text>();
        connectedPlayers = GetComponentInChildren<VerticalLayoutGroup>();
        readyButton = GetComponentInChildren<Button>();



        //NOTE: in the future, make it so all players must be ready before we start game
        readyButton.onClick.AddListener(delegate {
            roomManager.StartGame();
        });


    }

    private void Update() {

        //VERY, VERY, VERY BAD FOR NOW
        if (roomManager.currentRoom != null ) {
            roomNameText.text = roomManager.currentRoom?.Name;
            DisplayConnectedPlayers();
        }

    }

    //TODO: possibly improve this in the future, where we dont have to refresh the whole list everytime its updated
    void DisplayConnectedPlayers() {
        var players = roomManager.currentRoom.Players;

        //clear children 
        foreach (Transform child in connectedPlayers.transform) {
            Destroy(child.gameObject);
        }

        //repopulate
        foreach (var player in players.Values) {

            GameObject connectedPlayerElement = Instantiate(Resources.Load("UI/ConnectedPlayerElement")) as GameObject;
            connectedPlayerElement.GetComponent<RectTransform>().SetParent(connectedPlayers.transform);

            //set text as playername
            connectedPlayerElement.GetComponentInChildren<TMP_Text>().text = player.NickName;

        }
    }
}
