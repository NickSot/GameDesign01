using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public float GravityScale { get; private set; } = 1;
    public float JumpVelocity { get; private set; }
    [SerializeField] public float JumpHeight { get; private set; } = 30f;
    [SerializeField] public float JumpTimeToApex { get; private set; } = 2.7f;
    [SerializeField] public float JumpCutModifier { get; private set; } = 0.2f;
    [SerializeField] public int AirJumps { get; private set; } = 0;
    [SerializeField] public float MoveSpeed { get; private set; } = 5f;
    [SerializeField] public float Acceleration { get; private set; } = 3f;
    [SerializeField] public float Deceleration { get; private set; } = 3f;
    [SerializeField] public float VelocityPower { get; private set; } = 1f;
    [SerializeField] public float Friction { get; private set; } = 0.0001f;
    [SerializeField] public float Grip { get; private set; } = 0.1f;

    private void Start() {
        CalculateGravity();
        BodyPartManager.OnAbiltiesChanged += ChangeGrip;
    }

    private void OnDisable() {
        BodyPartManager.OnAbiltiesChanged -= ChangeGrip;
    }

    private void OnValidate() {
        CalculateGravity();
    }

    private void CalculateGravity() {
        GravityScale = (2 * JumpHeight) / Mathf.Pow(JumpTimeToApex, 2);
        JumpVelocity = GravityScale * JumpTimeToApex;
    }

    private void ChangeGrip(bool[] abilities) {
        if (abilities.Length == Enum.GetNames(typeof(PlayerAbilities)).Length) {
            if (abilities[(int)PlayerAbilities.Roll]) {
                Grip = 0f;
                Friction = 0f;
            }
            if (abilities[(int)PlayerAbilities.Walk]) {
                Grip = 0.1f;
                Friction = 0.1f;
            }
        } else {
            Debug.Log("ERROR changing grip, array passed by bodypartManager has incorrect size");
        }
    }
}
