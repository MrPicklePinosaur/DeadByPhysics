using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour {

    public static EventSystem eventSystem;

    private void Awake() {
        EventSystem.eventSystem = this;
    }

    public event Action<string> onCreateRoomEvent;
    public void CreateRoomEvent(string name) {
        onCreateRoomEvent?.Invoke(name);
    }
}
