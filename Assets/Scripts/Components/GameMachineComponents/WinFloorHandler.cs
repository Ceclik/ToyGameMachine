using System;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class WinFloorHandler : MonoBehaviour
    {
        [SerializeField] private Transform winPlace;
        
        private Transform _claw;
        private CoinRegisterHandler _coinRegister;
        private ClawMovementHandler _movementHandler;

        private void Start()
        {
            _movementHandler = GameObject.FindGameObjectWithTag("Claw").GetComponent<ClawMovementHandler>();
            _coinRegister = GameObject.FindGameObjectWithTag("CoinChecker").GetComponent<CoinRegisterHandler>();
            _claw = GameObject.FindGameObjectWithTag("Claw").transform;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Toy"))
            {
                _claw.GetComponent<Collider>().isTrigger = false;
                other.transform.position = winPlace.transform.position;
                _coinRegister.IsCoinThrown = false;
                _movementHandler.HasFallen = false;
                other.rigidbody.AddTorque(1, 0, 0);
            }
        }
    }
}