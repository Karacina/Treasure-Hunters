using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Cooldown
{
    [SerializeField] private float cooldown;
    private float _timesUp;

    public void Reset()
    {
        _timesUp = Time.time + cooldown;
    }

    public bool isReady => _timesUp <= Time.time;
}

