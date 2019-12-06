using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour
{
    public AudioSource music;
    void Awake()
    {
        
        
        GameObject[] objs = GameObject.FindGameObjectsWithTag("BackgroundMusic");
        if (objs.Length > 1)
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        Scene nameScene = SceneManager.GetActiveScene();
        if (nameScene.name == "CreditsScreen")
            Destroy(this.gameObject);
        if (nameScene.name == "PlayingScene")
            Destroy(this.gameObject);
        if (nameScene.name == "Animation")
            Destroy(this.gameObject);

        music.volume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        
    }
}
