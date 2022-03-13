using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerAccount
{
    [Header("Player Data")]
    public string playerName;
    public int playerNumber;
    public Sprite playerIcon;
    public Color playerColor;
    
    public List<Building> allPlayerBuildings = new List<Building>();

    public List<Building> miniTowers = new List<Building>();
    public List<Building> bigTowerBuildings = new List<Building>();
    public List<Conveyor> conveyors = new List<Conveyor>();

}
