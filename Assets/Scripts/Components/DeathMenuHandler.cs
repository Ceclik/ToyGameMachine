using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components
{
    public class DeathMenuHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;
        public int ToysAmount { get; set; }

        private int _highScore;

        public void OnRestartButtonClick()
        {
            //Time.timeScale = 1;
            SceneManager.LoadScene("Gameplay");
        }

        public void OnExitButtonClick()
        {
            Application.Quit();
        }

        private void Start()
        {
            _highScore = PlayerPrefs.GetInt("Highscore");

            if (ToysAmount > _highScore)
            {
                PlayerPrefs.SetInt("Highscore", ToysAmount);
            }

            highScoreText.text = $"Highscore: {PlayerPrefs.GetInt("Highscore")}";
            scoreText.text = $"Score: {ToysAmount}";
        }
    }
}