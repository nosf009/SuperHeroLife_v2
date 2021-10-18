using System;
using Managers;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class UiChoices : MonoBehaviour
    {
        [Serializable]
        public class ChoiceButton
        {
            public TMP_Text Text;
            public Button Button;
        }
        
        public ChoiceButton[] ChoiceButtons;

        public void SetChoices(Choice choice)
        {
            for (var i = 0; i < choice.ChildChoices.Length && i < ChoiceButtons.Length; i++)
            {
                var childChoice = choice.ChildChoices[i];
                
                ChoiceButtons[i].Text.text = childChoice.Name;
                ChoiceButtons[i].Button.onClick.AddListener((() =>
                {
                    // invoke choice event
                    childChoice.OnSelected.Invoke();
                }));
            }
        }

        public void OnChoiceSelected(int index)
        {
            foreach (var choiceText in ChoiceButtons)
            {
                choiceText.Button.onClick.RemoveAllListeners();
            }
            
            // hide options
            UiManager.Instance.SetGroup(null);
            // set choice
            Managers.GameManager.Instance.SetChoice(index);
        }
    }
}