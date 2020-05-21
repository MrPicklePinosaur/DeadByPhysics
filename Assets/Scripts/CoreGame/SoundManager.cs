using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using static EventSystem;

public class SoundManager : EventListener {

    public override void OnEvent(EventData data) {
        object[] payload = (object[])data.CustomData;

        switch (data.Code) {

            case (byte)EventCodes.OnSoundEvent:
                string sound_path = (string)payload[0];

                //play sound lmao
                SoundPlayer.quickStart(sound_path);

                break;
        }
    }

}
