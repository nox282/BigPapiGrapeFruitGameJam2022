using UnityEngine;
using UnityEngine.EventSystems;

public class PanelOpener : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UIPanel UIPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        UIPanel.Open();
    }
}
