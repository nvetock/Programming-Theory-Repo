using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hiscoreText;
    //[SerializeField] private TextMeshProUGUI
    //[SerializeField] private TextMeshProUGUI

    private void Awake()
    {
        hiscoreText.text = "HISCORE: " + GameManager.Instance.topScore.ToString();
    }

    private void Update()
    {
        
    }

    public void ContinueGame()
    {
        GameManager.Instance.LoadLastRun();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        //GameManager.Instance.SaveLastRun();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
