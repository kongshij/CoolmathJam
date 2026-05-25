using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private const int CHEESE_GOAL = 10;
    private int cheeseAmount = 0;


    private void OnEnable()
    {
        EventManager.onCheeseCollected += AddCheeseAmount;
        EventManager.onCheeseDeposited += SubtractCheeseAmount;
    }

    private void OnDisable()
    {
        EventManager.onCheeseCollected -= AddCheeseAmount;
        EventManager.onCheeseDeposited -= SubtractCheeseAmount;
    }

    public void AddCheeseAmount(int amount)
    {
        cheeseAmount += amount;
    }

    public void SubtractCheeseAmount(int amount)
    {
        cheeseAmount -= amount;
    }

    public int GetCheeseAmount() => cheeseAmount;
}
