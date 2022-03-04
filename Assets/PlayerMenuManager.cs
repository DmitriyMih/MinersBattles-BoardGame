using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuManager : MonoBehaviour
{
    [Header("Player list settings")]
    public int minimumNimberOfPlayers = 2;
    public int maximumNumberOfPlayers = 6;
    public List<PlayerAccount> playerList = new List<PlayerAccount>();

    public void StartGameButton()
    {
        if (playerList.Count < minimumNimberOfPlayers || playerList.Count > maximumNumberOfPlayers)
            return;
    }

    [ContextMenu("Add Player")]
    public void AddPlayer()
    {
        if (playerList.Count >= maximumNumberOfPlayers)
            return;

        PlayerAccount newPlayer = new PlayerAccount();
        
        newPlayer.playerNumber = playerList.Count;

        playerList.Add(newPlayer);
    }

    [ContextMenu("Remove Last Player")]
    public void RemoveLastPlayer()
    {
        if (playerList.Count < 1)
            return;

        playerList.Remove(playerList[playerList.Count - 1]);
    }
    
    [ContextMenu("Remove All")]
    public void RemoveallPlayers()
    {
        playerList.Clear();
    }
}
