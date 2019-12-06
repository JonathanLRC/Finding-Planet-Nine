using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitCredits : MonoBehaviour
{
    public static exitCredits instance;
    public AudioSource music;
    void Start()
    {
        StartCoroutine(DelayLoad());
        Time.timeScale = 1f;
        music.volume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }
    
    IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(17);
        SceneManager.LoadScene("MainScreen");
    }
}
