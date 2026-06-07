using UnityEngine;
using UnityEngine.SceneManagement;

public class EconomyManager : MonoBehaviour
{
    private const int CHEESE_GOAL = 150;
    public int cheeseAmount = 0;


    private void Start()
    {
        EventManager.onCheeseAmountUpdated?.Invoke(cheeseAmount);
    }

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
        if (cheeseAmount >= CHEESE_GOAL)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        EventManager.onCheeseAmountUpdated?.Invoke(cheeseAmount);
    }

    public void SubtractCheeseAmount(int amount)
    {
        cheeseAmount -= amount;
        EventManager.onCheeseAmountUpdated?.Invoke(cheeseAmount);
    }

    public int GetCheeseAmount() => cheeseAmount;
}
