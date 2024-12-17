using Interfaces;
using Services.CharacterServices;
using TMPro;
using UnityEngine;

namespace Components.CharacterComponents
{
    public class ActionTextHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI actionText;
        private IActionTextHandler _actionTextHandler;
        
        public TextMeshProUGUI ActionText => actionText;
        public bool IsTextShown { get; private set; }

        private void Start()
        {
            _actionTextHandler = new ActionTextHandlingService();
        }

        public void ShowActionText(bool isObjectPicked)
        {
            _actionTextHandler.HandleActionText(actionText, isObjectPicked);
            IsTextShown = true;
        }

        public void ShowThrowCoinText()
        {
            IsTextShown = true;
            _actionTextHandler.ShowThrowCoinText(actionText);
        }

        public void HideActionText()
        {
            actionText.gameObject.SetActive(false);
            IsTextShown = false;
        }
    }
}   