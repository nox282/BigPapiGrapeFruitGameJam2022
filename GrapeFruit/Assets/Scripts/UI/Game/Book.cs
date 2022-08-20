using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
	public Button PreviousPage;
	public Button NextPage;
	public GameObject[] Pages;

	private int _currentPageIdx = 0;

	private void Awake()
	{
		PreviousPage.onClick.AddListener(GoToPreviousPage);
		NextPage.onClick.AddListener(GoToNextPage);
		GoToPage(0);
	}

	private void GoToPreviousPage()
	{
		GoToPage(_currentPageIdx - 1);
	}

	private void GoToNextPage()
	{
		GoToPage(_currentPageIdx + 1);
	}

	private void GoToPage(int pageIndex)
	{
		if(Pages.Length == 0 || pageIndex < 0 || pageIndex >= Pages.Length)
		{
			return;
		}

		foreach(var page in Pages)
		{
			page.SetActive(false);
		}
		
		Pages[pageIndex].SetActive(true);
		_currentPageIdx = pageIndex;

		PreviousPage.gameObject.SetActive(pageIndex > 0);
		NextPage.gameObject.SetActive(pageIndex < Pages.Length - 1);
	}
}
