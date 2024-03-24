    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class Timer : MonoBehaviour
    {
        public TextMeshProUGUI whiteTimer_txt;
        public TextMeshProUGUI blackTimer_txt;
        public float maxTime = 600.0f;
        private float whiteCurrentTime;
        private float blackCurrentTime;
        private bool whiteTimerRunning = false;
        private bool blackTimerRunning = false;

        void Start()
        {
            whiteCurrentTime = maxTime;
            blackCurrentTime = maxTime;
        }

        void Update()
        {
            if (whiteTimerRunning)
            {
                whiteCurrentTime -= Time.deltaTime;

                if (whiteCurrentTime <= 0.0f)
                {
                    whiteCurrentTime = 0.0f;
                    Debug.Log("White Time's up!");
                }

                UpdateWhiteTimerUI();
            }

            if (blackTimerRunning)
            {
                blackCurrentTime -= Time.deltaTime;

                if (blackCurrentTime <= 0.0f)
                {
                    blackCurrentTime = 0.0f;
                    Debug.Log("Black Time's up!");
                }

                UpdateBlackTimerUI();
            }
        }

        void UpdateWhiteTimerUI()
        {
            int minutes = Mathf.FloorToInt(whiteCurrentTime / 60.0f);
            int seconds = Mathf.FloorToInt(whiteCurrentTime % 60.0f);
            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            whiteTimer_txt.text = timerString;
        }

        void UpdateBlackTimerUI()
        {
            int minutes = Mathf.FloorToInt(blackCurrentTime / 60.0f);
            int seconds = Mathf.FloorToInt(blackCurrentTime % 60.0f);
            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            blackTimer_txt.text = timerString;
        }

        // Method to start the timer for the white player
        public void StartWhiteTimer()
        {
            whiteTimerRunning = true;
        }

        // Method to start the timer for the black player
        public void StartBlackTimer()
        {
            blackTimerRunning = true;
        }

        // Method to stop the timer for the white player
        public void StopWhiteTimer()
        {
            whiteTimerRunning = false;
        }

        // Method to stop the timer for the black player
        public void StopBlackTimer()
        {
            blackTimerRunning = false;
        }
    }
