using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Data/ToolData", order = 1)]
public class ToolData : ScriptableObject
{
	[field: SerializeField]
	public string Name { get; private set; }

	[field: SerializeField]
	public Texture2D Icon { get; private set; }

	[field: SerializeField]
	public string Description { get; private set; }
}
