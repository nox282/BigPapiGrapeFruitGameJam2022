using UnityEngine;

public class Tool : DraggableObject
{
    [field: SerializeField]
    public ToolData ToolData { get; private set; }
}
