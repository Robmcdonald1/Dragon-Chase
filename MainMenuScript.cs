using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject Play;
    public GameObject Credits;
    public GameObject SettingsMenu;

    public Slider SoundSlider;
    public Slider MusicSlider;

    public AudioSource BGM;

    void Start(){
        Play.SetActive(false);
        Credits.SetActive(false);
        SettingsMenu.SetActive(false);

        if(PlayerPrefs.GetInt("IsFirst") == 0){
            PlayerPrefs.SetInt("IsFirst", 1);
            PlayerPrefs.SetFloat("SoundVolume", 1f);
            PlayerPrefs.SetFloat("MusicVolume", 1f);
        }

        SoundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        BGM.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Play.SetActive(false);
            Credits.SetActive(false);
            SettingsMenu.SetActive(false);
        }
    }

    public void SoundSliderValueIsChanged(){
        PlayerPrefs.SetFloat("SoundVolume", SoundSlider.value);
    }

    public void MusicSliderValueIsChanged(){
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        BGM.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void ExitButtonClicked(){
        Application.Quit();
    }

    public void PlayButtonClicked(){
        Play.SetActive(true);
    }
    public void SettingsMenuButtonClicked(){
        SettingsMenu.SetActive(true);
    }
    public void CreditsButtonClicked(){
        Credits.SetActive(true);
    }
    public void EasyPlayButtonClicked(){
        PlayerPrefs.SetInt("currentLevel", 1);
        SceneManager.LoadScene("Game");
    }
    public void MediumPlayButtonClicked(){
        PlayerPrefs.SetInt("currentLevel", 2);
        SceneManager.LoadScene("Game");
    }
    public void HardPlayButtonClicked(){
        PlayerPrefs.SetInt("currentLevel", 3);
        SceneManager.LoadScene("Game");
    }
    public void UltimatePlayButtonClicked(){
        PlayerPrefs.SetInt("currentLevel", 4);
        SceneManager.LoadScene("Game");
    }
}
