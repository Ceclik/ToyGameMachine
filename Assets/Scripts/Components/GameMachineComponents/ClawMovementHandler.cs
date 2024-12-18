using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
 
        [Space(10)]
        [SerializeField] private Transform clawBasePoint;
        
        [Space(10)] [Range(0, 1)] [SerializeField]
        private float dropChance;

        private ClawObjectHandler _objectHandler;
        private HandleHandler _handle;
        private ActionButtonHandler _actionButton;
        private CoinRegisterHandler _coinRegister;
        private bool _isCatching;
        public bool HasFallen { get; set; }
        private bool _isRising;
        private bool _isChanceCounted;
        public bool IsDropped { get; private set; }

        private void Start()
        {
            _objectHandler = GetComponent<ClawObjectHandler>();
            _coinRegister = GameObject.FindGameObjectWithTag("CoinChecker").GetComponent<CoinRegisterHandler>();
            _actionButton = GameObject.FindGameObjectWithTag("Button").GetComponent<ActionButtonHandler>();
            _handle = GameObject.FindGameObjectWithTag("Handle").GetComponent<HandleHandler>();

            _actionButton.OnActionButtonClick += CatchToy;
        }

        private void CatchToy()
        {
            IsDropped = false;
            _isCatching = true;
            _isChanceCounted = false;
            GetComponent<Collider>().isTrigger = false;
        }

        private IEnumerator CatchDelayed()
        {
            yield return new WaitForSeconds(lowPositionStayingDelay);
            _isRising = true;
        }

        private void FixedUpdate()
        {
            if (_coinRegister.IsCoinThrown)
            {
                if (_objectHandler.IsCatched)
                {
                    if (Random.value <= dropChance && !IsDropped && !_isChanceCounted)
                    {
                        IsDropped = true;
                        StartCoroutine(_objectHandler.ThrowObjectDelayed());
                    }

                    _isChanceCounted = true;
                    transform.position = Vector3.MoveTowards(transform.position, clawBasePoint.position,
                        Time.deltaTime * movingSpeed);
                    return;
                }
                
                if (_isCatching)
                {
                    if (!HasFallen)
                    {
                        transform.Translate(new Vector3(0.0f, -Time.deltaTime * fallingSpeed));
                        if (transform.position.y <= minYPosition)
                        {
                            HasFallen = true;
                            StartCoroutine(CatchDelayed());
                        }
                    }
                    else if (_isRising)
                    {
                        transform.Translate(new Vector3(0.0f, Time.deltaTime * fallingSpeed));
                        if (transform.position.y >= maxYPosition)
                        {
                            _isCatching = false;
                            _coinRegister.IsCoinThrown = false;
                            GetComponent<Collider>().isTrigger = false;
                            HasFallen = false;
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
        }
        
        

        private void OnDestroy()
        {
            _actionButton.OnActionButtonClick -= CatchToy;
        }
    }
}