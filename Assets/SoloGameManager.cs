using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloGameManager : MonoBehaviour
{
    public static SoloGameManager soloGameManager;
    public List<PlayerAccount> playerAccounts = new List<PlayerAccount>();

    public void Awake()
    {
        soloGameManager = this;
        playerAccounts.Clear();
        playerAccounts.AddRange(PlayerMenuManager.playerMenuManager.playerAccountList);
    }

    //public static void InitializationList(List<PlayerAccount> accounts)
    //{
    //    soloGameManager.playerAccounts.AddRange(accounts);
    //    Debug.Log("INIT " + soloGameManager.playerAccounts.Count);
    //}
}
