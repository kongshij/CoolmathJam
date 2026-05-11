using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private const int CHEESE_GOAL = 10;
    private int cheeseAmount = 0;


    private void OnEnable()
    {
        EventManager.onCheeseCollected += AddCheeseAmount;
    }

    private void OnDisable()
    {
        EventManager.onCheeseCollected -= AddCheeseAmount;
    }

    public void AddCheeseAmount(int amount)
    {
        cheeseAmount += amount;
    }
}
