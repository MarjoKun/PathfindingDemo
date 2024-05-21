using System.Collections.Generic;
using UnityEngine;

public class PlayerPathfindingController : MonoBehaviour
{
    [field: SerializeField]
    private PlayerMovement BoundPlayerMovement { get; set; }

    private List<PathNode> CurrentPathingNodesCollection { get; set; } = new List<PathNode>();

    private int CurrentPathingIndex { get; set; } = 0;

    public void StartPlayerMovement()
    {
        MovePlayerToNextDestination();
    }

    private void Start()
    {
        AttachToEvents();
    }

    private void OnDestroy()
    {
        DetachFromEvents();
    }

    private void MovePlayerToNextDestination()
    {
        if (CurrentPathingNodesCollection.Count > CurrentPathingIndex)
        {
            Vector3 destinationVector = new Vector3(CurrentPathingNodesCollection[CurrentPathingIndex].nodeWidth, 0, CurrentPathingNodesCollection[CurrentPathingIndex].nodeHeight);

            if (CurrentPathingIndex == 0)
            {
                BoundPlayerMovement.SetStartPosition(destinationVector);
            }

            BoundPlayerMovement.MoveToDestionation(destinationVector);
            CurrentPathingIndex++;
        }
        else
        {
            CurrentPathingIndex = 0;
        }
    }

    private void HandleOnPathFound(List<PathNode> pathNodeCollection)
    {
        CurrentPathingNodesCollection.Clear();
        CurrentPathingNodesCollection.AddRange(pathNodeCollection);
    }

    private void HandleOnPlayerStoppedMoving()
    {
        MovePlayerToNextDestination();
    }

    private void AttachToEvents()
    {
        GameActionNotifier.OnPathFound += HandleOnPathFound;
        BoundPlayerMovement.OnPlayerStoppedMoving += HandleOnPlayerStoppedMoving;
    }

    private void DetachFromEvents()
    {
        GameActionNotifier.OnPathFound -= HandleOnPathFound;
        BoundPlayerMovement.OnPlayerStoppedMoving -= HandleOnPlayerStoppedMoving;
    }
}
