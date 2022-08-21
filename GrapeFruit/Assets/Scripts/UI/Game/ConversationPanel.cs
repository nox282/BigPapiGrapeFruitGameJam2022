using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationPanel : UIPanel
{
	[field: SerializeField]
	public TMP_Text Text { get; private set; }
	
	public float TimeToAutoNext = 6f;

	private Queue<string> _toDisplay = new Queue<string>();
	
	private float _closeTimer;


	public bool HasDialog { get; private set; }

	public void AddDialog(string dialog)
	{
		HasDialog = true;
		_toDisplay.Enqueue(dialog);
	}

	public void ShowNext(bool timeout = false)
	{
		if (_toDisplay.Count > 0)
		{
			var nextText = _toDisplay.Dequeue();
			Text.text = nextText;
			Open();

			_closeTimer = TimeToAutoNext;
		}
		else if (timeout)
		{
			HasDialog = false;
			Close();
			_closeTimer = -1;
		}
	}

	private void Update()
	{
		if (_closeTimer > 0)
        {
			_closeTimer -= Time.deltaTime;

			if (_closeTimer <= 0)
            {
				ShowNext(true);
            }
        }
	}
}
