using UnityEngine;
using Mirror;

public class Machine : NetworkBehaviour
{
    private SongManager songManager;
    private HitManager hitManager;

    [SerializeField] private Mole[] moles = new Mole[0];
    [SerializeField] private int playerID;

    private void Start()
    {
        GameManager.Instance.TryGetManager(out songManager);
        GameManager.Instance.TryGetManager(out hitManager);

        songManager.OnNoteBroadcasted += OnNoteBroadcasted;
        hitManager.OnHitInfoReceived += OnHitInfoReceived;
    }

    public bool CheckHit(int key)
    {
        return GetMole(key).Active;
    }

    private void OnHitInfoReceived(HitInfo hitInfo)
    {
        if (hitInfo.PlayerID != playerID) return;

        Mole mole = GetMole(hitInfo.Key);

        mole.ShowHit(hitInfo.Success);
    }

    private void OnNoteBroadcasted(Note note)
    {
        GetMole(note.Key).ActivateMole();
    }

    private Mole GetMole(int key)
    {
        int moleIndex = key - 1;

        return moles[moleIndex];
    }
}
