using UnityEngine;

public class Treatment : DraggableObject
{
    [field: SerializeField]
    public TreatmentData TreatmentData { get; private set; }

    protected override void OnDragged(DragObjectRecipient Recipient)
    {
        base.OnDragged(Recipient);
    }
}
