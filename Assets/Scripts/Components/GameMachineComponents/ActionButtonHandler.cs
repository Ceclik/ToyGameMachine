using System;
using System.Collections;
using Components.CharacterComponents;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class ActionButtonHandler : MonoBehaviour
    {
        [SerializeField] private Light actionButtonLight;
        [SerializeField] private float lightTime;

        private ObjectsHandler _objectsHandler;
        private ActionTextHandler _actionText;

        public delegate void HandleActionButton();

        public event HandleActionButton OnActionButtonClick;

        private void Start()
        {
            GameObject character = GameObject.FindGameObjectWithTag("Player");
            _objectsHandler = character.GetComponent<ObjectsHandler>();
            _actionText = character.GetComponent<ActionTextHandler>();
        }

        public bool IsMoving { get; private set; }
        

        private void Update()
        {
            if (_actionText.IsTextShown && _objectsHandler.ObjectTransform.name == gameObject.name &&
                Input.GetKeyDown(KeyCode.F) && !IsMoving)
            {
                IsMoving = true;
                actionButtonLight.enabled = true;
                OnActionButtonClick?.Invoke();
                StartCoroutine(LightButton());
            }
        }

        private IEnumerator LightButton()
        {
            yield return new WaitForSeconds(lightTime);
            actionButtonLight.enabled = false;
            IsMoving = false;
        }
    }
}