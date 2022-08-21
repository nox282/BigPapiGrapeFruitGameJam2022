using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationPanel : UIPanel
{
	public GameObject Helper;

	[field: SerializeField]
	public TMP_Text Text { get; private set; }

	public float TimeToDisplayHelp = 6f;

	private Queue<string> _toDisplay = new Queue<string>();

	private float _counter;

	private float _closeTimer;

	private const float totalTimer = 5f;

	public bool HasDialog { get; private set; }

	public void AddDialog(string dialog)
	{
		HasDialog = true;
		_toDisplay.Enqueue(dialog);
	}

	public void ShowNext()
	{
		if(Helper != null)
		{	
			//Helper.SetActive(false);
		}
		if (_toDisplay.Count > 0)
		{
			var nextText = _toDisplay.Dequeue();
			Text.text = nextText;
			_counter = TimeToDisplayHelp;
			Open();

			_closeTimer = totalTimer;
		}
		else
		{
			HasDialog = false;
			Close();
			_counter = 0;
			_closeTimer = -1;
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

		if (_closeTimer > 0)
        {
			_closeTimer -= Time.deltaTime;

			if (_closeTimer <= 0)
            {
				ShowNext();
            }
        }
	}
}
