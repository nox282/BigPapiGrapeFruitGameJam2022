using UnityEngine;
using UnityEngine.EventSystems;

public class DragableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Camera MainCamera;
    private Vector3 OriginalPosition;
    private float OriginalZ;

    private void Awake()
    {
        MainCamera = Camera.main;
        OriginalPosition = transform.position;
        OriginalZ = OriginalPosition.z;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        var worldPosition = MainCamera.ScreenToWorldPoint(eventData.position);
        worldPosition.z = OriginalZ;
        transform.position = worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var worldPosition = MainCamera.ScreenToWorldPoint(eventData.position);
        var hits = Physics.RaycastAll(worldPosition, Vector3.forward, 10000);
        foreach (var hit in hits)
        {
            var dragRecipent = hit.collider.gameObject.GetComponent<DragObjectRecipient>();
            if (dragRecipent != null)
            {
                dragRecipent.OnDraggedOn(this);
            }
        }

        transform.position = OriginalPosition;
    }

    // This needs to be implemented along with the DragHandlers, otherwise it seems to lock for no reason
    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
