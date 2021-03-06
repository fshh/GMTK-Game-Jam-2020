﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAppearance : MonoBehaviour
{
    public float delayInSeconds;

    void Start()
    {
        gameObject.SetActive(false);
        Invoke("Appear", delayInSeconds);
    }

    public void Appear()
    {
        gameObject.SetActive(true);
    }
}
