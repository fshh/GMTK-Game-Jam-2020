﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowManager : Singleton<WindowManager>
{
	private Dictionary<ApplicationSO, List<Window>> openWindowsGrouped = new Dictionary<ApplicationSO, List<Window>>();
	private List<Window> openWindowsAll = new List<Window>();
	private Window focusedWindow;

	public List<Window> GetWindows()
	{
		return openWindowsAll;
	}

	public List<Window> GetWindows(ApplicationSO app)
	{
		return openWindowsGrouped[app];
	}

	public bool HasWindowsOpen(ApplicationSO app)
	{
		return NumWindowsOpen(app) > 0;
	}

	public int NumWindowsOpen(ApplicationSO app)
	{
		if (openWindowsGrouped.ContainsKey(app))
		{
			return openWindowsGrouped[app].Count;
		}

		return 0;
	}

	public bool HasWindowFocused(ApplicationSO app)
	{
		return focusedWindow != null && focusedWindow.App == app;
	}

	public Window GetFocusedWindow()
	{
		return focusedWindow;
	}

	public void AddWindow(Window window)
	{
		if (!openWindowsGrouped.ContainsKey(window.App))
		{
			openWindowsGrouped.Add(window.App, new List<Window>());
		}

		if (!openWindowsGrouped[window.App].Contains(window))
		{
			openWindowsGrouped[window.App].Add(window);
		}

		if(!openWindowsAll.Contains(window))
		{
			openWindowsAll.Add(window);
		}

		window.transform.SetParent(transform);
		window.transform.localScale = Vector3.one;
		window.Focus();
	}

	public void RemoveWindow(Window window)
	{
		if (openWindowsGrouped.ContainsKey(window.App) && openWindowsGrouped[window.App].Contains(window))
		{
			openWindowsGrouped[window.App].Remove(window);
		}

		if (openWindowsAll.Contains(window))
		{
			openWindowsAll.Remove(window);
		}
	}

	public void FocusWindow(Window window)
	{
		window.transform.SetAsLastSibling();
		focusedWindow = window;
	}

	public void MinimizeWindow(Window window)
    {
		window.transform.SetAsFirstSibling();
		if (focusedWindow == window)
        {
			for (int ii = transform.childCount - 1; ii >= 0; ii--)
            {
				Window w = transform.GetChild(ii).GetComponent<Window>();
				if (w != window && !w.Minimized)
                {
					focusedWindow = w;
					return;
                }
            }
			focusedWindow = null;
        }
    }
}
