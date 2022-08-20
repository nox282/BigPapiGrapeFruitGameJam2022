using UnityEngine;

public class DragObjectRecipient : MonoBehaviour
{
    public void OnDraggedOn(DragableObject Object)
    {
        Debug.Log($"Get dragged on scrub {Object.name}");
    }
}
