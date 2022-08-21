using System;
using UnityEngine;

public class Treatment : DraggableObject
{
    [field: SerializeField]
    public TreatmentData TreatmentData { get; private set; }

    public event Action<Treatment> OnUsedEvent;

    protected override void OnDragged(DragObjectRecipient Recipient)
    {
        base.OnDragged(Recipient);
    }

    public void OnUsed()
    {
        OnUsedEvent?.Invoke(this);
    }
}
