using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event Action OnPlayerStoppedMoving = delegate { };

    [field: SerializeField]
    private float MovementSpeedMultiplier { get; set; } = 1.0f;

    private Vector3 CachedStartPositionTransform { get; set; }
    private Vector3 MovementDestinationTransform { get; set; }

    private float MovementStartTime { get; set; }
    private float JourneyLength { get; set; }
    private bool IsMovementEnabled { get; set; } = false;

    public void MoveToDestionation(Vector3 destinationPosition)
    {
        MovementDestinationTransform = destinationPosition;
        CachedStartPositionTransform = transform.position;
        StartMovement();
    }

    public void SetStartPosition(Vector3 startPosition)
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        if (IsMovementEnabled == true)
        {
            float distanceCovered = (Time.time - MovementStartTime) * MovementSpeedMultiplier;
            float fractionOfJourney = 0;

            if (distanceCovered != 0)
            {
                fractionOfJourney = distanceCovered / JourneyLength;
            }

            transform.position = Vector3.Lerp(CachedStartPositionTransform, MovementDestinationTransform, fractionOfJourney);

            if (fractionOfJourney >= 1.0f)
            {
                IsMovementEnabled = false;
                OnPlayerStoppedMoving.Invoke();
            }
        }
    }

    private void StartMovement()
    {
        MovementStartTime = Time.time;
        JourneyLength = Vector3.Distance(CachedStartPositionTransform, MovementDestinationTransform);
        IsMovementEnabled = true;
    }
}
