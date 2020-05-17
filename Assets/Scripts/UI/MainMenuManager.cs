using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using static EventSystem;

public class MainMenuManager : EventListener {

    public static MainMenuManager mainMenuManager;

    public GameObject currentFrame;

    //FIX THIS LATER!!!!!!!!!!
    public GameObject StartGameFrame;
    public GameObject RoomFrame;


    private void Start() {
        base.Start();

        mainMenuManager = this;
    }

    public void SwitchFrame(GameObject frame) {

        if (currentFrame != null) {
            currentFrame.SetActive(false);
        }

        frame.SetActive(true);
        currentFrame = frame;
    }

    public override void OnEvent(EventData data) {
        
        switch (data.Code) {
            case (byte)EventCodes.OnJoinedRoomEvent:
                SwitchFrame(RoomFrame);
                break;
            case (byte)EventCodes.OnLeftRoomEvent:
                SwitchFrame(StartGameFrame);
                break;
        }

    }



}
