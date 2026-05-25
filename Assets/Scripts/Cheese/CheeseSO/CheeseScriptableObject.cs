using UnityEngine;

[CreateAssetMenu(fileName = "CheeseType", menuName = "Cheese/CheeseScriptableObject", order = 1)]
public class CheeseScriptableObject : ScriptableObject
{
    public string cheeseName = "";
    public int value = 0;
}
