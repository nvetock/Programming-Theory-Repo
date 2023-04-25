using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MainUIHandler : MonoBehaviour
{
    public static MainUIHandler Instance;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI pauseScoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;


    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hiScoreText;


    [SerializeField] private float totalTime;
    [SerializeField] private int currentScore;
    [SerializeField] private int topScore;
    [SerializeField] private bool isPaused = false;
    [SerializeField] private bool m_GameOver;



    //[SerializeField] private int placeholder;


    [SerializeField] private GameObject shot1;
    [SerializeField] private GameObject shot2;
    [SerializeField] private GameObject shot3;
    [SerializeField] private GameObject shot4;
    [SerializeField] private GameObject shot5;
    [SerializeField] private GameObject shot6;

    //Game Functionality Variables






    private void Awake()
    {
        Instance = this;
        StartCoroutine(GameTimeCountdown());
        currentScore = 0;
        scoreText.text = currentScore.ToString();
        m_GameOver = false;
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        hiScoreText.SetActive(false);
    }

    private void Start()
    {
        topScore = GameManager.Instance.topScore;
    }

    private void Update()
    {
        int ammo = GameObject.Find("Player").GetComponent<PlayerController>().curAmmo;

        if (totalTime <= 0f)
        {
            Debug.Log("Game Over! Ran out of Time.");
            GameOver();
        }

        BulletShotUI(ammo);
        HandlePause();
    }



    //
    //
    // GAME FUNCTIONALITY
    //
    //


    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void GameOver()
    {
        GameManager.Instance.SaveLastRun();

        m_GameOver = true;
        gameOverScoreText.text = currentScore.ToString() + " points";
        gameOverMenu.SetActive(true);
        if(currentScore > topScore)
        {
            UpdateHiScore(currentScore);
            hiScoreText.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        pauseScoreText.text = currentScore.ToString() + " | " + Mathf.Round(totalTime).ToString() + "sec";
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MenuReturn()
    {
        GameManager.Instance.SaveLastRun();

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        GameManager.Instance.LoadLastRun();
    }

    public void Exit()
    {
        GameManager.Instance.SaveLastRun();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }




    //
    //
    //  TIMER LOGIC & MATH LOGIC
    //
    //


IEnumerator GameTimeCountdown()
    {
        float duration = 0f;
        totalTime = 60f;
        while(totalTime >= duration)
        {
            totalTime -= Time.deltaTime;
            timerText.text = Mathf.Round(totalTime).ToString();
            m_GameOver = false;

            yield return null;
        }
    }

    public void AddScore(int pointValue)
    {
        currentScore += pointValue;
        scoreText.text = currentScore.ToString();
    }

    int UpdateHiScore(int point)
    {
        topScore = point;
        GameManager.Instance.topScore = topScore;

        return topScore;
    }

    public void BulletShotUI(int remainingShots)
    {
        //int remainingShots = BulletPool.SharedInstance.amountToPool;

        if (remainingShots > 0)
        {
            shot1.SetActive(true);
        }
        else
        {
            shot1.SetActive(false);
        }
        if (remainingShots > 1)
        {
            shot2.SetActive(true);
        }
        else
        {
            shot2.SetActive(false);
        }
        if (remainingShots > 2)
        {
            shot3.SetActive(true);
        }
        else
        {
            shot3.SetActive(false);
        }
        if (remainingShots > 3)
        {
            shot4.SetActive(true);
        }
        else
        {
            shot4.SetActive(false);
        }
        if (remainingShots > 4)
        {
            shot5.SetActive(true);
        }
        else
        {
            shot5.SetActive(false);
        }
        if (remainingShots > 5)
        {
            shot6.SetActive(true);
        }
        else
        {
            shot6.SetActive(false);
        }
    }
}