using UnityEngine;

public abstract class DragObjectRecipient : MonoBehaviour
{
    public abstract void OnDraggedOn(DraggableObject Object);
    public abstract void OnDraggedOnEnter(DraggableObject Object);
    public abstract void OnDraggedOnExit(DraggableObject Object);
}
