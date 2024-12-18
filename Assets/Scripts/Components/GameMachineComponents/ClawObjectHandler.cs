using System.Collections;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class ClawObjectHandler : MonoBehaviour
    {
        private Transform _toyTransform;
        [SerializeField] private Transform clawBasePoint;
        [SerializeField] private float throwObjectDelay;
        [SerializeField] private Transform toysParent;

        private CoinRegisterHandler _coinRegister;
        private ClawMovementHandler _mover;
        public bool IsCatched { get; private set; }


        private void Start()
        {
            _mover = GetComponent<ClawMovementHandler>();
            _coinRegister = GameObject.FindGameObjectWithTag("CoinChecker").GetComponent<CoinRegisterHandler>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!IsCatched && other.gameObject.CompareTag("Toy"))
            {
                Debug.Log($"Toy with name {other.gameObject.name} has been catched");
                IsCatched = true;
                _toyTransform = other.transform;
                _toyTransform.SetParent(transform);
                _toyTransform.GetComponent<Rigidbody>().isKinematic = true;
                _toyTransform.GetComponent<Collider>().isTrigger = true;    
            }
        }

        private void Update()
        {
            if (IsCatched && Vector3.Distance(transform.position, clawBasePoint.position) < 0.01f)
            {
                Debug.Log("Claw is arrived");
                IsCatched = false;
                //_coinRegister.IsCoinThrown = false;
                if(!_mover.IsDropped)
                    StartCoroutine(ThrowObjectDelayed());
            }
        }

        public void DropObject()
        {
            GetComponent<Collider>().isTrigger = true;
            _toyTransform.SetParent(toysParent);
            _toyTransform.GetComponent<Rigidbody>().isKinematic = false;
            _toyTransform.GetComponent<Collider>().isTrigger = false;
            _toyTransform = null;
        }

        public IEnumerator ThrowObjectDelayed()
        {
            yield return new WaitForSeconds(throwObjectDelay);
            DropObject();
        }
    }
}