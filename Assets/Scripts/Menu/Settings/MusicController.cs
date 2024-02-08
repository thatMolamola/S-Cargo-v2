using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is designed to allow the player to change the music track they are listening to
// and mute the music if so desired, all from the Settings scene.
public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button buttonMute;
    [SerializeField] private Text muteButtonText;
    [SerializeField] private Dropdown musicDropdown;
    [SerializeField] private AudioClip Track1, Track2; 

    void Start () {
        buttonMute.onClick.AddListener( () => {ChangeMusicState(); }  );
        musicDropdown.onValueChanged.AddListener(delegate {
            changeMusicTrack();});
    }
    
    private void ChangeMusicState() {
        if (GlobalControl.Instance.musicState) {
            muteButtonText.text = "Enable Music";
        } else {
            muteButtonText.text = "Disable Music";
        }
        GlobalControl.Instance.musicState = !GlobalControl.Instance.musicState;
        audioSource.mute = !audioSource.mute;
    }

    private void changeMusicTrack() {
        GlobalControl.Instance.musicTrack = musicDropdown.value;
        if (GlobalControl.Instance.musicTrack == 0) {
            audioSource.clip = Track1;
            audioSource.Play();
        } else {
            audioSource.clip = Track2;
            audioSource.Play();
        }
    }
}
