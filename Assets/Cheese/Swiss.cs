using UnityEngine;

public class Swiss : MonoBehaviour, ICheese
{
    private const int VALUE = 1;

    public void Collect()
    {
        EventManager.OnCheeseCollected(VALUE);
        gameObject.SetActive(false);
    }
}
