using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    float timeLimit;
    [SerializeField] TMP_Text txtTime;
    [SerializeField] Image img_Circle_fill;


    ScoreManager scoreManager;
    float levelSecondsInitial;
    float fill;

    [SerializeField] GameObject gameOverMenu;
    AudioManager audioManager;

    private void Awake()
    {
        timeLimit = StaticConfiguration.staticLevelTimeLimit;
        scoreManager = GameObject.Find("LevelManager").GetComponent<ScoreManager>();
        audioManager = GameObject.Find("LevelManager").GetComponent<AudioManager>();

    }
    private void Start()
    {
        StartCoroutine(Counter());
        levelSecondsInitial = timeLimit;
        img_Circle_fill.fillAmount = 1;
    }


    void levelTimeManager()
    {
        if (timeLimit > 0)
        {
            timeLimit -= 1;
            fill = timeLimit / levelSecondsInitial;
            img_Circle_fill.fillAmount = fill;
            txtTime.text = timeLimit.ToString();
        }
        else
        {
            Time.timeScale = 0f;
            gameOverMenu.SetActive(true);
            audioManager.playGameOverAudioClip();
        }
    }

    IEnumerator Counter()
    {
        yield return new WaitForSeconds(1f);
        levelTimeManager();
        StartCoroutine(Counter());
    }
}
