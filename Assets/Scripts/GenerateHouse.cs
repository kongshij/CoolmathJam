using System.Linq;
using UnityEngine;

public class GenerateHouse : MonoBehaviour
{
    [SerializeField] GameObject[] newHouseParts;
    [SerializeField] bool updatePositions = false;
    [SerializeField] float xSpacing = 4f;
    [SerializeField] float zSpacing = 5.5f;

    [SerializeField] GameObject[] houseParts;

    private void Start()
    {
        string housePartsNames = string.Join(", ", houseParts.Select(part => part.name));
        Debug.Log("House parts to generate: " + housePartsNames);
        for (int i = 0; i < houseParts.Length; i++)
        {
            // Instantiate the house parts from bottom left to right to top left to right so all 38 can be shown in rows and columns
            var newPart = Instantiate(houseParts[i], new Vector3((i % 6) * 4f, 0, (i / 6) * 5.5f), Quaternion.identity);
            newHouseParts[i] = newPart;  
        }
    }

    private void Update()
    {
        if (updatePositions)
        {
            updatePositions = false;
            for (int i = 0; i < newHouseParts.Length; i++)
            {
                newHouseParts[i].transform.position = new Vector3((i % 6) * xSpacing, 0, (i / 6) * zSpacing);
            }
        }
    }
}
