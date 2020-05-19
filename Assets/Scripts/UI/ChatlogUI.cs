using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

using static PlayerProfile;
using ExitGames.Client.Photon;
using static EventSystem;

public class ChatlogUI : EventListener {

    VerticalLayoutGroup messageWindow;
    TMP_InputField input;


    private void Start() {
        base.Start();

        messageWindow = GetComponentsInChildren<VerticalLayoutGroup>()[1];
        input = GetComponentInChildren<TMP_InputField>();

        input.onSubmit.AddListener(delegate {

            if (input.text.Length == 0) { return; }
            eventSystem.RaiseNetworkEvent(EventCodes.OnChatMessageEvent, new object[] { CreateTextMessage(input.text)});
            input.text = ""; //clear text
        });
    }

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {
            case (byte)EventCodes.OnChatMessageEvent:
                AddNewMessage((string)payload[0]);
                break;
        }

    }

    public void AddNewMessage(string message) {
        GameObject chatMessage = Instantiate(Resources.Load("UI/ChatMessage")) as GameObject;
        chatMessage.GetComponent<RectTransform>().SetParent(messageWindow.transform);

        chatMessage.GetComponentInChildren<TMP_Text>().text = message;
    }

    public static string CreateTextMessage(string message) {
        return $"{playerProfile.Name}: {message}";
    }
    
}
