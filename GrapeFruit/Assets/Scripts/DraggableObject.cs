using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public bool RevertPosition = true;
    public bool MaintainOffset = false;

    private Camera MainCamera;
    private Vector3 OriginalPosition;
    private Paw Paw;

    private float OriginalZ;
    private Vector3 _offset;

    private HashSet<DragObjectRecipient> HoveringDraggableObjects = new HashSet<DragObjectRecipient>();

    private void Awake()
    {
        MainCamera = Camera.main;
        OriginalPosition = transform.position;
        OriginalZ = OriginalPosition.z;
    }

    public void OnBeginDrag()
    {
        Debug.Log($"{gameObject.name}.OnBeginDrag");

        if (PawsController.Instance == null || Paw != null)
        {
            return;
        }

        PawsController.Instance.SetHoldingObject(true);
        Paw = PawsController.Instance.CurrentActivePaw;
        _offset = transform.position - Paw.transform.position;
    }

    public void OnDrag()
    {
        if (Paw == null)
        {
            return;
        }

        var targetPosition = Paw.transform.position;

        if (MaintainOffset)
        {
            targetPosition += _offset;
        }

        targetPosition.z = OriginalZ;

        transform.position = targetPosition;

        var worldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        // fuck this piece of code.
        // Handle on object enter/exit
        List<DragObjectRecipient> stillHoveringDragObjectRecipients = new List<DragObjectRecipient>();
        var colliders = Physics2D.OverlapPointAll(worldPosition);
        foreach (var collider in colliders)
        {
            var dragRecipent = collider.gameObject.GetComponent<DragObjectRecipient>();
            if (dragRecipent != null)
            {
                if (HoveringDraggableObjects.Add(dragRecipent))
                {
                    dragRecipent.OnDraggedOnEnter(this);
                }

                stillHoveringDragObjectRecipients.Add(dragRecipent);
            }
        }

        List<DragObjectRecipient> dragObjectRecipientsToRemove = new List<DragObjectRecipient>();
        foreach (var hoveringDraggableObjects in HoveringDraggableObjects)
        {
            if (!stillHoveringDragObjectRecipients.Contains(hoveringDraggableObjects))
            {
                dragObjectRecipientsToRemove.Add(hoveringDraggableObjects);
            }
        }

        foreach (var dragObjectRecipient in dragObjectRecipientsToRemove)
        {
            dragObjectRecipient.OnDraggedOnExit(this);
            HoveringDraggableObjects.Remove(dragObjectRecipient);
        }
    }

    public void OnEndDrag()
    {
        Paw = null;
        if (RevertPosition)
        {
            transform.position = OriginalPosition;
        }

        var worldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        var colliders = Physics2D.OverlapPointAll(worldPosition);
        foreach (var collider in colliders)
        {
            var dragRecipent = collider.gameObject.GetComponent<DragObjectRecipient>();
            if (dragRecipent != null)
            {
                dragRecipent.OnDraggedOn(this);
                OnDragged(dragRecipent);
            }
        }

        List<DragObjectRecipient> dragObjectRecipientsToRemove = new List<DragObjectRecipient>();
        foreach (var hoveringDraggableObjects in HoveringDraggableObjects)
        {
            hoveringDraggableObjects.OnDraggedOnExit(this);
        }
        HoveringDraggableObjects.Clear();

        PawsController.Instance?.SetHoldingObject(false);
        Debug.Log($"{gameObject.name}.OnEndDrag");
    }

    protected virtual void OnDragged(DragObjectRecipient Recipient)
    {
        // TODO @nox extend tool function here.
    }
}
