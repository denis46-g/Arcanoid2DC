using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    public GameDataScript gameData;

    public GameObject ScrollbarPhone;
    private bool CanTurnPhoneLoud = false;
    public GameObject ScrollbarSoundEffects;
    private bool CanTurnSoundEfectsLoud = false;

    //[SerializeField]
    public AudioSource audioSrcPhone;

    public void ButtonNewGamePressed()
    {
        Cursor.visible = false;
        gameData.Reset();
        gameData.Save();
        Time.timeScale = 1; 
        SceneManager.LoadScene("SampleScene");
    }
    public void ButtonExitPressed()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void ButtonThisGamePressed()
    {
        Cursor.visible = false;
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("MenuScene");
            Time.timeScale = 1;
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void TogglePhone()
    {
        CanTurnPhoneLoud = !CanTurnPhoneLoud;
        ScrollbarPhone.SetActive(CanTurnPhoneLoud);
    }

    public void ToggleSoundEffects()
    {
        CanTurnSoundEfectsLoud = !CanTurnSoundEfectsLoud;
        ScrollbarSoundEffects.SetActive(CanTurnSoundEfectsLoud);
    }

    public void AudioScrollPhone()
    {
        audioSrcPhone = Camera.main.GetComponent<AudioSource>();
        float volume = ScrollbarPhone.GetComponent<Scrollbar>().value;
        Camera.main.GetComponent<AudioSource>().volume = volume;
        audioSrcPhone.volume = volume;
        PlayerPrefs.SetFloat("PhoneVolume", audioSrcPhone.volume);
    }

    public void AudioScrollSoundEffects()
    {
        float volume = ScrollbarSoundEffects.GetComponent<Scrollbar>().value;
        var coef = volume / 0.5f;
        PlayerPrefs.SetFloat("SoundEffectsVolumeCoef", coef);
    }
    void Start()
    {
        try
        {
            Cursor.visible = true;
            foreach (var item in GameObject.FindGameObjectsWithTag("ThisButton"))
                item.SetActive(!gameData.is_reset);
        }
        catch (Exception e)
        {}
        
    }
}
