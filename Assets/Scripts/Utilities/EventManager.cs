using System;

public static class EventManager
{
    public static Action openBankUI;
    public static Action closeBankUI;

    public static Action<int> onCheeseCollected;
    public static Action onCheeseGoalReached;

    // Cheese transactions
    public static Action<int> onCheeseWithdrawn;
    public static Action<int> onCheeseDeposited;
    public static Action<int> onCheeseSavingsUpdated;

    // Cheese transaction errors
    public static Action onCheeseWithdrawnError;
    public static Action onCheeseDepositError;

    public static void OnCheeseCollected(int amount) => onCheeseCollected?.Invoke(amount);
}
