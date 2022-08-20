using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    [SerializeField] public DayData DefaultDayData;
    [SerializeField] public string EndOfDaySceneName;
    [SerializeField] public TMPro.TMP_Text TimeText;
    [SerializeField] public UIPanel TimerPanel;
    [SerializeField] public Transform PatientAnchor;

    private bool DayStarted;
    private float TimeLeft;

    private DayData CurrentDayData;
    private Patient CurrentPatient;

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

        if (CurrentPatient == null)
        {
            SpawnPatient();
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

    public void SpawnPatient()
    {
        if (CurrentDayData == null)
        {
            return;
        }

        var randomIndex = Random.Range(0, CurrentDayData.Illnesses.Length - 1);
        var illnessData = CurrentDayData.Illnesses[randomIndex];

        CurrentPatient = GameObject.Instantiate<Patient>(illnessData.PatientPrefab);
        CurrentPatient.OnSpawned(this, illnessData);
        CurrentPatient.transform.SetParent(PatientAnchor);
    }

    public void DisplaySymptomFeedback(SymptomData symptomData)
    {
        Debug.Log($"Symptom feedback {symptomData.Name}");
    }

    public void OnSuccesfulTreatment()
    {
        Debug.Log($"Patient {CurrentPatient.name} treated.");
        DismissPatient();
    }

    // :oronuke:
    public void DismissPatient()
    {
        // TODO play the animation before.
        Destroy(CurrentPatient.gameObject);
    }
}
