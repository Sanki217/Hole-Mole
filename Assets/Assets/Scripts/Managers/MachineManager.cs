using UnityEngine;

public class MachineManager : MonoBehaviour, IManager
{
    [SerializeField] private Machine PlayerOneMachine;
    [SerializeField] private Machine PlayerTwoMachine;

    public Machine GetMachine(int playerID)
    {
        return playerID == 1 ? PlayerOneMachine : PlayerTwoMachine;
    }

    public void SendHit(int key, int PlayerID)
    {
        Debug.Log("PlayerID " + PlayerID + "/ Key " + key);
    }
}
