using Interfaces;
using UnityEngine;

namespace Services.GameMachineServices
{
    public class HandleMovingService : IHandleMover
    {
        private bool _isTilted;
        
        public void MoveHandler(Transform handlerTransform)
        {
            if (Input.GetKey(KeyCode.W) && !_isTilted)
            {
                _isTilted = true;
                handlerTransform.rotation = Quaternion.Euler(new Vector3(-65, 90, -90));
            }
            else if (Input.GetKey(KeyCode.A) && !_isTilted)
            {
                _isTilted = true;
                handlerTransform.rotation = Quaternion.Euler(new Vector3(-65, 0, 0));
            }
            else if (Input.GetKey(KeyCode.S) && !_isTilted)
            {
                _isTilted = true;
                handlerTransform.rotation = Quaternion.Euler(new Vector3(-65, -90, 90));
            }
            else if (Input.GetKey(KeyCode.D) && !_isTilted)
            {
                _isTilted = true;
                handlerTransform.rotation = Quaternion.Euler(new Vector3(-115, 0, 0));
            }
            else if (!Input.GetKey(KeyCode.A) &&
                     !Input.GetKey(KeyCode.W) &&
                     !Input.GetKey(KeyCode.S) &&
                     !Input.GetKey(KeyCode.D) &&
                     _isTilted)
            {
                _isTilted = false;
                handlerTransform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            }
        }
    }
}