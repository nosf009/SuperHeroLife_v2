using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    #region Singleton
    private static SceneManager _Instance;
    public static SceneManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<SceneManager>();
            return _Instance;
        }
    }
    #endregion

    void Start()
    {
        
    }

    /*
    public void Signal_LoadLevelMiniGame()
    {
        Debug.Log("LoadLevelMiniGame() Fired...");
        GameManager.Instance.LoadNextScene(LevelManager.Instance.currentGameLevel.miniGameScene);
    }
    */

    /*
    public void Signal_LoadLevelWinScene()
    {
        Debug.Log("LoadLevelWinScene() Fired...");
        GameManager.Instance.LoadNextScene(LevelManager.Instance.winLevelScene);
    }
    */

    public void Signal_RestartGameLevel()
    {
        Debug.Log("RestartGameLevelSignal() Fired...");
        HcKit.GameManager.Instance.LoadNextScene(LevelManager.Instance.currentGameLevel.introScene);
    }
    

    public void Signal_LoadNextGameLevel()
    {
        Debug.Log("MoveToNextGameLevelSignal() Fired...");
        LevelManager.Instance.IncreaseCurrentLevel();
        HcKit.GameManager.Instance.LoadNextScene(LevelManager.Instance.currentGameLevel.introScene);
    }

}
