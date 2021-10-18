using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;


namespace HCFW
{
    public class ServicesManager : MonoBehaviour
    {
        #region Singleton
        private static ServicesManager _Instance;
        public static ServicesManager Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = FindObjectOfType<ServicesManager>();
                return _Instance;
            }
        }
        #endregion

        private void Awake()
        {
            /*
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
            */
            
            if (!FB.IsInitialized)
            {
                // Initialize the Facebook SDK
                FB.Init(InitCallback, OnHideUnity);
            }
            else
            {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
        }

        private void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void Start()
        {
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

        }

        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                // Signal an app activation App Event
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            if (!isGameShown)
            {
                // Pause the game - we will need to hide
                Time.timeScale = 0;
            }
            else
            {
                // Resume the game - we're getting focus again
                Time.timeScale = 1;
            }
        }

        public void LogDesignEvent(string level, string status)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Level", level);
            parameters.Add("Status", status);
        }
        
    }

}

[System.Serializable]
public class RvConfig
{
    public string placementName;
    public string rvIdAndroid;
    public string rvIdIos;
}

[System.Serializable]
public class ActiveRvConfig
{
    public string placementName;
    public string rvId;
}