using Components.CharacterComponents;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class HandleHandler : MonoBehaviour
    {
        public bool IsInPlayMode { get; private set; }
        private ActionTextHandler _actionText;

        private void Start()
        {
            GameObject character = GameObject.FindGameObjectWithTag("Player");
            _actionText = character.GetComponent<ActionTextHandler>();
        }

        private void Update()
        {
            if (!IsInPlayMode && Input.GetKeyDown(KeyCode.Mouse0) && _actionText.IsTextShown)
            {
                IsInPlayMode = true;
                _actionText.ShowHandleMoveText(IsInPlayMode);
            }
            else if (IsInPlayMode && Input.GetKeyDown(KeyCode.Mouse1) && _actionText.IsTextShown)
            {
                IsInPlayMode = false;
                _actionText.ShowHandleMoveText(IsInPlayMode);
            }
        }
    }
}