using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public GameData GameData;

    private int DayIndex;

    private void Awake()
    {
        if (Instance != null)
        {
            throw new System.Exception("Shit happened yo.");
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public DayData GetCurentDatData()
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
