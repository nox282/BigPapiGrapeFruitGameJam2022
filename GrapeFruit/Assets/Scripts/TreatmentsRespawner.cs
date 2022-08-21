using System.Collections.Generic;
using UnityEngine;

public class TreatmentsRespawner : MonoBehaviour
{
    [SerializeField] private List<Treatment> Treatments = new List<Treatment>();

    private Dictionary<Treatment, Vector3> OriginalSpots = new Dictionary<Treatment, Vector3>();

    private void Awake()
    {
        OriginalSpots.Clear();

        foreach (var treatment in Treatments)
        {
            OriginalSpots.Add(treatment, treatment.transform.position);
        }
    }

    private void OnEnable()
    {
        foreach (var treatment in Treatments)
        {
            treatment.OnUsedEvent += OnTreatmentUsed;
        }
    }

    private void OnDisable()
    {
        foreach (var treatment in Treatments)
        {
            treatment.OnUsedEvent -= OnTreatmentUsed;
        }
    }

    public void OnPatientSpawned()
    {
        foreach (var treatment in Treatments)
        {
            if (!treatment.isActiveAndEnabled)
            {
                treatment.transform.position = OriginalSpots[treatment];
                treatment.gameObject.SetActive(true);
            }
        }
    }

    public void OnPatientDestroyed()
    {

    }

    private void OnTreatmentUsed(Treatment treatment)
    {
        treatment.gameObject.SetActive(false);
    }
}
