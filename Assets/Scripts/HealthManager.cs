using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI healthtext;
    public float Health = 12;

    private void Update()
    {
        //  change sprite to version with {health} rays
        healthtext.text = $"Health: {Health}";
    }
}
