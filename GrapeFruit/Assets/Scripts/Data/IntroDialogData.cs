using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "IntroDialogData", menuName = "Data/IntroDialogData", order = 0)]
public class IntroDialogData : ScriptableObject
{
	[field: SerializeField]
	public string[] GenericArrivalDialogs { get; private set; }

	private List<string> _arrivalDialogs = new List<string>();

	private void OnEnable()
	{
		_arrivalDialogs.Clear();
	}

	public string GetRandomArrivalDialog()
	{
		if (_arrivalDialogs.Count == 0)
		{
			_arrivalDialogs.AddRange(GenericArrivalDialogs);
			var rng = new System.Random();
			_arrivalDialogs.OrderBy(a => rng.Next());
		}

		var randomIndex = Random.Range(0, _arrivalDialogs.Count - 1);
		var toReturn = _arrivalDialogs[randomIndex];
		_arrivalDialogs.Remove(toReturn);

		return toReturn;
	}
}
