using UnityEngine;

[CreateAssetMenu(fileName = "SymptomData", menuName = "Data/SymptomData", order = 1)]
public class SymptomData : ScriptableObject
{
    [field: SerializeField]
    public string Description { get; private set; }

    [field: SerializeField]
    public ToolData RequiredTool { get; private set; }

    [field: SerializeField]
    public float FeedbackFloatValue { get; private set; }
}
