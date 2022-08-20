using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymptomData", menuName = "Data/SymptomData", order = 1)]
public class SymptomData : ScriptableObject
{
	[field: SerializeField]
	public string Name { get; private set; }

	[field: SerializeField]
	public Texture2D Icon { get; private set; }

	[field: SerializeField]
	public string Description { get; private set; }

	[field: SerializeField]
	public ToolData RequiredTool { get; private set; }
}
