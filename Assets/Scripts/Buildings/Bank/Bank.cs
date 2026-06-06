using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bank : MonoBehaviour, IInteractable
{
    private const int AMOUNT_INCREASE = 10;
    private const float INVESTMENT_TIME = 2.0f;

    [SerializeField] private EconomyManager economyManager;
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioClip depositSfx;
    [SerializeField] private AudioClip withdrawSfx;

    private InputSystem_Actions inputActions;
    private int _storedCheese = 0;
    private bool investingActive = false;
    private bool canUseServices = false;


    private void Awake()
    {
        inputActions = new();
    }

    private void Start()
    {
        EventManager.onCheeseSavingsUpdated?.Invoke(_storedCheese);
    }

    private void OnEnable()
    {
        inputActions.Bank.Enable();
        inputActions.Bank.Deposit.performed += DepositInputAction;
        inputActions.Bank.Withdraw.performed += WithdrawInputAction;
        EventManager.closeBankUI += NoLongerServicable;
    }

    private void OnDisable()
    {
        inputActions.Bank.Disable();
        inputActions.Bank.Deposit.performed -= DepositInputAction;
        inputActions.Bank.Withdraw.performed -= WithdrawInputAction;
        EventManager.closeBankUI -= NoLongerServicable;
    }

    public void DepositInputAction(InputAction.CallbackContext context)
    {
        if (context.performed && canUseServices)
        {
            DepositCheese(economyManager.cheeseAmount);
            StartInvesting();
            EventManager.onCheeseSavingsUpdated?.Invoke(_storedCheese);
        }
    }

    public void WithdrawInputAction(InputAction.CallbackContext context)
    {
        if (context.performed && canUseServices)
        {
            WithdrawCheese();
        }
    }

    public void StartInvesting()
    {
        if (!investingActive)
        {
            StartCoroutine(nameof(Invest));
            investingActive = true;
        }
    }

    public IEnumerator Invest()
    {
        while (_storedCheese > 0)
        {
            yield return new WaitForSeconds(INVESTMENT_TIME);
            if (_storedCheese <= 0)
            {
                _storedCheese = 0;
                investingActive = false;
                StopCoroutine(nameof(Invest));
                yield return null;
            }
            _storedCheese += AMOUNT_INCREASE;
            EventManager.onCheeseSavingsUpdated?.Invoke(_storedCheese);
        }
        yield return null;
    }

    private void DepositCheese(int amount)
    {
        if (economyManager.GetCheeseAmount() == 0) return;
        sfxPlayer.PlayOneShot(depositSfx);
        _storedCheese += amount;
        economyManager.SubtractCheeseAmount(amount);
    }

    private void WithdrawCheese()
    {
        if (_storedCheese == 0) return;
        sfxPlayer.PlayOneShot(withdrawSfx);
        EventManager.onCheeseCollected?.Invoke(_storedCheese);
        _storedCheese = 0;
        StopCoroutine(nameof(Invest));
        investingActive = false;
        EventManager.onCheeseSavingsUpdated?.Invoke(_storedCheese);
    }

    private void NoLongerServicable() => canUseServices = false;

    public void Interact()
    {
        EventManager.openBankUI?.Invoke();
        canUseServices = true;
    }
}
