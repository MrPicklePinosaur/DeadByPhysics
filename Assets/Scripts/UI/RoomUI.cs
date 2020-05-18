using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using static RoomManager;
using ExitGames.Client.Photon;
using static EventSystem;
using static MainMenuManager;
using static PlayerProfile;

public class RoomUI : MonoBehaviour {


    //ui stuff
    TMP_Text roomNameText;
    VerticalLayoutGroup connectedPlayers;
    Button startButton;
    Button leaveButton;

    private void Start() {

        roomNameText = GetComponentInChildren<TMP_Text>();
        connectedPlayers = GetComponentInChildren<VerticalLayoutGroup>();
        startButton = GetComponentsInChildren<Button>()[0];
        leaveButton = GetComponentsInChildren<Button>()[1];

        startButton.onClick.AddListener(delegate {
            roomManager.StartGame();
        });
        leaveButton.onClick.AddListener(delegate {
            roomManager.LeaveRoom();
        });

    }

    private void Update() {

        //VERY, VERY, VERY BAD FOR NOW
        if (roomManager.currentRoom != null ) {
            roomNameText.text = roomManager.currentRoom?.Name;
            DisplayConnectedPlayers();

            //if there are 5 players in lobby, ungrey out the button
            startButton.interactable = (roomManager.currentRoom.PlayerCount >= 1);

            //if user is not master client, disable button
            startButton.gameObject.SetActive(playerProfile.player.IsMasterClient);
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
