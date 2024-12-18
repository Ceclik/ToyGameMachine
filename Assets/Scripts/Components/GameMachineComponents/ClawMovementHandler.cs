using System;
using System.Collections;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class ClawMovementHandler : MonoBehaviour
    {
        [SerializeField] private float movingSpeed;
        [SerializeField] private float fallingSpeed;
        [SerializeField] private float lowPositionStayingDelay;

        [Space(10)] [Header("Limit position values")] 
        [SerializeField] private float minXValue;
        [SerializeField] private float maxXValue;
        [SerializeField] private float minZValue;
        [SerializeField] private float maxZValue;

        [Space(10)] [Header("Up and down limits")] 
        [SerializeField] private float maxYPosition;
        [SerializeField] private float minYPosition; 
 
        private HandleHandler _handle;
        private ActionButtonHandler _actionButton;
        private bool _isCatching;
        private bool _hasFallen;
        private bool _isRising;

        private void Start()
        {
            _actionButton = GameObject.FindGameObjectWithTag("Button").GetComponent<ActionButtonHandler>();
            _handle = GameObject.FindGameObjectWithTag("Handle").GetComponent<HandleHandler>();

            _actionButton.OnActionButtonClick += CatchToy;
        }

        private void CatchToy()
        {
            _isCatching = true;
        }

        private IEnumerator CatchDelayed()
        {
            yield return new WaitForSeconds(lowPositionStayingDelay);
            _isRising = true;
        }

        private void FixedUpdate()
        {
            if (_isCatching)
            {
                if (!_hasFallen)
                {
                    transform.Translate(new Vector3(0.0f, -Time.deltaTime * fallingSpeed));
                    if (transform.position.y <= minYPosition)
                    {
                        _hasFallen = true;
                        StartCoroutine(CatchDelayed());
                    }
                }
                else if(_isRising)
                {
                    transform.Translate(new Vector3(0.0f, Time.deltaTime * fallingSpeed));
                    if(transform.position.y >= maxYPosition)
                    {
                        _isCatching = false;
                    }
                }
            }
            
            
            float translation = Time.deltaTime * movingSpeed;
            if (_handle.IsInPlayMode && !_isCatching)
            {
                if (Input.GetKey(KeyCode.W) && transform.position.x + translation < maxXValue)
                {
                    transform.Translate(new Vector3(translation, 0.0f, 0.0f));
                }
                else if (Input.GetKey(KeyCode.S) && transform.position.x - translation > minXValue)
                {
                    transform.Translate(new Vector3(-translation, 0.0f, 0.0f));
                }
                else if (Input.GetKey(KeyCode.A) && transform.position.z + translation < maxZValue)
                {
                    transform.Translate(new Vector3(0.0f, 0.0f, translation));
                }
                else if (Input.GetKey(KeyCode.D) && transform.position.z - translation > minZValue)
                {
                    transform.Translate(new Vector3(0.0f, 0.0f, -translation));
                }
            }
        }

        private void OnDestroy()
        {
            _actionButton.OnActionButtonClick -= CatchToy;
        }
    }
}