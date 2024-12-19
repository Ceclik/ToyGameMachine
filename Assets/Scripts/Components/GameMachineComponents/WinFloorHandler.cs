using System;
using Components.CharacterComponents;
using TMPro;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class WinFloorHandler : MonoBehaviour
    {
        [SerializeField] private Transform winPlace;
        [SerializeField] private TextMeshProUGUI toysText;

        private int _counter;
        private GameObject _previousToy;

        public int ToysCount
        {
            get => _counter;
            set
            {
                _counter = value;
                UpdateToysText();
            }
        }
        private Transform _claw;
        private CoinRegisterHandler _coinRegister;
        private ClawMovementHandler _movementHandler;
        private CoinsHandler _coins;

        private void UpdateToysText()
        {
            toysText.text = ToysCount.ToString();
        }

        private void Start()
        {
            ToysCount = _counter;
            _movementHandler = GameObject.FindGameObjectWithTag("Claw").GetComponent<ClawMovementHandler>();
            _coinRegister = GameObject.FindGameObjectWithTag("CoinChecker").GetComponent<CoinRegisterHandler>();
            _claw = GameObject.FindGameObjectWithTag("Claw").transform;
            _coins = GameObject.FindGameObjectWithTag("Player").GetComponent<CoinsHandler>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Toy"))
            {
                if (_previousToy != null)
                {
                    Destroy(_previousToy);
                    _previousToy = other.gameObject;
                }

                ToysCount++;
                _claw.GetComponent<Collider>().isTrigger = false;
                other.transform.position = winPlace.transform.position;
                _coinRegister.IsCoinThrown = false;
                _movementHandler.HasFallen = false;
                _coins.CoinsAmount += 2;
                other.rigidbody.AddTorque(5, 0, 0);
            }
        }
    }
}