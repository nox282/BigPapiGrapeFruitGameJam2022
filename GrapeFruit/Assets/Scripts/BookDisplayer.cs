using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BookDisplayer : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ToDisplay;
    public Vector3 Offset;
    private Vector3 _originalPos;

    public UnityEvent OnClicked;

    private void Awake()
    {
        _originalPos = transform.position;

        ToDisplay.SetActive(false);
    }

    private void OnEnable()
    {
        transform.position = _originalPos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToDisplay.SetActive(true);
        gameObject.SetActive(false);
        OnClicked.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.position = _originalPos + Offset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = _originalPos;
    }
}
