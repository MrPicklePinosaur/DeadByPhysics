using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour {

    public static PlayerProfile playerProfile;

    public string Name { get; private set; }
    public Player player;

    private void Awake() {
        DontDestroyOnLoad(this);
        PlayerProfile.playerProfile = this;
        Name = $"Player#{Random.Range(0,9999)}"; //TODO: MAKE AN ACTUAL PLAYER PROFILE LATER
        player = PhotonNetwork.LocalPlayer;
    }

}
