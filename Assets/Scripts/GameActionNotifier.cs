using System;

public static class GameActionNotifier
{
    public static event Action<int> OnSpawnMapRequested = delegate { };
    public static event Action OnCameraResetRequested = delegate { };

    public static void NotifyOnSpawnMapRequested(int mapSize)
    {
        OnSpawnMapRequested.Invoke(mapSize);
    }

    public static void NotifyOnCameraResetRequested()
    {
        OnCameraResetRequested.Invoke();
    }
}
