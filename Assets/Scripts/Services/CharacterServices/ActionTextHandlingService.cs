using Interfaces;
using TMPro;

namespace Services.CharacterServices
{
    public class ActionTextHandlingService : IActionTextHandler
    {
        public void ShowThrowCoinText(TextMeshProUGUI actionText)
        {
            actionText.text = "Press 'F' to throw coin";
            actionText.gameObject.SetActive(true);
        }

        public void ShowHandleMoveText(TextMeshProUGUI actionText, bool isInHandleMode)
        {
            if (!isInHandleMode)
            {
                actionText.text = "Press 'Left Mouse' to enter play mode";
                actionText.gameObject.SetActive(true);
            }
            else
            {
                actionText.text = "Press 'Right Mouse' to exit play mode";
                actionText.gameObject.SetActive(true);
            }
        }
    }
}