using UnityEngine;
using System.Collections;

public class CollisionSoundTrigger : MonoBehaviour
{
    // Wwise event
    public AK.Wwise.Event collisionSoundEvent;

    // Flag to control collision detection
    private bool canDetectCollision = true;

    // Delay time in milliseconds
    [Tooltip("Delay time in milliseconds")]
    public float delayTimeInMilliseconds = 10f;

    // Called when the object starts colliding
    private void OnCollisionEnter(Collision collision)
    {
        if (canDetectCollision)
        {
            // Ensure the Wwise event is assigned
            if (collisionSoundEvent != null)
            {
                // Trigger the Wwise event to play the collision sound
                collisionSoundEvent.Post(gameObject);
            }
            else
            {
                Debug.LogWarning("No Wwise event assigned for collision sound.");
            }

            // Stop detecting collisions and start the delay coroutine to re-enable collision detection
            canDetectCollision = false;
            StartCoroutine(EnableCollisionDetectionAfterDelay());
        }
    }

    // Coroutine to enable collision detection after a delay
    private IEnumerator EnableCollisionDetectionAfterDelay()
    {
        // Convert delay time from milliseconds to seconds
        float delayTimeInSeconds = delayTimeInMilliseconds / 1000f;

        yield return new WaitForSeconds(delayTimeInSeconds);

        // Re-enable collision detection
        canDetectCollision = true;
    }
}
