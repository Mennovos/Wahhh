using UnityEngine;

/// <summary>
/// Pickup that infects the player with "Aids" when collected. Inherits the existing Pickup behavior.
/// Configure duration and multiplier in the inspector.
/// </summary>
public class AidsPickup : Pickup
{
    [Tooltip("How long the infection lasts in seconds")]
    [SerializeField] private float infectionDuration = 20f;

    [Tooltip("Speed multiplier while infected (0..1). 0.5 => 50% speed")]
    [SerializeField, Range(0f, 1f)] private float infectionMultiplier = 0.5f;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (!collected)
        {
            var vm = collision.gameObject.GetComponent<VirusMeter>();
            if (vm != null)
            {
                vm.Infect(infectionDuration, infectionMultiplier);
            }

            // Let base class handle coins / destroy
            base.OnCollisionEnter(collision);
        }
    }
}
