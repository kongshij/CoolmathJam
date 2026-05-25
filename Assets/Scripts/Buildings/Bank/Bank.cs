using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Bank : MonoBehaviour, IInteractable
{
    [SerializeField] private EconomyManager economyManager;
    private Dictionary<string, int> storedCheese;
    // private bool isActive = false;


    public void StartInvesting()
    {
        InvokeRepeating(nameof(Invest), 0.0f, 2.0f);
    }

    public IEnumerator Invest()
    {
        
        StopCoroutine(nameof(Invest));
        yield return null;
    }

    public void DepositCheese(string cheeseName, int amount)
    {
        // check here for validity
        if (economyManager.GetCheeseAmount() < amount)
        {
            EventManager.onCheeseWithdrawnError?.Invoke();  // use this to display error in UI
            return;
        }


        storedCheese[cheeseName] += amount;
        economyManager.SubtractCheeseAmount(amount);
    }

    public void WithdrawCheese(string cheeseName, int amount)
    {
        if (!storedCheese.ContainsKey(cheeseName))
        {
            EventManager.onCheeseDepositError?.Invoke();
            return;
        }

        storedCheese[cheeseName] -= amount;
        EventManager.onCheeseCollected?.Invoke(amount);
    }

    public void Interact()
    {
        EventManager.openBankUI?.Invoke();
    }
}
