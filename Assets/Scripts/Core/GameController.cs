using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;

    private DiceRoller diceRoller;

    private void Awake()
    {
        diceRoller = new DiceRoller();
    }

    public void RollDice()
    {
        int result = diceRoller.Roll();

        resultText.text = result.ToString();
    }
}