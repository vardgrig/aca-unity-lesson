using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.Game
{
    public class Timer : MonoBehaviour
    {
        private float _defaultValue;
        private float _current;

        private bool _isRunning;
        private bool _destroyOnComplete;

        public event Action OnStart;
        public event Action<float> OnTick;
        public event Action OnComplete;


        // private void Update()
        // {
        //     // 1 sec -> 60 fps
        //     // 5 sec -> ~300 fps
        //     if (!_isRunning)
        //     {
        //         return;
        //     }
        //
        //     _current -= Time.deltaTime;
        //     if (_current <= 0f)
        //     {
        //         _current = 0f;
        //         Complete();
        //     }
        //     Debug.Log("Update: " + _current);
        //
        //     OnTick?.Invoke(_current);
        // }

        private IEnumerator CountDownEnumerator()
        {
            float delta = 1f;
            while (_current > 0f)
            {
                yield return new WaitForSeconds(delta);
                Debug.Log("Enumerating: " + _current);
                _current -= delta;
                OnTick?.Invoke(_current);
            }

            if (_current <= 0f)
            {
                _current = 0f;
                Complete();
            }
        }

        private void Complete()
        {
            _isRunning = false;
            OnComplete?.Invoke();
            if (_destroyOnComplete)
            {
                Destroy(gameObject);
            }
        }

        public void Run(float value)
        {
            _defaultValue = value;
            ResetTimer();
            _isRunning = true;
            IEnumerator result = CountDownEnumerator();
            Coroutine coroutine = StartCoroutine(result);
            OnStart?.Invoke();
        }

        public void Stop()
        {
            _isRunning = false;
            ResetTimer();
        }

        public void ResetTimer()
        {
            _current = _defaultValue;
        }

        private void SetDestroyOnComplete(bool state)
        {
            _destroyOnComplete = state;
        }


        public static Timer CreateTimer(bool destroyOnComplete)
        {
            GameObject timerGameObject = new GameObject("Timer");
            Timer timer = timerGameObject.AddComponent<Timer>();
            timer.SetDestroyOnComplete(destroyOnComplete);
            return timer;
        }
    }
}