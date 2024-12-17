using Components.GameMachineComponents;
using Interfaces;
using Services.CharacterServices;
using UnityEngine;

namespace Components.CharacterComponents
{
    public class CharacterMovementHandler : MonoBehaviour
    {
        [SerializeField] private float movingSpeed;
        private ICharacterMover _characterMover;
        private HandleHandler _handle;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _handle = GameObject.FindGameObjectWithTag("Handle").GetComponent<HandleHandler>();
            _rigidbody = GetComponent<Rigidbody>();
            _characterMover = new CharacterMovementService();
        }

        private void FixedUpdate()
        {
            if (!_handle.IsInPlayMode)
            {
                KeyCode keyCode;
                if (Input.GetKey(KeyCode.W))
                {
                    keyCode = KeyCode.W;
                    _characterMover.Move(_rigidbody, keyCode, movingSpeed);
                }

                if (Input.GetKey(KeyCode.A))
                {
                    keyCode = KeyCode.A;
                    _characterMover.Move(_rigidbody, keyCode, movingSpeed);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    keyCode = KeyCode.S;
                    _characterMover.Move(_rigidbody, keyCode, movingSpeed);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    keyCode = KeyCode.D;
                    _characterMover.Move(_rigidbody, keyCode, movingSpeed);
                }
            }
        }
    }
}