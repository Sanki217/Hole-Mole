using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;

    private static GameManager instance;

    public List<IManager> _managers = new List<IManager>();

    [SerializeField] private GameObject[] GameObjectManagers = new GameObject[0];

    private void Awake()
    {
        if (Instance == null) { instance = this; }
        else if (instance != this) { Destroy(this); }

        foreach (GameObject go in GameObjectManagers)
        {
            if (go.TryGetComponent(out IManager imanager))
                _managers.Add(imanager);
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool TryGetManager<T>(out T manager)
    {
        foreach (IManager possibleManager in _managers)
        {
            if (possibleManager is T)
            {
                manager = (T)possibleManager;
                return true;
            }
        }

        manager = default;
        Debug.LogError($"Manager {typeof(T)} was not found!");
        return false;
    }
}
