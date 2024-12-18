using System;
using UnityEngine;

namespace Components.GameMachineComponents
{
    public class ClawMovementHandler : MonoBehaviour
    {
        [SerializeField] private float movingSpeed;

        [Space(10)] [Header("Limit position values")] 
        [SerializeField] private float minXValue;
        [SerializeField] private float maxXValue;
        [SerializeField] private float minZValue;
        [SerializeField] private float maxZValue;
 
        private HandleHandler _handle;

        private void Start()
        {
            _handle = GameObject.FindGameObjectWithTag("Handle").GetComponent<HandleHandler>();
        }

        private void FixedUpdate()
        {
            float translation = Time.deltaTime * movingSpeed;
                                     
            if (_handle.IsInPlayMode)
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
}