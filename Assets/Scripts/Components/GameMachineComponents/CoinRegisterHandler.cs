using Components.CharacterComponents;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class CoinRegisterHandler : MonoBehaviour
    {
        public bool IsCoinThrown { get; set; }
        private ObjectsHandler _objectsHandler;
        private ActionTextHandler _actionText;
        private CoinsHandler _coins;

        private void Start()
        {
            GameObject character = GameObject.FindGameObjectWithTag("Player");
            _objectsHandler = character.GetComponent<ObjectsHandler>();
            _actionText = character.GetComponent<ActionTextHandler>();
            _coins = character.GetComponent<CoinsHandler>();
        }

        private void Update()
        {
            if (!IsCoinThrown && _objectsHandler.ObjectTransform != null &&
                _objectsHandler.ObjectTransform.gameObject.name == gameObject.name && _actionText.IsTextShown &&
                Input.GetKeyDown(KeyCode.F) && _coins.CoinsAmount > 0)
            {
                IsCoinThrown = true;
                _coins.CoinsAmount--;
                _actionText.HideActionText();
            }
        }
    }
}