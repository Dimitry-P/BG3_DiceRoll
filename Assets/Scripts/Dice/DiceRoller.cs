using TMPro;
using UnityEngine;

public class DiceRoller
{
    public int Roll()
    {
        return Random.Range(1, 21);
    }
}