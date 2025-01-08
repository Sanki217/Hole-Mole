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

    [SerializeField] private AudioClip selectionSound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip clickSound;

    [SerializeField] private AudioSource ambientAudioSource;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GameManager.Instance.TryGetManager(out networkGameStateManager);

        networkGameStateManager.OnNetworkGameStateSet += OnNetworkGameStateSet;
    }

    public void PlaySelectionSound()
    {
        PlaySound(selectionSound);
    }

    public void PlayCoinSound(float volume = 1.0f)
    {
        PlaySound(coinSound, volume);
    }

    public void PlayClickSound()
    {
        PlaySound(clickSound);
    }

    private void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    public void OnClose()
    {
    }

    public void OnOpen()
    {
    }

    public void HostButton()
    {
        PlayCoinSound(0.5f); // Przyk³adowo, zmniejszamy g³oœnoœæ do 50%

        if (useSteam)
        {
            steamLobby.HostLobby();
        }
        else
        {
            manager.HostLocal();
        }
    }

    public void JoinButton()
    {
        PlayCoinSound(0.5f); // Przyk³adowo, zmniejszamy g³oœnoœæ do 50%
        manager.JoinLocal();
    }

    public void StartGameButton()
    {
        if (ambientAudioSource != null)
        {
            ambientAudioSource.Stop();
        }

        networkGameStateManager.ChangeGameState(NetworkGameState.Game);
    }

    public void ServerListButton()
    {
        PlayClickSound();
        // Kod do wyœwietlenia listy serwerów
    }

    private void OnNetworkGameStateSet(NetworkGameState networkGameState)
    {
        switch (networkGameState)
        {
            case NetworkGameState.Lobby:
                ShowLobby();
                break;
            case NetworkGameState.Game:
                HideMainMenu();
                break;
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