using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EventSystem;

public class Generator : InteractableObject {

    public override void OnInteract(int actorId) {
        eventSystem.RaiseNetworkEvent(EventCodes.OnOpenGeneratorWindowEvent, new object[] { interactable_id, actorId });
    }

    public override void OnUninteract(int actorId) {
        eventSystem.RaiseNetworkEvent(EventCodes.OnCloseGeneratorWindowEvent, new object[] { interactable_id, actorId });

    }

}
