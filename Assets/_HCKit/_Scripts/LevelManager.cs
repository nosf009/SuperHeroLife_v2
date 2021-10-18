using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    #region Singleton
    private static LevelManager _Instance;
    public static LevelManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<LevelManager>();
            return _Instance;
        }
    }
    #endregion

    [Header("Level Data")]
    public int currentLevel; // current level number, accessible via Singleton
    //public Transform levelDataParent; // parent transform for levels
    [Header("Level Debug")]
    public GameLevel currentGameLevel;

    [Header("Level Config")]
    //public string winLevelScene;
    public List<GameLevel> gameLevels = new List<GameLevel>();

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }

    private void Init()
    {
        currentLevel = HcKit.GameManager.Instance.activeProfile.currentLevel;
        currentGameLevel = gameLevels[currentLevel - 1]; // level starting from 1, list from index 0, hence -1
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == currentGameLevel.introScene) { return; } // avoid loop
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentGameLevel.introScene); // always continue from intro scene
    }

    public void IncreaseCurrentLevel()
    {
        currentLevel += 1; // update local level reference
        Debug.Log("currentLevel = " + currentLevel);
        if (currentLevel > gameLevels.Count)
        {
            currentLevel = 1; // check then reloop back
        }
        currentGameLevel = gameLevels[currentLevel - 1]; // set the currentlevel
        HcKit.GameManager.Instance.activeProfile.currentLevel = currentLevel; // update active profile level reference
        DataManager.PlayerGameProfileData = HcKit.GameManager.Instance.activeProfile; // and save to json because in JSON current level is stored
    }

}

[System.Serializable]
public class GameLevel
{
    [Header("Starting scene")]
    public string introScene;
    //[Header("Minigame scene")]
    //public string nextScene;
}