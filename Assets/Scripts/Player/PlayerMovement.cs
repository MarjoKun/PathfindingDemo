using Logic;
using Logic.Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public event Action OnPlayerStoppedMoving = delegate { };

        [field: SerializeField]
        private float MovementSpeedMultiplier { get; set; } = 1.0f;

        private Vector3 CachedDefaultPosition { get; set; }
        private Vector3 CachedStartPosition { get; set; }
        private Vector3 MovementDestination { get; set; }

        private float MovementStartTime { get; set; }
        private float JourneyLength { get; set; }
        private bool IsMovementEnabled { get; set; } = false;

        public void MoveToDestionation(Vector3 destinationPosition)
        {
            MovementDestination = destinationPosition;
            CachedStartPosition = transform.position;
            StartMovement();
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            transform.position = startPosition;
        }

        private void Start()
        {
            CachedDefaultPosition = transform.position;
            AttachToEvents();
        }

        private void OnDestroy()
        {
            DetachFromEvents();
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

                transform.position = Vector3.Lerp(CachedStartPosition, MovementDestination, fractionOfJourney);

                if (fractionOfJourney >= 1.0f)
                {
                    IsMovementEnabled = false;
                    OnPlayerStoppedMoving.Invoke();
                }
            }
        }

        private void StopPlayerMovement()
        {
            IsMovementEnabled = false;
            transform.position = CachedDefaultPosition;
        }

        private void StartMovement()
        {
            MovementStartTime = Time.time;
            JourneyLength = Vector3.Distance(CachedStartPosition, MovementDestination);
            IsMovementEnabled = true;
        }

        private void HandleOnSpawnMapRequested(int mapSize)
        {
            StopPlayerMovement();
        }

        private void HandleOnPathChanged(List<PathNode> pathNodeCollection)
        {
            StopPlayerMovement();
        }

        private void AttachToEvents()
        {
            GameActionNotifier.OnSpawnMapRequested += HandleOnSpawnMapRequested;
            GameActionNotifier.OnPathChanged += HandleOnPathChanged;
        }

        private void DetachFromEvents()
        {
            GameActionNotifier.OnSpawnMapRequested -= HandleOnSpawnMapRequested;
            GameActionNotifier.OnPathChanged -= HandleOnPathChanged;
        }
    }
}
