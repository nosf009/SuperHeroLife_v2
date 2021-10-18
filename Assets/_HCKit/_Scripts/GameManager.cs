using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HcKit
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager _Instance;
        public static GameManager Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = FindObjectOfType<GameManager>();
                return _Instance;
            }
        }
        #endregion

        [Header("Data / Game Profile")]
        public PlayerGameProfile activeProfile;

        [Header("Transitions")]
        public GameObject fadeOut;
        public GameObject fadeIn;

        //[Header("Scene sequencing")]
        //public List<int> gameSceneIndexes = new List<int>();

        private void Awake()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            // Uncomment for DDOL which works if dropped on scene that reloads etc
            if (_Instance == null)
            {
                this.transform.SetParent(null);
                DontDestroyOnLoad(this.gameObject);
                _Instance = Instance;
            }
            else
            {
                Destroy(this.gameObject);
            }
            InitGameData();
        }

        private void Start()
        {
            /*
            var numScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            //Debug.Log(numScenes);
            gameSceneIndexes.Clear();
            for (int i = 0; i < numScenes; ++i)
            {
                gameSceneIndexes.Add(i);
            }
            //LoadNextScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneIndexes[activeProfile.currentLevel]); // level 1 == index 1, _preload scene always 0
        */
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (fadeIn != null) { fadeIn.SetActive(false); }
            StartCoroutine(OnSceneLoadTransition());
            Debug.Log("OnSceneLoaded: " + scene.name);
        }

        public void LoadNextScene(string sceneName)
        {
            StartCoroutine(LoadSceneWithTransition(sceneName));
        }

        IEnumerator OnSceneLoadTransition()
        {
            if (fadeOut != null) { fadeOut.SetActive(true); }
            yield return new WaitForSeconds(0.5f);
            if (fadeOut != null) { fadeOut.SetActive(false); }
        }

        IEnumerator LoadSceneWithTransition(string sceneName)
        {
            if (fadeIn != null) { fadeIn.SetActive(true); }
            yield return new WaitForSeconds(0.5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); // level 1 == index 1, _preload scene always 0
        }

        #region Do NOT Modify

        // This is mandatory part, gets called in Awake to load up JSON cache/config
        // Instead of player prefs, JSON is used

        void InitGameData()
        {
            PlayerGameProfile currentGameData = null; // set as null first
            activeProfile.currentGameId = 999; // use dummy game ID
            currentGameData = DataManager.PlayerGameProfileData;
            if (currentGameData != null)
            {
                // if it exists from before, load up values
                Debug.Log("PlayerGameProfile found, loading values...");
                activeProfile = currentGameData;
            }
            else
            {
                activeProfile = new PlayerGameProfile();
                Debug.Log("PlayerGameProfile not found, creating one...");
                DataManager.PlayerGameProfileData = activeProfile; // save immediately
            }
        }

        #endregion

    }
}


