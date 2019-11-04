using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Health : ScriptableObject
{
    public float defaultHealthValue = 5, healthValue;

    private void Awake() {
        healthValue = defaultHealthValue;
    }
}
