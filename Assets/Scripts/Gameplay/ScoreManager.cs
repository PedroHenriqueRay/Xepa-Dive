using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Config")]
    public int score;
    public int food;
    public int energy;
    public int money;
    [SerializeField] int multiplier;
    [SerializeField] int multiplierSecondsDefault;
    [SerializeField] int multiplierSeconds;

    [Header("UI")]
    [SerializeField] TMP_Text txtScore;
    [SerializeField] TMP_Text txtLevelTitle;
    [SerializeField] TMP_Text txtMultiplier;
    [SerializeField] GameObject levelCompleteMenu;
    [SerializeField] GameObject GameOverMenu;

    [Header("FX")]
    [SerializeField] GameObject heartsPrefab;
    [SerializeField] GameObject explosionPrefab;

    AudioManager audioManager;
    int playerLevel;

    private void Awake()
    {
        txtLevelTitle.text = StaticConfiguration.staticLevelTitle;
        audioManager = GameObject.Find("LevelManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        StartCoroutine(multiplierCounter());
    }
    public void Soma(string tag)
    {
        switch (tag)
        {
            case "Food":
                food += 100 * multiplier;
                break;
            case "SpoiledFood":
                energy += 100 * multiplier;
                break;
            case "Scrap":
                money += 100 * multiplier;
                break;
            default:
                print("Incorrect Tag");
                break;
        }

        multiplierSeconds = multiplierSecondsDefault;
        if(multiplier <= 4)
        {
            multiplier++;
        }
        score = food + energy + money;
        updateUi();
        spawnFx();
    }

    public void resetMultiplier()
    {
        multiplier = 1;
        multiplierSeconds = 0;
        updateUi();
    }

    void updateUi()
    {
        txtScore.text = score.ToString();
        txtMultiplier.text = "" + multiplier + "x";
    }

    void spawnFx()
    {
        Vector3 heartsPosition = new Vector3(0, 1.2f, 0);
        Vector3 explosionPosition = new Vector3(0, 1.2f, -1.088f);
        GameObject gameObject = Instantiate(heartsPrefab, heartsPosition, Quaternion.Euler(90f, 0, 0));
        Destroy(gameObject, 1.167f);
        GameObject gameObject1 = Instantiate(explosionPrefab, explosionPosition, Quaternion.Euler(90f, 0, 0));
        Destroy(gameObject1, 0.667f);
    }


    IEnumerator multiplierCounter()
    {
        while (true)
        {
            if (multiplierSeconds > 0)
            {
                yield return new WaitForSeconds(1f);
                multiplierSeconds -= 1;
                updateUi();
            }
            else
            {
                resetMultiplier();
                yield return null;
            }
        }

    }

    public void levelCompleteCheck()
    {
        GameObject go = GameObject.Find("Objetos");
        print(go.transform.childCount);
        if(go.transform.childCount <= 5)
        {
            if(score >= StaticConfiguration.staticLevelGoal)
            {
                audioManager.playLevelCompleteAudioClip();
                playerLevel = PlayerPrefs.GetInt("playerLevel");
                if(StaticConfiguration.staticLevel == playerLevel)
                {
                    playerLevel += 1;
                    PlayerPrefs.SetInt("playerLevel", playerLevel);
                }
                StartCoroutine(LevelComplete());
            }
            else
            {
                Time.timeScale = 0f;
                GameOverMenu.SetActive(true);
                audioManager.playGameOverAudioClip();
            }

        }
    }

    IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(1.167f);
        Time.timeScale = 0f;
        levelCompleteMenu.SetActive(true);
    }
}
