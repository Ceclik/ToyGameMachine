using System;
using TMPro;
using UnityEngine;

namespace Components.CharacterComponents
{
    public class CoinsHandler : MonoBehaviour
    {
        [SerializeField] private int baseCoinsAmounts;
        [SerializeField] private TextMeshProUGUI coinsText;

        private int _coinsAmount;
        public int CoinsAmount
        {
            get => _coinsAmount;
            set
            {
                _coinsAmount = value;
                UpdateCoinsText();
            }
        }

        private void Start()
        {
            CoinsAmount = baseCoinsAmounts;
        }

        private void UpdateCoinsText()
        {
            coinsText.text = CoinsAmount.ToString();
        }
    }
}