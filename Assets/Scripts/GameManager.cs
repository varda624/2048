using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnNewRecordAchived = new UnityEvent();
    public static UnityEvent OnNumberOnDiceIncreases = new UnityEvent();
    public static UnityEvent OnMakingChangesToGameScore = new UnityEvent();
    public static int CurrentGameScore;
    public static int RecordGameScore;
    public static int HighestNumberOnDice;
    public static bool IsGameOver;


    public void Start()
    {
        CheckDicesOnScene();
    }


    public static void CheckHighestNumberOnDice(NumberedDice dice)
    {
        if (dice.NumberOnDice > HighestNumberOnDice)
        {
            HighestNumberOnDice = dice.NumberOnDice;
            OnNumberOnDiceIncreases?.Invoke();

        }
    }
    public static void MakeChangesToGameScore(NumberedDice dice)
    {
        CurrentGameScore += dice.NumberOnDice;
        if (RecordGameScore < CurrentGameScore)
        {
            RecordGameScore = CurrentGameScore;
            OnNewRecordAchived?.Invoke();
        }
        OnMakingChangesToGameScore?.Invoke();
    }

    public static void CheckDicesOnScene()
    {
        NumberedDice[] dices = FindObjectsByType<NumberedDice>(FindObjectsSortMode.InstanceID);
        for (int i = 0; i < dices.Length; i++)
        {
            CheckHighestNumberOnDice(dices[i]);
        }
    }
}
