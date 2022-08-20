using UnityEngine;

public class DragObjectRecipient : MonoBehaviour
{
    public void OnDraggedOn(DraggableObject Object)
    {
        Debug.Log($"{Object.name} dragged on {gameObject.name}");
        // TODO @nox show feedback here.
    }
}
