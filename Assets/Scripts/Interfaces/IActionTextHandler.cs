using TMPro;

namespace Interfaces
{
    public interface IActionTextHandler
    {
        public void ShowThrowCoinText(TextMeshProUGUI actionText);

        public void ShowHandleMoveText(TextMeshProUGUI actionText, bool isInHandleMode);

        public void ShowButtonText(TextMeshProUGUI actionText);
    }
}