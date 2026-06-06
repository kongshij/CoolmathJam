using TMPro;
using UnityEngine;

// another name could be BankInterface
public class BankUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private TextMeshProUGUI cheeseSavedText;

    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioClip bankOpenSFX;
    [SerializeField] private AudioClip bankCloseSFX;


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

    private void OpenUI()
    {
        sfxPlayer.PlayOneShot(bankOpenSFX);
        canvas.alpha = 1;
    }

    private void CloseUI()
    {
        sfxPlayer.PlayOneShot(bankCloseSFX);
        canvas.alpha = 0;
    }

    private void UpdateCheeseText(int amount) => cheeseSavedText.text = amount + "";
}
