using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TempSceneRefs
{
    public ChatDisplay chat_display
    {
        get
        {
            if (chat_display_ == null)
                chat_display_ = GameObject.FindObjectOfType<ChatDisplay>();

            return chat_display_;
        }
    }


    public ObjectiveManager objective_manager
    {
        get
        {
            if (objective_manager_ == null)
                objective_manager_ = GameObject.FindObjectOfType<ObjectiveManager>();

            return objective_manager_;
        }
    }


    public DistanceIndicator distance_indicator
    {
        get
        {
            if (distance_indicator_ == null)
                distance_indicator_ = GameObject.FindObjectOfType<DistanceIndicator>();

            return distance_indicator_;
        }
    }


    public MoneyPanel money_panel
    {
        get
        {
            if (money_panel_ == null)
                money_panel_ = GameObject.FindObjectOfType<MoneyPanel>();

            return money_panel_;
        }
    }


    public Truck player_truck
    {
        get
        {
            if (player_truck_ == null)
                player_truck_ = GameObject.FindObjectOfType<Truck>();

            return player_truck_;
        }
    }


    public Trailer player_trailer
    {
        get
        {
            if (player_trailer_ == null)
                player_trailer_ = GameObject.FindObjectOfType<Trailer>();

            return player_trailer_;
        }
    }


    private MoneyPanel money_panel_;
    private ObjectiveManager objective_manager_;
    private DistanceIndicator distance_indicator_;
    private Truck player_truck_;
    private Trailer player_trailer_;
    private ChatDisplay chat_display_;
}
