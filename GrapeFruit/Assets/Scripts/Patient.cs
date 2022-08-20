using System.Collections.Generic;
using UnityEngine;

public class Patient : DragObjectRecipient
{
    [field: SerializeField] public IllnessData IllnessData { get; private set; }

    private List<TreatmentData> ExpectedTreatments = new List<TreatmentData>();

    private DayManager DayManager;

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
                    foreach (var symptom in IllnessData.Symptoms)
                    {
                        if (symptom.RequiredTool == tool.ToolData)
                        {
                            DayManager.DisplaySymptomFeedback(symptom);
                        }
                    }
                    break;
                }
        }
    }
}
