using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public bool IsOpened()
    {
        return gameObject.activeSelf;
    }
}
