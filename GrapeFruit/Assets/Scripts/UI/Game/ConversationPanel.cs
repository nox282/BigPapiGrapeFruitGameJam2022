using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ConversationPanel : UIPanel
{
	public GameObject Helper;

	[field: SerializeField]
	public TMP_Text Text { get; private set; }

	public float TimeToDisplayHelp = 6f;

	private Queue<string> _toDisplay = new Queue<string>();

	private float _counter;

	public void AddDialog(string dialog)
	{
		_toDisplay.Enqueue(dialog);
	}

	public void ShowNext()
	{
		if(Helper != null)
		{	
			Helper.SetActive(false);
		}
		if (_toDisplay.Count > 0)
		{
			var nextText = _toDisplay.Dequeue();
			Text.text = nextText;
			_counter = TimeToDisplayHelp;
			Open();
		}
		else
		{
			Close();
			_counter = 0;
		}
	}

	private void Update()
	{
		if(_counter > 0)
		{
			_counter -= Time.deltaTime;
			if(_counter <= 0 && Helper != null)
			{
				Helper.SetActive(true);
			}
		}
	}
}
