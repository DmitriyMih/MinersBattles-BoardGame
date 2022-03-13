using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloGameManager : MonoBehaviour
{
    public static SoloGameManager soloGameManager;
    public GlobalGridController globalGridController;

    public List<PlayerAccount> playerAccounts = new List<PlayerAccount>();

    public int currentAccountNumber;
    public PlayerAccount currentmainAccount;
    [Header("View settings")]
    public PlayerBuildingStatsView statsView;

    public bool stop = true;
    public void Awake()
    {
        if (stop == true)
            return;

        soloGameManager = this;

        //if (PlayerMenuManager.playerMenuManager.playerAccountList.Count == 0 || PlayerMenuManager.playerMenuManager == null)
        //    return;

        //playerAccounts.Clear();
        //playerAccounts.AddRange(PlayerMenuManager.playerMenuManager.playerAccountList);

        currentAccountNumber = 0;
        currentmainAccount = playerAccounts[currentAccountNumber];
        statsView.PlayerInitialization(currentmainAccount);

        globalGridController.InitializationPlayersColor(playerAccounts);
    }

    public void CharacterAccountChangeToggle()
    {
        currentAccountNumber += 1;
        if (currentAccountNumber > playerAccounts.Count - 1)
            currentAccountNumber = 0;

        currentmainAccount = playerAccounts[currentAccountNumber];
        statsView.PlayerInitialization(currentmainAccount);

        globalGridController.InitializationPlayersColor(playerAccounts);
    }

    public static void InitializationList(List<PlayerAccount> accounts)
    {
        soloGameManager.playerAccounts.AddRange(accounts);
        Debug.Log("INIT " + soloGameManager.playerAccounts.Count);
        soloGameManager.globalGridController.InitializationPlayersColor(accounts);
    }
}
