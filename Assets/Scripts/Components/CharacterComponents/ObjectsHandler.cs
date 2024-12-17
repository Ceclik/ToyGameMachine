using Components.GameMachineComponents;
using Interfaces;
using Services.CharacterServices;
using UnityEngine;

namespace Components.CharacterComponents
{
    [RequireComponent(typeof(ActionTextHandler))]
    public class ObjectsHandler : MonoBehaviour
    {
        [SerializeField] private float rayDistanceToFindObjects;
        
        private IObjectsFinder _objectsFinder;
        public Transform _objectTransform { get; private set; }

        private ActionTextHandler _actionText;

        private void Start()
        {
            _objectsFinder = new ObjectsFinderService();
            _actionText = GetComponent<ActionTextHandler>();
        }

        private void Update()
        {
            if (_objectTransform == null &&
                _objectsFinder.FindObject(Camera.main, rayDistanceToFindObjects, out Transform foundObject))
            {
                _objectTransform = foundObject;
                Debug.Log($"Object {foundObject.gameObject.name} has been found");
            }
            else if (_objectTransform != null &&
                     !_objectsFinder.FindObject(Camera.main, rayDistanceToFindObjects, out Transform foundObject1))
            {
                _objectTransform = null;
            }

            if (_objectTransform != null && !_actionText.IsTextShown)
            {
                if (_objectTransform.TryGetComponent(out CoinRegisterHandler coinRegisterHandler) &&
                    !coinRegisterHandler.IsCoinThrown)
                {
                    Debug.Log("Showing action Text");
                    _actionText.ShowThrowCoinText();
                }
                else if (_objectTransform.TryGetComponent(out HandleHandler handle))
                {
                    Debug.Log("Showing action Text");
                    _actionText.ShowHandleMoveText(handle.IsInPlayMode);
                }
            }
            else if(_objectTransform == null && _actionText.IsTextShown)
            {
                Debug.Log("Hiding Action Text");
                _actionText.HideActionText();
            }
        }
    }
}