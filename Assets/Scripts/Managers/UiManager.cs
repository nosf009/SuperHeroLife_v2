using System;
using System.Collections;
using Models;
using NaughtyAttributes;
using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance { get; private set; }
        
        [BoxGroup("Groups")]
        public CanvasGroup OptionsGroup;
        [BoxGroup("Groups")]
        public CanvasGroup WinGroup;
        [BoxGroup("Groups")]
        public CanvasGroup FailGroup;

        public GameObject DialogBox;
        public TMP_Text DialogText;

        private CanvasGroup _activeGroup;

        private void Awake()
        {
            Instance = this;

            // hide all groups
            HideGroup(OptionsGroup);
        }

        public void SetGroup(CanvasGroup newGroup)
        {
            if (_activeGroup == newGroup)
                return;

            if (_activeGroup != null)
            {
                HideGroup(_activeGroup);
            }

            _activeGroup = newGroup;
            if (newGroup)
            {
                ShowGroup(newGroup);
            }
        }

        private void ShowGroup(CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(true);
        }

        private void HideGroup(CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(false);
        }

        public void ShowChoices(Choice choice)
        {
            SetGroup(OptionsGroup);
            var uiChoices = OptionsGroup.GetComponent<UiChoices>();
            uiChoices.SetChoices(choice);
        }

        public void ShowDialog(string text)
        {
            DialogText.text = text;
            
            if (_dialogCoroutine != null)
            {
                StopCoroutine(_dialogCoroutine);
            }
            
            _dialogCoroutine = StartCoroutine(FadeOutDialog());
        }

        private Coroutine _dialogCoroutine;
        
        private IEnumerator FadeOutDialog()
        {
            DialogBox.SetActive(true);
            yield return new WaitForSeconds(2f);
            DialogBox.SetActive(false);
        }
        
        public void HideChoices()
        {
            if (_activeGroup == OptionsGroup)
                SetGroup(null);
        }

        public void RetryClicked()
        {
            GameManager.Instance.Retry();
            //SceneManager.Instance.Signal_RestartGameLevel();
        }

        public void ResetClicked()
        {
            //GameManager.Instance.LoadLevelSelect();
            SceneManager.Instance.Signal_RestartGameLevel();
        }

        public void ContinueClicked()
        {
            //GameManager.Instance.LoadNextLevel();
            SceneManager.Instance.Signal_LoadNextGameLevel();
        }
    }
}