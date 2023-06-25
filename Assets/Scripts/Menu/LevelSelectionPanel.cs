using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectionPanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text txtLevelTitle;
    [SerializeField] TMP_Text txtLevelDescription;
    [SerializeField] TMP_Text txtLevelGoalDescription;
    [SerializeField] Button btnPlay;

    int playerLevel;
    // Start is called before the first frame update

    private void OnEnable()
    {
        playerLevel = PlayerPrefs.GetInt("playerLevel");

        if (StaticConfiguration.staticLevel <= playerLevel)
        {
            btnPlay.interactable = true;
        }
        else
        {
            btnPlay.interactable = false;
        }
        txtLevelTitle.text = StaticConfiguration.staticLevelTitle;
        txtLevelDescription.text = StaticConfiguration.staticLevelDescription;
        txtLevelGoalDescription.text = StaticConfiguration.staticLevelGoalDescription;
    }
}
