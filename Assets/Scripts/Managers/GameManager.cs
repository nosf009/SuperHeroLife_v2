using Models;
using NaughtyAttributes;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Scenario Scenario;
        public Choice CurrentChoice { get; private set; }

        public string NextSceneName;

        private PlayableDirector _director;

        private void Awake()
        {
            Instance = this;
            _director = FindObjectOfType<PlayableDirector>();
        }

        private void Start()
        {
            SetChoice(Scenario.ChoiceTree);
        }

        public void SetChoice(int index)
        {
            // get choice at index
            SetChoice(CurrentChoice.ChildChoices[index]);
        }

        private void SetChoice(Choice currentChoice)
        {
            CurrentChoice = currentChoice;
            
            if (currentChoice != null && currentChoice.TimelineScene)
            {
                _director.Stop();
                _director.Play(currentChoice.TimelineScene);
            }
        }

        public void ShowChoices()
        {
            UiManager.Instance.ShowChoices(CurrentChoice);
        }

        public void Win()
        {
            UiManager.Instance.SetGroup(UiManager.Instance.WinGroup);
        }

        public void Fail()
        {
            UiManager.Instance.SetGroup(UiManager.Instance.FailGroup);
        }

        public void Retry()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneManagerOnsceneLoaded;
        }

        public void LoadLevelSelect()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
        }

        [Button]
        public void LoadNextLevel()
        {
            if (string.IsNullOrEmpty(NextSceneName))
            {
                Debug.Log("Cannot start next scene - scene name is not set, resetting scene");
                LoadLevelSelect();
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName);
        }

        private void OnSceneManagerOnsceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // iterate root scene gameobjects and find game manager
            foreach (var go in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (go.TryGetComponent<GameManager>(out var gameManager))
                {
                    // set game manager scenario if one is provided
                    if (CurrentChoice.OverrideScenario) gameManager.Scenario = CurrentChoice.OverrideScenario;

                    break;
                }
            }

            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneManagerOnsceneLoaded;
        }
    }
}
