using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TempSceneRefs
{
    public ObjectiveManager objective_manager
    {
        get { return objective_manager_ ?? (objective_manager_ = GameObject.FindObjectOfType<ObjectiveManager>()); }
    }


    public DistanceIndicator distance_indicator
    {
        get { return distance_indicator_ ?? (distance_indicator_ = GameObject.FindObjectOfType<DistanceIndicator>()); }
    }


    public MoneyPanel money_panel
    {
        get { return money_panel_ ?? (money_panel_ = GameObject.FindObjectOfType<MoneyPanel>()); }
    }


    public Truck player_truck
    {
        get { return player_truck_ ?? (player_truck_ = GameObject.FindObjectOfType<Truck>()); }
    }


    public Trailer player_trailer
    {
        get { return player_trailer_ ?? (player_trailer_ = GameObject.FindObjectOfType<Trailer>()); }
    }


    private MoneyPanel money_panel_;
    private ObjectiveManager objective_manager_;
    private DistanceIndicator distance_indicator_;
    private Truck player_truck_;
    private Trailer player_trailer_;
}
