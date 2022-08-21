using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    [SerializeField] public DayData DefaultDayData;
    [SerializeField] public string EndOfDaySceneName;
    [SerializeField] public TMPro.TMP_Text TimeText;
    [SerializeField] public UIPanel TimerPanel;
    [SerializeField] public Transform PatientAnchor;
    [SerializeField] public ConversationPanel ConversationPanel;

    [SerializeField] private BadumController BadumController;
    [SerializeField] private IntroDialogData IntroDialogData;
    [SerializeField] private SmellController SmellController;
    [SerializeField] private TreatmentsRespawner TreatmentsRespawner;
    [SerializeField] private DayMusicPlayer DayMusicPlayer;
    [SerializeField] private BarkSFXController BarkSFXController;

    [SerializeField] private ToolData SniffTool;
    [SerializeField] private ToolData HeartTool;

    private bool DayStarted;
    private float TimeLeft;
    private int PatientIndex;

    private DayData CurrentDayData;
    private Patient CurrentPatient;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            CurrentDayData = GameManager.Instance.GetCurentDayData();
        }
        else
        {
            CurrentDayData = DefaultDayData;
        }

        ConversationPanel.Close();
        StartDay();
        DayMusicPlayer.StartMusic();
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

        PatientIndex = 0;

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

        if (PatientIndex < 0 || PatientIndex >= CurrentDayData.Illnesses.Length)
        {
            StopDay();
            return;
        }

        var illnessData = CurrentDayData.Illnesses[PatientIndex++];

        CurrentPatient = GameObject.Instantiate<Patient>(illnessData.PatientPrefab);
        CurrentPatient.OnSpawned(this, illnessData);
        CurrentPatient.transform.SetParent(PatientAnchor);

        // generic patient arrival dialog
        ConversationPanel.AddDialog(IntroDialogData.GetRandomArrivalDialog());
        ConversationPanel.ShowNext();
        CurrentPatient.OnPat += OnPatPatient;

        TreatmentsRespawner.OnPatientSpawned();
    }

    private void OnPatPatient()
    {
        ConversationPanel.AddDialog(CurrentPatient.IllnessData.Conversation);
        ConversationPanel.ShowNext();
    }

    public void OnBeginSymptomFeedback(SymptomData symptomData)
    {
        Debug.Log($"OnBeginSymptomFeedback {symptomData.name}");

        if (symptomData.RequiredTool == SniffTool)
        {
            SmellController.StartSmelling(symptomData.Description);
        }
        else if (symptomData.RequiredTool == HeartTool)
        {
            BadumController.StartBadum(symptomData.FeedbackFloatValue);
        }
    }

    public void OnEndSymptomFeedback(SymptomData symptomData)
    {
        Debug.Log($"OnBeginSymptomFeedback {symptomData.name}");

        if (symptomData.RequiredTool == SniffTool)
        {
            SmellController.StopSmelling();
        }
        else if (symptomData.RequiredTool == HeartTool)
        {
            BadumController.StopBadum();
        }
    }

    public void OnSuccesfulTreatment()
    {
        Debug.Log($"Patient {CurrentPatient.name} treated.");
        BarkSFXController.Bark();
        DismissPatient(true);
    }

    public void OnFailedTreatment()
    {
        Debug.Log($"Patient {CurrentPatient.name} not treated.");
        BarkSFXController.Whine();
        DismissPatient(false);
    }

    // :oronuke:
    public void DismissPatient(bool wasTreated)
    {
        // TODO play the animation before.
        CurrentPatient.OnPat -= OnPatPatient;
        StartCoroutine(DismissPatientRoutine(wasTreated));
    }

    public DayData GetDayData()
    {
        return CurrentDayData;
    }

    public float GetTimeLeftRatio()
    {
        if (CurrentDayData == null)
        {
            return 0f;
        }

        if (Mathf.Abs(CurrentDayData.MaxTime) <= float.Epsilon)
        {
            return 0f;
        }

        var timeSpent = CurrentDayData.MaxTime - TimeLeft;
        return timeSpent / CurrentDayData.MaxTime;
    }

    private IEnumerator DismissPatientRoutine(bool wasTreated)
    {
        CurrentPatient.PlayOutAnimation();
        CurrentPatient.DisplayTreatmentFeedback(wasTreated);
        yield return new WaitForSeconds(1f);

        Destroy(CurrentPatient.gameObject);
        TreatmentsRespawner.OnPatientDestroyed();
    }
}
