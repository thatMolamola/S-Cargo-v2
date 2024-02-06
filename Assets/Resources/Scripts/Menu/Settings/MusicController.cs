using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is designed to allow the player to change the music track they are listening to
// and mute the music if so desired, all from the Settings scene.
public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    private Button buttonMute;
    private Text muteButtonText;
    private GlobalControl globalController;
    private Dropdown musicDropdown;
    public AudioClip Track1;
    public AudioClip Track2; 

    void Start () {
        audioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        buttonMute = GameObject.Find("MuteButton").GetComponent<Button>();
        muteButtonText = GameObject.Find("MuteText").GetComponent<Text>();
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
        musicDropdown = GameObject.Find("MusicTracks").GetComponent<Dropdown>();
        buttonMute.onClick.AddListener( () => {ChangeMusicState(); }  );
        musicDropdown.onValueChanged.AddListener(delegate {
            changeMusicTrack();});
    }
    
    void ChangeMusicState() {
        if (globalController.musicState) {
            muteButtonText.text = "Enable Music";
        } else {
            muteButtonText.text = "Disable Music";
        }
        globalController.musicState = !globalController.musicState;
        audioSource.mute = !audioSource.mute;
    }

    public void changeMusicTrack() {
        globalController.musicTrack = musicDropdown.value;
        if (globalController.musicTrack == 0) {
            audioSource.clip = Track1;
            audioSource.Play();
        } else {
            audioSource.clip = Track2;
            audioSource.Play();
        }
    }
}
