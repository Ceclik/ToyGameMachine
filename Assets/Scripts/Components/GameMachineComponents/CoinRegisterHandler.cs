using Components.CharacterComponents;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class CoinRegisterHandler : MonoBehaviour
    {
        public bool IsCoinThrown { get; set; }
        private ObjectsHandler _objectsHandler;
        private ActionTextHandler _actionText;
        
        public delegate void StartGame();
        public event StartGame OnCoinThrown;

        private void Start()
        {
            GameObject character = GameObject.FindGameObjectWithTag("Player");
            _objectsHandler = character.GetComponent<ObjectsHandler>();
            _actionText = character.GetComponent<ActionTextHandler>();
        }

        private void Update()
        {
            if (!IsCoinThrown && _objectsHandler.ObjectTransform != null &&
                _objectsHandler.ObjectTransform.gameObject.name == gameObject.name && _actionText.IsTextShown &&
                Input.GetKeyDown(KeyCode.F))
            {
                IsCoinThrown = true;
                _actionText.HideActionText();
                OnCoinThrown?.Invoke();
            }
        }
    }
}