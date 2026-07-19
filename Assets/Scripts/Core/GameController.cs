using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text modifierText;
    [SerializeField] private RectTransform diceTransform;
    [SerializeField] private TMP_Text outcomeText;
    [SerializeField] private Button rollButton;
    [SerializeField] private Image diceImage;

    [SerializeField] private Sprite[] diceSprites;

    private int modifier = 0;

    private DiceRoller diceRoller;
   

    private void Awake()
    {
        diceRoller = new DiceRoller();

        UpdateModifierUI();
    }

    public void RollDice()
    {
        StartCoroutine(RollAnimation());
    }

    private IEnumerator RollAnimation()
    {
        float numberTimer = 0f;

        float duration = 3f;
        float timer = 0f;

        outcomeText.text = "";
        rollButton.interactable = false;

        Vector3 startPosition = diceTransform.localPosition;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            numberTimer += Time.deltaTime;

            float rotationSpeed = Mathf.Lerp(360f, 40f, timer / duration);

            diceTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

            float yOffset = Mathf.Sin(timer * 18f) * 12f;

            diceTransform.localPosition =
                startPosition + Vector3.up * yOffset;

            float scale = 1f + Mathf.Sin(timer * 15f) * 0.28f;
            diceTransform.localScale = Vector3.one * scale;

            if (numberTimer > 0.2f)
            {
                numberTimer = 0f;
                resultText.text = diceRoller.Roll().ToString();

                diceImage.sprite = diceSprites[Random.Range(1, diceSprites.Length)];
            }

            yield return null;
        }
        diceImage.sprite = diceSprites[0];
        diceTransform.localScale = Vector3.one;
        diceTransform.localPosition = startPosition;
        diceTransform.localRotation = Quaternion.identity;
        int roll = diceRoller.Roll();

        int total = roll + modifier;

        string modifierString = modifier >= 0
            ? "+" + modifier
            : modifier.ToString();

        resultText.text = $"{roll} {modifierString} = {total}";

        if (roll == 20)
        {
            outcomeText.color = Color.green;
            outcomeText.text = "Critical Success!";
        }
        else if (roll == 1)
        {
            outcomeText.color = Color.red;
            outcomeText.text = "Critical Failure!";
        }
        else
        {
            outcomeText.color = Color.white;
            outcomeText.text = "";
        }
        rollButton.interactable = true;
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