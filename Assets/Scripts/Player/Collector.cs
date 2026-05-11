using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ICheese>()?.Collect();
    }
}
