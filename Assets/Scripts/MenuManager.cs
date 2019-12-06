using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void loadScene(string scene)
    {
        if((scene == "LevelSelectionScreen") && (!PlayerPrefs.HasKey("TutorialPlayed")))
        {
            PlayerPrefs.SetInt("TutorialPlayed", 1);
            loadLevel(0);
        }
        else
        {
            Debug.Log("Loading Scene " + scene);
            DataBase.ins.XmlDataBase.gameDB.status = -1;
            SceneManager.LoadScene(scene);
        }
    }
    public void loadLevel(int level)
    {
        DataBase.ins.XmlDataBase.gameDB.status = level;
        SceneManager.LoadScene("PlayingScene");
    }
    public void reloadLevel()
    {
        int level = DataBase.ins.XmlDataBase.gameDB.status;
        if(1 <= level && level <= 9)
        {
            Debug.Log("Reloading level " + level);
            loadLevel(level);
        }
        else
        {
            Debug.Log("Cannot reload level " + level);
            loadScene("MainScreen");
        }
    }
    public void nextLevel()
    {
        int nextLevel = DataBase.ins.XmlDataBase.gameDB.status+1;
        if (1 <= nextLevel && nextLevel <= 9)
        {
            Debug.Log("Loading level " + nextLevel);
            loadLevel(nextLevel);
        }
        else if(nextLevel == 10)
        {
            loadScene("CreditsScreen");
        }
        else
        {
            Debug.Log("Cannot load level " + nextLevel);
            loadScene("MainScreen");
        }
    }
    public void resetGame()
    {
        PlayerPrefs.DeleteKey("TutorialPlayed");
        PlayerPrefs.DeleteKey("AnimationPlayed");
        DataBase.ins.XmlDataBase.gameDB.score = 0;
        DataBase.ins.SaveGameData();
        loadScene("MainScreen");
    }
    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    void Awake()
    {
        if(!PlayerPrefs.HasKey("AnimationPlayed"))
        {
            if(SceneManager.GetActiveScene().name == "Animation")
            {
                StartCoroutine(PlayAnimation());
            }
            else
            {
                loadScene("Animation");
            }
        }
    }
    
    IEnumerator PlayAnimation()
    {
        PlayerPrefs.SetInt("AnimationPlayed", 1);
        GameObject camera = GameObject.Find("Main Camera");
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.url = Application.dataPath + "/StreamingFiles/Animation.mp4";
        videoPlayer.Prepare();

        // Silenciar música

        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.1f);
        }
        while (true)
        {
            long playerCurrentFrame = videoPlayer.frame;
            long playerFrameCount = Convert.ToInt64(videoPlayer.frameCount);
            if(playerCurrentFrame+1 == playerFrameCount)
            {
                break;
            }
            //Debug.Log(playerCurrentFrame + " < " + playerFrameCount);
            yield return new WaitForSeconds(1f);
        }

        // No silenciar música

        loadScene("MainScreen");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loadScene("MainScreen");
        }
    }
}