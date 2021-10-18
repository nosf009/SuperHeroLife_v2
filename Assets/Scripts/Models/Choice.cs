using System;
using NaughtyAttributes;
using ScriptableObjects;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace Models
{
    [Serializable]
    public class Choice
    {
        // public enum ChoiceEffect
        // {
        //     None = 0,
        //     Win = 1,
        //     Fail = 2
        // }

        public string Name;
        
        /// <summary>
        /// Animated scene to play
        /// </summary>
        public PlayableAsset TimelineScene;

        // /// <summary>
        // /// Effect of this choice
        // /// </summary>
        // public ChoiceEffect EffectOnSelected;
        
        /// <summary>
        /// Scenario to use on fail
        /// </summary>
        [FormerlySerializedAs("Scenario")]
        public Scenario OverrideScenario;
        
        /// <summary>
        /// Events to call when choice selected
        /// </summary>
        public UnityEvent OnSelected;
        
        /// <summary>
        /// Nested choices
        /// </summary>
        public Choice[] ChildChoices;
    }
}