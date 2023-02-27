using System;
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

        private void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            _current -= Time.deltaTime;
            if (_current <= 0f)
            {
                _current = 0f;
                Complete();
            }

            OnTick?.Invoke(_current);
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