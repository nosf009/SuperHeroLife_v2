using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Controllers
{
    public class ScaleAnimator : MonoBehaviour
    {
        public AnimationCurve SpawnAnimationCurve;
        public float Duration = 1f;
        public float RandomVariation = 0.5f;
        [Range(0, 1f)]
        public float Factor = 1f;

        public bool PlayOnEnabled;
        
        private Coroutine _spawnAnimationCoroutine;
        private Vector3 _initialScale;
        
        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        private void OnEnable()
        {
            if (PlayOnEnabled)
                Play();
        }

        public void Play(float scale = 1f)
        {
            Stop();
            _spawnAnimationCoroutine = StartCoroutine(SpawnAnimationEnumerator(scale));
        }

        public void Stop()
        {
            if (_spawnAnimationCoroutine != null)
            {
                StopCoroutine(_spawnAnimationCoroutine);
                _spawnAnimationCoroutine = null;
            }
        }
        
        private IEnumerator SpawnAnimationEnumerator(float scale)
        {
            var duration = Duration + Random.value * RandomVariation;

            var startTime = Time.time;
            var endTime = startTime + duration;

            while (Time.time < endTime)
            {
                var t = (Time.time - startTime) / duration;
                transform.localScale = Vector3.Lerp(_initialScale, _initialScale * SpawnAnimationCurve.Evaluate(t), Factor * scale);

                yield return null;
            }

            transform.localScale = _initialScale;

            _spawnAnimationCoroutine = null;
        }
    }
}