using UnityEngine;

/// <summary>
/// Simple "Aids" virus mechanic. Attach to the Player gameobject.
/// Call Infect(...) to apply a speed penalty for a duration.
/// </summary>
public class VirusMeter : MonoBehaviour
{
    [Tooltip("Current infection state")]
    public bool isInfected { get; private set; }

    [Tooltip("Multiplier applied to base speed while infected (0..1). 0.5 = 50% speed.")]
    [SerializeField] private float slowMultiplier = 0.5f;

    [Tooltip("Remaining time (seconds) of the current infection")]
    public float remainingTime { get; private set; }

    // Infect the player for durationSeconds and apply multiplier (0..1).
    // Example: Infect(30f, 0.6f) -> 30s at 60% speed.
    public void Infect(float durationSeconds, float multiplier = 0.5f)
    {
        if (durationSeconds <= 0f) return;
        slowMultiplier = Mathf.Clamp01(multiplier);
        remainingTime = durationSeconds;
        isInfected = true;
    }

    // Immediately cure the infection
    public void Cure()
    {
        isInfected = false;
        remainingTime = 0f;
    }

    // Returns modified speed based on whether the player is infected.
    public float ModifiedSpeed(float baseSpeed)
    {
        return isInfected ? baseSpeed * slowMultiplier : baseSpeed;
    }

    private void Update()
    {
        if (!isInfected) return;

        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            Cure();
        }
    }
}
