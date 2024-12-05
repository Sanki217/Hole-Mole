using UnityEngine;
using Mirror;

public class MainMenu : MonoBehaviour, IWindow
{
    private CustomNetworkManager manager => NetworkManager.singleton as CustomNetworkManager;

    private NetworkGameStateManager networkGameStateManager;

    [SerializeField] private GameObject mainMenuToggle;
    [SerializeField] private GameObject mainMenuButtonsToggle;
    [SerializeField] private GameObject lobbyToggle;

    [SerializeField] private bool useSteam;

    [SerializeField] private SteamLobby steamLobby;

    public void Start()
    {
        GameManager.Instance.TryGetManager(out networkGameStateManager);

        networkGameStateManager.OnNetworkGameStateSet += OnNetworkGameStateSet;
    }

    public void OnClose()
    {

    }

    public void OnOpen()
    {

    }

    public void HostButton()
    {
        if(useSteam) { steamLobby.HostLobby(); }
        else { manager.HostLocal(); }    
    }

    public void JoinButton()
    {
        manager.JoinLocal();
    }

    public void StartGameButton()
    {
        networkGameStateManager.ChangeGameState(NetworkGameState.Game);
    }

    private void OnNetworkGameStateSet(NetworkGameState networkGameState)
    {
        switch(networkGameState)
        {
            case NetworkGameState.Lobby: ShowLobby(); break;
            case NetworkGameState.Game: HideMainMenu(); break;
        }
    }

    private void ShowLobby()
    {
        mainMenuButtonsToggle.SetActive(false);

        mainMenuToggle.SetActive(true);
        lobbyToggle.SetActive(true);
    }
    private void HideMainMenu()
    {
        mainMenuToggle.SetActive(false);
    }

    private void ShowMainMenu()
    {
        mainMenuToggle.SetActive(true);
        mainMenuButtonsToggle.SetActive(true);

        lobbyToggle.SetActive(false);
    }
}
