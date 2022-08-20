using UnityEngine;

public abstract class DragObjectRecipient : MonoBehaviour
{
    public abstract void OnDraggedOn(DraggableObject Object);
}
