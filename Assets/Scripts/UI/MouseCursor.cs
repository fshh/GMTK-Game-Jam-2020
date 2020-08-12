﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseCursor : MonoBehaviour
{
    Vector2 cursorPosition;
    Image image;
    public Sprite loadingCursor;
    public Sprite defaultCursor;
    public Sprite smallCursor;
    public AudioClip clickingSound;

    public bool locked;

    private void Start()
    {
        Cursor.visible = false;
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        image = GetComponent<Image>();
    }

    void Update()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPosition;
        if (Input.GetMouseButtonDown(0))
        {
            SwitchToSmallCursor();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SwitchToDefaultCursor();
        }
    }

    private void OnMouseDown()
    {
        if (clickingSound != null)
        {
            AudioSource.PlayClipAtPoint(clickingSound, Camera.main.transform.position);
        }
    }

    public void SwitchToLoadingCursor()
    {
        image.sprite = loadingCursor;
    }

    public void SwitchToDefaultCursor()
    {
        image.sprite = defaultCursor;
        image.color = Color.white;
    }

    public void SwitchToSmallCursor()
    {
        image.sprite = smallCursor;
        image.color = Color.grey;
    }
}
