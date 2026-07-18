using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text modifierText;

    private int modifier = 0;

    private DiceRoller diceRoller;
   

    private void Awake()
    {
        diceRoller = new DiceRoller();

        UpdateModifierUI();
    }

    public void RollDice()
    {
        int roll = diceRoller.Roll();

        int total = roll + modifier;

        string modifierString = modifier >= 0 ? "+" + modifier : modifier.ToString();

        resultText.text = $"{roll} {modifierString} = {total}";
    }

    public void IncreaseModifier()
    {
        modifier = Mathf.Min(modifier + 1, 10);

        UpdateModifierUI();
    }
    public void DecreaseModifier()
    {
        modifier = Mathf.Max(modifier - 1, -10);

        UpdateModifierUI();
    }

    private void UpdateModifierUI()
    {
        modifierText.text = modifier >= 0 ? $"+{modifier}" : modifier.ToString();
    }
}