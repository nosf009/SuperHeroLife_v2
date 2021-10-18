using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Scenario", menuName = "SuperheroLife/Scenario", order = 0)]
    public class Scenario : ScriptableObject
    {
        public Choice ChoiceTree;
    }
}