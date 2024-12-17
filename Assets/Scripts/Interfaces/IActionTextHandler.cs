using TMPro;

namespace Interfaces
{
    public interface IActionTextHandler
    {
        public void HandleActionText(TextMeshProUGUI actionText, bool isObjectPicked);
        public void ShowThrowCoinText(TextMeshProUGUI actionText);
        public void ShowCashRegisterText(TextMeshProUGUI actionText, bool isOpened);
    }
}