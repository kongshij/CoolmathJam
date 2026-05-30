using UnityEngine;

// another name could be BankInterface
public class BankUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;

    void OnEnable()
    {
        EventManager.openBankUI += OpenUI;
        EventManager.closeBankUI += CloseUI;
    }

    void OnDisable()
    {
        EventManager.openBankUI -= OpenUI;
        EventManager.closeBankUI -= CloseUI;
    }

    private void OpenUI() => canvas.alpha = 1;
    private void CloseUI() => canvas.alpha = 0;
}
