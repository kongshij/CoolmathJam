using TMPro;
using UnityEngine;

// another name could be BankInterface
public class BankUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private TextMeshProUGUI cheeseSavedText;

    void OnEnable()
    {
        EventManager.onCheeseSavingsUpdated += UpdateCheeseText;
        EventManager.openBankUI += OpenUI;
        EventManager.closeBankUI += CloseUI;
    }

    void OnDisable()
    {
        EventManager.onCheeseSavingsUpdated -= UpdateCheeseText;
        EventManager.openBankUI -= OpenUI;
        EventManager.closeBankUI -= CloseUI;
    }

    private void OpenUI() => canvas.alpha = 1;
    private void CloseUI() => canvas.alpha = 0;
    private void UpdateCheeseText(int amount) => cheeseSavedText.text = amount + "";
}
