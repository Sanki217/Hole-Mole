using UnityEngine.UI;
using UnityEngine;
using Mirror;

public class Lobby : MonoBehaviour
{
    [SerializeField] private Button startButton;

    void Start()
    {
        startButton.interactable = NetworkServer.active;
    }
}
