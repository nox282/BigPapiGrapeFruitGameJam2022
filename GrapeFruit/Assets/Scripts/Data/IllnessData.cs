using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IllnessData", menuName ="Data/Illness",order = 0)]
public class IllnessData : ScriptableObject
{
	[field: SerializeField]
	public string Name { get; private set; }

	[field: SerializeField]
	public Texture2D Icon { get; private set; }

	[field: SerializeField]
	public string Description { get; private set; }

	[field: SerializeField]
	public string Conversation { get; private set; }

	[field: SerializeField]
	public SymptomData[] Symptoms { get; private set; }

	[field: SerializeField]
	public TreatmentData[] Treatments { get; private set; }
}
