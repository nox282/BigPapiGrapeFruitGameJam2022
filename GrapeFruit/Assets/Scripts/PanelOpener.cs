using UnityEngine;

public class PanelOpener : ClickableObject
{
    [SerializeField] private UIPanel UIPanel;

    public override void OnClicked()
    {
        base.OnClicked();
        UIPanel.Open();
    }
}
