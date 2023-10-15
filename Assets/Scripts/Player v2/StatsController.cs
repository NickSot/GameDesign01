using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public float GravityScale { get; private set; } = 1;
    public float JumpVelocity { get; private set; }
    public float JumpHeight { get; private set; } = 30f;
    public float JumpTimeToApex { get; private set; } = 2.7f;
    public float JumpCutModifier { get; private set; } = 0.2f;
    public int AirJumps { get; private set; } = 0;
    public float MoveSpeed { get; private set; } = 5f;
    public float Acceleration { get; private set; } = 3f;
    public float Deceleration { get; private set; } = 3f;
    public float VelocityPower { get; private set; } = 1f;
    public float Friction { get; private set; } = 0.000f;
    public float Grip { get; private set; } = 0.1f;

    private void Start() {
        CalculateGravity();
        ChangeGrip(GetComponent<PlayerManager>().PartManager.HasAbility);
    }

    private void OnEnable() {
        BodyPartManager.OnAbiltiesChanged += ChangeGrip;
        GetComponent<PlayerManager>().PartManager.UpdateEventListeners();
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

    public void ChangeGrip(bool[] abilities) {
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
