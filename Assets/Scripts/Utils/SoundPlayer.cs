using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EventSystem;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour {

    public static float defaultVolume = 0.7f;
    private AudioSource audio_source;

    private bool sound_started;

    void Awake() {
        audio_source = GetComponent<AudioSource>();
    }

    void Update() {

        if (!audio_source.isPlaying && sound_started) {
            Destroy(this.gameObject);
        }
    }

    public void playSound(AudioClip sound) {
        playSound(sound, defaultVolume);
    }
    public void playSound(AudioClip sound, float volume) {
        audio_source.clip = sound;
        audio_source.volume = volume;
        audio_source.Play();
        sound_started = true;
    }

    public static void quickStart(string audiopath, float volume) { //shen we dont wanna do anything special with the audio object
        SoundPlayer sp = Instantiate(Resources.Load("SoundPlayer") as GameObject).GetComponent<SoundPlayer>();
        AudioClip clip = Instantiate(Resources.Load<AudioClip>(audiopath));
        sp.playSound(clip, volume);
    }
    public static void quickStart(string audiopath) {
        SoundPlayer.quickStart(audiopath, defaultVolume);
    }

    public static void quickRandomSound(string audiodir, float volume) {
        SoundPlayer sp = Instantiate(Resources.Load("SoundPlayer") as GameObject).GetComponent<SoundPlayer>();
        AudioClip[] clips = Resources.LoadAll<AudioClip>(audiodir);

        if (clips.Length == 0) { return; }
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        sp.playSound(clip, volume);
    }
    public static void quickRandomSound(string audiodir) {
        SoundPlayer.quickRandomSound(audiodir, defaultVolume);
    }

    public static void quickStartNetworked(string audiodir) {
        eventSystem.RaiseNetworkEvent(EventCodes.OnSoundEvent, new object[] { audiodir });
    }
}