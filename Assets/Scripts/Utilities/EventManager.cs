using System;

public static class EventManager
{
    public static Action<int> onCheeseCollected;
    public static Action onCheeseGoalReached;

    public static void OnCheeseCollected(int amount) => onCheeseCollected?.Invoke(amount);
}
