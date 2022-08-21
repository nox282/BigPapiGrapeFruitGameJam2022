using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Patient : DragObjectRecipient, IPointerClickHandler
{
    private const string kIsOutString = "IsOut";
    private static int kIsOutHash = Animator.StringToHash(kIsOutString);

    public float PatMaxTime = 1f;
    public int PatAmount = 2;
    [field: SerializeField] public IllnessData IllnessData { get; private set; }

    [field: SerializeField] public AudioSource SittingDownSFX { get; private set; }
    [field: SerializeField] public AudioSource GettingUpSFX { get; private set; }
    [field: SerializeField] public Animator AnimatorController { get; private set; }

    private List<TreatmentData> ExpectedTreatments = new List<TreatmentData>();

    public Action OnPat;

    private DayManager DayManager;
    private int _patCount = 0;
    private float _patTimer = 0;

    private float _jumpTimer;

    private const float jumpTotalTime = .2f;

    private void OnEnable()
    {
        AnimatorController.SetBool(kIsOutHash, false);
    }

    public void OnSpawned(DayManager dayManager, IllnessData illnessData)
    {
        DayManager = dayManager;
        IllnessData = illnessData;

        foreach (var treatmentData in IllnessData.Treatments)
        {
            ExpectedTreatments.Add(treatmentData);
        }
    }

    public override void OnDraggedOn(DraggableObject DraggedObject)
    {
        Debug.Log($"{DraggedObject.name} dragged on {gameObject.name}");

        switch (DraggedObject)
        {
            case Treatment treatment:
                {
                    int treatmentIndex = ExpectedTreatments.IndexOf(treatment.TreatmentData);
                    if (treatmentIndex >= 0)
                    {
                        ExpectedTreatments.RemoveAt(treatmentIndex);
                        treatment.OnUsed();

                        if (ExpectedTreatments.Count <= 0)
                        {
                            DayManager.OnSuccesfulTreatment();
                        }
                    }
                    break;
                }

            case Tool tool:
                {
                    break;
                }
        }
    }

    public override void OnDraggedOnEnter(DraggableObject DraggedObject)
    {
        Debug.Log($"{DraggedObject.name} OnDraggedOnEnter {gameObject.name}");

        switch (DraggedObject)
        {
            case Treatment treatment:
                {
                    break;
                }

            case Tool tool:
                {
                    foreach (var symptom in IllnessData.Symptoms)
                    {
                        if (symptom.RequiredTool == tool.ToolData)
                        {
                            DayManager.OnBeginSymptomFeedback(symptom);
                        }
                    }
                    break;
                }
        }
    }

    public override void OnDraggedOnExit(DraggableObject DraggedObject)
    {
        Debug.Log($"{DraggedObject.name} OnDraggedOnExit {gameObject.name}");

        switch (DraggedObject)
        {
            case Treatment treatment:
                {
                    break;
                }

            case Tool tool:
                {
                    foreach (var symptom in IllnessData.Symptoms)
                    {
                        if (symptom.RequiredTool == tool.ToolData)
                        {
                            DayManager.OnEndSymptomFeedback(symptom);
                        }
                    }
                    break;
                }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _patCount++;

        if (_patCount >= PatAmount)
        {
            _patCount = 0;

            OnPat?.Invoke();
        }
    }

    private void Update()
    {
        if (_patCount > 0)
        {
            _patTimer -= Time.deltaTime;
            if (_patTimer <= 0f)
            {
                _patCount = 0;
            }
        }
        else
        {
            _patTimer = PatMaxTime;
        }
    }

    public void StartJump()
    {
        if (_jumpTimer <= 0)
        {
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        _jumpTimer = jumpTotalTime;
        Vector3 v = new Vector3(1f, 1f, 1f);
        float half = jumpTotalTime / 2f;

        float xOffset = -0.1f;
        float yOffset = 0.1f;

        while (_jumpTimer > 0)
        {
            _jumpTimer -= Time.deltaTime;

            if (_jumpTimer > half)
            {
                float t = Mathf.Abs(_jumpTimer - jumpTotalTime) / half;
                v.x = 1 + (t * xOffset);
                v.y = 1 + (t * yOffset);
            }
            else
            {
                float t = Mathf.Abs(_jumpTimer - half) / half;
                v.x = 1 + xOffset - (t * xOffset);
                v.y = 1 + yOffset - (t * yOffset);
            }

            transform.localScale = v;
            Debug.Log(v);

            yield return null;
        }

        transform.localScale = new Vector3(1, 1, 1);
    }

    public void PlaySittingDownSFX()
    {
        SittingDownSFX.Play();
    }

    public void PlayGettingUpFX()
    {
        GettingUpSFX.Play();
    }

    public void PlayOutAnimation()
    {
        AnimatorController.SetBool(kIsOutHash, true);
    }
}
