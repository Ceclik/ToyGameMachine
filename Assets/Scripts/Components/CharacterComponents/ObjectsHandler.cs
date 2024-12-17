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
        public Transform ObjectTransform { get; private set; }

        private ActionTextHandler _actionText;

        private void Start()
        {
            _objectsFinder = new ObjectsFinderService();
            _actionText = GetComponent<ActionTextHandler>();
        }

        private void Update()
        {
            if (ObjectTransform == null &&
                _objectsFinder.FindObject(Camera.main, rayDistanceToFindObjects, out Transform foundObject))
            {
                ObjectTransform = foundObject;
                Debug.Log($"Object {foundObject.gameObject.name} has been found");
            }
            else if (ObjectTransform != null &&
                     !_objectsFinder.FindObject(Camera.main, rayDistanceToFindObjects, out Transform foundObject1))
            {
                ObjectTransform = null;
            }

            if (ObjectTransform != null && !_actionText.IsTextShown)
            {
                if (ObjectTransform.TryGetComponent(out CoinRegisterHandler coinRegisterHandler) &&
                    !coinRegisterHandler.IsCoinThrown)
                {
                    Debug.Log("Showing action Text");
                    _actionText.ShowThrowCoinText();
                }
                else if (ObjectTransform.TryGetComponent(out HandleHandler handle))
                {
                    Debug.Log("Showing action Text");
                    _actionText.ShowHandleMoveText(handle.IsInPlayMode);
                }
                else if (ObjectTransform.TryGetComponent(out ActionButtonHandler button))
                {
                    Debug.Log("Showing action Text");
                    if(!button.IsMoving)
                        _actionText.ShowPlayButtonText();
                    else _actionText.HideActionText();
                }
            }
            else if(ObjectTransform == null && _actionText.IsTextShown)
            {
                Debug.Log("Hiding Action Text");
                _actionText.HideActionText();
            }
        }
    }
}