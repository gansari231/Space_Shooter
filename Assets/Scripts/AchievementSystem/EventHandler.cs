using System;

public class EventHandler : SingletonGeneric<EventHandler>
{
    public event Action OnBulletFired;
    public event Action OnGameOver;

    public void InvokeOnBulletFired()
    {
        OnBulletFired?.Invoke();
    }

    public void InvokeOnGameOver()
    {
        OnGameOver?.Invoke();
    }
}
