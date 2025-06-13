using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StepCounter : MonoBehaviour
{
    private Vector3 previousAcceleration;
    private Vector3 currentAcceleration;
    private float threshold; // 튜닝 필요
    private int stepCount;
    public int StepCount { get { return stepCount; } set { stepCount = value; ChangeCount?.Invoke(); } }
    public UnityAction ChangeCount;

    private bool isCooldown;
    private float cooldownTime; // 최소 걸음 간격 (초)
    private float cooldownTimer;

    public Text stepText;

    private void Awake() => Init();

    private void Init()
    {
        threshold = 2.0f;
        stepCount = 0;
        stepText.text = "Steps: " + stepCount;
        Input.gyro.enabled = true;
        isCooldown = false;
        cooldownTime = 0.4f;
        cooldownTimer = 0f;
    }

    private void Update()
    {
        currentAcceleration = Input.gyro.rotationRateUnbiased;
        float delta = (currentAcceleration - previousAcceleration).magnitude;

        if (!isCooldown && delta > threshold)
        {
            stepCount++;
            stepText.text = "Steps: " + stepCount;

            // 쿨다운 시작
            isCooldown = true;
            cooldownTimer = 0f;
        }
        else if (isCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                isCooldown = false;
            }
        }

        previousAcceleration = currentAcceleration;
    }
}
