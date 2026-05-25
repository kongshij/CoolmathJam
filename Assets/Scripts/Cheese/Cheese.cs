using UnityEngine;

public class Cheese : MonoBehaviour, ICheese
{
    [SerializeField] private CheeseScriptableObject cheese;

    public void Collect()
    {
        EventManager.OnCheeseCollected(cheese.value);
        gameObject.SetActive(false);
    }
}
