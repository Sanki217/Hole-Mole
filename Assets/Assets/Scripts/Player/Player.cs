using Steamworks;
using Mirror;
using System;

public class Player : NetworkBehaviour
{
    public event Action<string> OnPlayerNameSet;

    [SyncVar(hook = nameof(ClientHandleNameSet))]
    public string PlayerName;

    [SyncVar(hook = nameof(ClientHandleScoreSet))]
    public int PlayerScore;

    [SyncVar(hook = nameof(ClientHandleScoreSet))]
    public int PlayerScoreMultiplier;

    [SyncVar]
    private int playerID;

    private int successStreak;

    private PlayerInputManager playerInputManager;
    private MachineManager machineManager;
    private ScoreManager scoreManager;
    private HitManager hitManager;

    private void Awake()
    {
        GameManager.Instance.TryGetManager(out playerInputManager);
        GameManager.Instance.TryGetManager(out machineManager);
        GameManager.Instance.TryGetManager(out scoreManager);
        GameManager.Instance.TryGetManager(out hitManager);
    }

    public override void OnStartClient()
    {
        if (GameManager.Instance.TryGetManager(out PlayerManager playerManager))
        {
            playerManager.AddPlayer(this);

            if (NetworkServer.active)
                playerID = playerManager.PlayerCount;

            PlayerScoreMultiplier = 1;
        }

        if(isOwned)
        {
            string name = SteamFriends.GetPersonaName();
            CmdSetName(name);
            playerInputManager.OnKeyPressed += OnNumpadPressed;
        }

    }

    public override void OnStopClient()
    {
        if (GameManager.Instance.TryGetManager(out PlayerManager playerManager))
            playerManager.RemovePlayer(this);
    }

    private void ClientHandleNameSet(string oldValue, string newValue)
    {
        OnPlayerNameSet?.Invoke(newValue);
    }

    private void ClientHandleScoreSet(int oldValue, int newValue)
    {
        scoreManager.DisplayScore(playerID, PlayerScore, PlayerScoreMultiplier);
    }

    private void OnNumpadPressed(int key)
    {
        Machine machine = machineManager.GetMachine(playerID);

        bool hit = machine.CheckHit(key);

        HitInfo hitInfo = new HitInfo();

        hitInfo.Success = hit;
        hitInfo.Key = key;

        CmdSendHit(hitInfo);
    }

    [Command]
    private void CmdSetName(string name)
    {
        PlayerName = name;
    }

    [Command]
    private void CmdSendHit(HitInfo info)
    {
        info.PlayerID = playerID;

        if(info.Success) { ServerOnSuccessHit(); }
        else { ServerOnMissHit(); }

        hitManager.ServerBroadcastHit(info);
    }

    private void ServerOnSuccessHit()
    {
        successStreak++;

        if(successStreak >= 3)
        {
            PlayerScoreMultiplier++;
            successStreak = 0;
        }

        PlayerScore += 100 * PlayerScoreMultiplier;
    }

    private void ServerOnMissHit()
    {
        PlayerScoreMultiplier = 1;
        successStreak = 0;
    }

}

public struct HitInfo
{
    public int PlayerID;
    public int Key;

    public bool Success;
}
