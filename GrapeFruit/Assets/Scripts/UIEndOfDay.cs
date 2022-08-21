using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndOfDay : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text Text;
    [SerializeField] private string GameSceneName;
    [SerializeField] private float DisplayTime;

    private float EndTimeStamp;

    private void OnEnable()
    {
        EndTimeStamp = Time.time + DisplayTime;

        if (GameManager.Instance == null)
        {
            return;
        }

        Text.text = $"End of day {GameManager.Instance.GetCurrentDayIndex()}.";
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
        SceneManager.LoadScene(GameSceneName);
    }
}
