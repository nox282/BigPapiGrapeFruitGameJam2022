using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Patient : DragObjectRecipient, IPointerClickHandler
{
	public float PatMaxTime = 1.5f;
	public int PatAmount = 3;
    [field: SerializeField] public IllnessData IllnessData { get; private set; }

    private List<TreatmentData> ExpectedTreatments = new List<TreatmentData>();

	public Action OnPat;

    private DayManager DayManager;
	private int _patCount = 0;
	private float _patTimer = 0;

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
		if(_patCount == 0)
		{
			_patTimer = PatMaxTime;
		}

		_patCount++;
		
		if(_patCount >= PatAmount)
		{
			_patCount = 0;
			
			OnPat?.Invoke();
		}
	}

	private void Update()
	{
		if(_patCount > 0)
		{
			_patTimer -= Time.deltaTime;
			if(_patTimer <= 0f)
			{
				_patCount = 0;
			}
		}
	}
}
