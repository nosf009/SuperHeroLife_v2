using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class AnimationRandomizer : MonoBehaviour
    {
        [InfoBox("Set this to the number of animations")] [Range(1, 100)]
        public int Range = 1;

        [Range(0.1f, 5f)]
        public float UpdateInterval = 0.5f;

        public string AnimatorParameterName = "Random";

        public bool RandomizeSpeed;

        [ShowIf("RandomizeSpeed"), Range(0.1f, 0.5f)]
        public float RandomSpeedFactor = 0.2f;

        private Animator _animator;
        private WaitForSeconds _updateIntervalSeconds;

        private void Awake()
        {
            if (!TryGetComponent(out _animator))
            {
                throw new Exception("AnimationRandomizer needs an animator component");
            }

            // randomly change interval
            UpdateInterval += UpdateInterval * Random.value * 0.1f;
            
            GenerateRandomValue();
            
            _updateIntervalSeconds = new WaitForSeconds(UpdateInterval);
            StartCoroutine(Enumerator());
        }

        private IEnumerator Enumerator()
        {
            // wait a random amount of time to differentiate animation randomizers
            yield return new WaitForSeconds(Random.value * UpdateInterval);
            
            while (true)
            {
                GenerateRandomValue();
                yield return _updateIntervalSeconds;
            }
        }

        private void GenerateRandomValue()
        {
            _animator.SetInteger(AnimatorParameterName, Random.Range(0, Range));
            _animator.speed = 1f + Random.value * RandomSpeedFactor;
        }
    }
}