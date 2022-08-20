using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    [SerializeField] public DayData DefaultDayData;
    [SerializeField] public string EndOfDaySceneName;
    [SerializeField] public TMPro.TMP_Text TimeText;
    [SerializeField] public UIPanel TimerPanel;

    private bool DayStarted;
    private float TimeLeft;

    private DayData CurrentDayData;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            CurrentDayData = GameManager.Instance.GetCurentDatData();
        }
        else
        {
            CurrentDayData = DefaultDayData;
        }

        StartDay();
    }

    private void Update()
    {
        if (DayStarted)
        {
            TimerPanel.Open();
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0f)
            {
                StopDay();
            }
            TimeText.text = $"Time left: {TimeLeft.ToString("0.00")}";
        }
        else
        {
            TimerPanel.Close();
        }

    }

    public void StartDay()
    {
        if (CurrentDayData == null)
        {
            return;
        }

        if (!CurrentDayData.IsTutorial)
        {
            DayStarted = true;
            TimeLeft = CurrentDayData.MaxTime;
        }
    }

    public void StopDay()
    {
        DayStarted = false;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDayFinished();
        }

        SceneManager.LoadScene(EndOfDaySceneName);
    }
}
