using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameEnding gameEnding;

    [Header("FOV Settings")]
    public float fovAngleThreshold = 0.7f; // Lower = wider FOV
    public float peripheralThreshold = 0.3f; // Slower alert buildup in peripherals as opposed to direct LOS

    [Header("Alert Settings")]
    public float maxAlertLevel = 1f;
    public float alertBuildSpeed = 0.6f;
    public float alertDecaySpeed = 0.2f;
    public float detectionRange = 5f;

    float m_AlertLevel = 0f;
    bool m_IsPlayerInRange;
    bool m_IsGameEnded = false;

    //void OnTriggerEnter (Collider other)
    //{
    //    if (other.transform == player)
    //    {
    //        m_IsPlayerInRange = true;
    //    }
    //}

    //void OnTriggerExit (Collider other)
    //{
    //    if (other.transform == player)
    //    {
    //        m_IsPlayerInRange = false;
    //    }
    //}

    void Update ()
    {
        if (m_IsGameEnded) return;

        if (InRange() && HasLineOfSight())
        {
            float dot = GetDotToPlayer();

            if (dot > fovAngleThreshold)
            {
                // Scale build speed by how directly the enemy faces the player
                // dot near 1.0 (dead center) is full speed whereas closer to the threshold would be slower
                float normalizedDot = Mathf.InverseLerp(fovAngleThreshold, 1f, dot);
                m_AlertLevel += alertBuildSpeed * normalizedDot * Time.deltaTime;
            }
            else
            {
                // Player is in range and unobstructed, but in peripheral vision
                m_AlertLevel += (alertBuildSpeed * 0.1f) * Time.deltaTime;
            }
        }
        else
        {
            // Decay alert when out of sight or out of range
            m_AlertLevel -= alertDecaySpeed * Time.deltaTime;
        }

        UnityEngine.Debug.Log($"{gameObject.name} Alert: {m_AlertLevel:F2} | InRange: {m_IsPlayerInRange} | LOS: {HasLineOfSight()} | Dot: {GetDotToPlayer():F2}");
        m_AlertLevel = Mathf.Clamp(m_AlertLevel, 0f, maxAlertLevel);

        if (m_AlertLevel >= maxAlertLevel)
        {
            m_IsGameEnded = true;
            gameEnding.CaughtPlayer();
        }
    }

    // Helper functions for Update
    bool HasLineOfSight()
    {
        Vector3 direction = player.position - transform.position + Vector3.up;
        Ray ray = new Ray(transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit))
            return hit.collider.transform == player;

        return false;
    }

    float GetDotToPlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        return Vector3.Dot(transform.forward, dirToPlayer);
    }

    bool InRange()
    {
        return Vector3.Distance(transform.position, player.position) < detectionRange;
    }

    public float GetAlertLevel() => m_AlertLevel / maxAlertLevel; // Returns float between 0.0 and 1.0
}
