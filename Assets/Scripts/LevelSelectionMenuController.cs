using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenuController : MonoBehaviour
{
    public int score;
    public GameObject menuPanel;
    public Button[] LevelButtons = new Button[9];
    void Start()
    {
        menuPanel = transform.Find("MenuPanel").gameObject;
        for (int i = 0; i < 9; i++)
        {
            LevelButtons[i] = menuPanel.transform.Find("ButtonLvl"+(i+1)).GetComponent<Button>();
            LevelButtons[i].interactable = false;
        }
        UnlockLevels();
    }

    void UnlockLevels()
    {
        score = DataBase.ins.XmlDataBase.gameDB.score;
        LevelButtons[0].interactable = true;
        for (int i = 1; i < 9; i++)
        {
            if (i >= score) break;
            LevelButtons[i].interactable = true;
        }
    }
}
