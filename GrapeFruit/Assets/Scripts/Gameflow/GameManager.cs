using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public GameData GameData;

    private int DayIndex;

    public int GetCurrentDayIndex() => DayIndex;

    public int HeartFeedbackCount;
    public int AngryFeedbackCount;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public DayData GetCurentDayData()
    {
        if (DayIndex < 0 || DayIndex >= GameData.Days.Length)
        {
            return null;
        }

        return GameData.Days[DayIndex];
    }


    public void OnDayFinished()
    {
        DayIndex++;
    }
}
