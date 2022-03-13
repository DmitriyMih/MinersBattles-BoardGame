using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBuildingStatsView : MonoBehaviour
{
    [Header("Account Info")]
    public PlayerBuildingStatsView statsView;
    public PlayerAccount currentAccount;

    [Header("Left UI")]
    public Image playerIcon;
    public TextMeshProUGUI playerName;
    public Image coloringBorder;

    [Header("Building Storage ")]
    public TextMeshProUGUI miniTowerCount;
    public TextMeshProUGUI bigTowerCount;
    public TextMeshProUGUI conveyorCount;

    public void Awake()
    {
        statsView = this;
    }

    public void PlayerInitialization(PlayerAccount currentPlayerAccount)
    {
        currentAccount = currentPlayerAccount;
        if (currentAccount == null)
            return;

        UpdatePlayerView();
    }

    public void UpdatePlayerView()
    {
        if (currentAccount == null)
            return;

        playerIcon.sprite = currentAccount.playerIcon;
        playerName.text = currentAccount.playerName;
        coloringBorder.color = currentAccount.playerColor;


        UpdatePlayerStorage();
    }

    public void UpdatePlayerStorage()
    {
        miniTowerCount.text = "- " + currentAccount.miniTowers.Count.ToString();
        bigTowerCount.text = "- " + currentAccount.bigTowerBuildings.Count.ToString();
        conveyorCount.text = "- " + currentAccount.conveyors.Count.ToString();


    }
}
