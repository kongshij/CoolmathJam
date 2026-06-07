using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cheeseText;
    [SerializeField] private Slider fuelSlider;


    private void OnEnable()
    {
        EventManager.onCheeseAmountUpdated += UpdateCheeseUI;
        EventManager.onJetpackFuelUpdated += UpdateJetpackFuelUI;
    }

    private void OnDisable()
    {
        EventManager.onCheeseAmountUpdated -= UpdateCheeseUI;
        EventManager.onJetpackFuelUpdated -= UpdateJetpackFuelUI;
    }

    private void UpdateCheeseUI(int amount)
    {
        cheeseText.text = amount + "";
    }

    private void UpdateJetpackFuelUI(float amount)
    {
        fuelSlider.value = amount;
    }
}
