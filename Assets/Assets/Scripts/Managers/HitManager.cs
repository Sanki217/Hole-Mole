using UnityEngine;
using Mirror;
using System;

public class HitManager : NetworkBehaviour, IManager
{
    public event Action<HitInfo> OnHitInfoReceived;

    public void ServerBroadcastHit(HitInfo hitInfo)
    {
        BroadcastHitInfoRpc(hitInfo);
    }

    [ClientRpc]
    private void BroadcastHitInfoRpc(HitInfo hitInfo)
    {
        OnHitInfoReceived?.Invoke(hitInfo);
    }
}
