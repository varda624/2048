using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text HighestDiceText;

    public void Update()
    {
        HighestDiceText.text = $"Highest Dice: {GameManager.HighestNumberOnDice}";
    }
}
