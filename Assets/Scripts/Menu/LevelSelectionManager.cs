using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Config")]
    [SerializeField] public int level;
    [SerializeField] public string levelTitle;
    [SerializeField] public string levelDescription;
    [SerializeField] public string levelGoalDescription;
    [SerializeField] public int levelGoal;
    [SerializeField] public int levelObjectsNumber;
    [SerializeField] public float levelTimeLimit;

    int playerLevel;
    GameObject levelUnavailable;
    GameObject levelAvailable;
    GameObject levelComplete;

    void Awake()
    {
        playerLevel = PlayerPrefs.GetInt("playerLevel");
        levelUnavailable = transform.Find("levelUnavailable").gameObject;
        levelAvailable = transform.Find("levelAvailable").gameObject;
        levelComplete = transform.Find("levelComplete").gameObject;
    }
    void Start()
    {
        if (level < playerLevel)
        {
            levelComplete.SetActive(true);
        }
        else if (level == playerLevel)
        {
            levelAvailable.SetActive(true);
        }
        else
        {
            levelUnavailable.SetActive(true);
        }
    }
}
