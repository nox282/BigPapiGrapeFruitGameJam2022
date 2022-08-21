using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIEndOfDay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMPro.TMP_Text Text;
    [SerializeField] private string GameSceneName;
    [SerializeField] private string EndOfGameSceneName;
    [SerializeField] private float MaxDisplayTime;
    [SerializeField] private float MinDisplayTime;


    [SerializeField] private TMPro.TMP_Text HeartFeedbackCounterText;
    [SerializeField] private TMPro.TMP_Text AngryFeedbackCounterText;

    private float EndTimeStamp;
    private float MinTimeStamp;

    private void OnEnable()
    {
        EndTimeStamp = Time.time + MaxDisplayTime;
        MinTimeStamp = Time.time + MinDisplayTime;

        if (GameManager.Instance == null)
        {
            return;
        }

        Text.text = $"End of day {GameManager.Instance.GetCurrentDayIndex()}.";

        if (GameManager.Instance != null)
        {
            HeartFeedbackCounterText.text = $"0{GameManager.Instance.HeartFeedbackCount}";
            AngryFeedbackCounterText.text = $"0{GameManager.Instance.AngryFeedbackCount}";
        }
    }

    private void Update()
    {
        if (Time.time > EndTimeStamp)
        {
            EndTimeStamp = float.PositiveInfinity;
            NextDay();
        }
    }

    public void NextDay()
    {
        if (GameManager.Instance == null)
        {
            SceneManager.LoadScene(GameSceneName);
            return;
        }

        if (GameManager.Instance.GetCurrentDayIndex() >= GameManager.Instance.GameData.Days.Length)
        {
            SceneManager.LoadScene(EndOfGameSceneName);
        }
        else
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time > MinTimeStamp)
        {
            NextDay();
        }
    }
}
