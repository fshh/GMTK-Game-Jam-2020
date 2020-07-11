﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextSelection))]
public class PlayerInput : MonoBehaviour
{
    public static event Action<string> SubmitTextEvent;

    public TextMeshProUGUI conversationText;

    private string clipboard;

    private TextMeshProUGUI textMesh;
    private TextSelection inputSelection;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        inputSelection = GetComponent<TextSelection>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.C)) { Copy(); }
            else if (Input.GetKeyDown(KeyCode.V)) { Paste(); }
            else if (Input.GetKeyDown(KeyCode.X)) { Cut(); }
        }
        else if (Input.GetKeyDown(KeyCode.Backspace)) { Delete(); }
        else if (Input.GetKeyDown(KeyCode.Return)) { Submit(); }
    }

    private void Copy()
    {
        Debug.Log($"Copying \"{TextSelection.SelectedText}\"");
        clipboard = TextSelection.SelectedText + " ";
    }

    private void Paste()
    {
        Debug.Log($"Pasting \"{clipboard}\"");
        textMesh.text += clipboard;
    }

    private void Cut()
    {
        if (TextSelection.SelectedInstance == inputSelection)
        {
            Debug.Log($"Cutting \"{TextSelection.SelectedText}\"");
            clipboard = TextSelection.SelectedText + " ";
            inputSelection.DeleteSelection();
        }
    }

    private void Delete()
    {
        if (TextSelection.SelectedInstance == inputSelection)
        {
            Debug.Log($"Deleting \"{TextSelection.SelectedText}\"");
            inputSelection.DeleteSelection();
        }
    }

    private void Submit()
    {
        if (textMesh.text.Length > 0)
        {
            Debug.Log($"Submitting \"{textMesh.text}\"");
            string submissionText = "> " + textMesh.text;
            conversationText.text += "\n\n" + submissionText;
            textMesh.text = "";

            if (SubmitTextEvent != null)
            {
                SubmitTextEvent(submissionText);
            }
        }
    }
}