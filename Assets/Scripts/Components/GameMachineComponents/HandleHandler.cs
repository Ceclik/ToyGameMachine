using Components.CharacterComponents;
using Interfaces;
using Services.GameMachineServices;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class HandleHandler : MonoBehaviour
    {
        public bool IsInPlayMode { get; private set; }
        private ActionTextHandler _actionText;
        private IHandleMover _handleMover;

        private void Start()
        {
            _handleMover = new HandleMovingService();
            GameObject character = GameObject.FindGameObjectWithTag("Player");
            _actionText = character.GetComponent<ActionTextHandler>();
        }

        private void Update()
        {
            if (IsInPlayMode)
            {
                _handleMover.MoveHandler(transform);
            }
            
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