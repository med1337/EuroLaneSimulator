using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum ObjectiveState
{
    INITIAL_TRAILER_PICK_UP,
    RETRIEVING_LOST_TRAILER,
    DELIVERING_TRAILER
}


public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private float max_distance_from_last = 20;
    [SerializeField] private float min_distance_from_last = 10;
    [SerializeField] private GameObject trailer_prefab = null;

    private float min_squared = 0;
    private float max_squared = 0;
    private ObjectiveState objective_state = ObjectiveState.DELIVERING_TRAILER;//player starts with a trailer

    private Depot[] depots;
    private Depot last_depot_target = null;//will be a depot script instead
    private Depot current_depot_target = null;

    private Transform last_waypoint = null;
    private Transform current_waypoint = null;//could be switched to trailer instead of depot

    private GameObject current_trailer = null;

    void Start ()
	{
	    depots = FindObjectsOfType<Depot>();
        CalculateDistanceSquares();

	    if (GameManager.scene.player_truck == null)
	    {
            Debug.LogWarning("Player Truck not in scene! destroying objective manager!");
	        Destroy(this);
	        return;
	    }


        GameObject player_spawn = new GameObject("Player Spawn");//create a starting point for start delivery
	    player_spawn.transform.position = GameManager.scene.player_truck.transform.position;
	    current_waypoint = player_spawn.transform;
	    last_waypoint = current_waypoint;
	    //TODO set current trailer to one spawned with player

        SetNewObjective();
	}


    public void SetNewObjective()
    {
        last_depot_target = current_depot_target;
        last_waypoint = current_waypoint;
        current_depot_target = PickNewDepot();//find next optimal depot

        if (current_depot_target == null)
            return;

        if (GameManager.scene.player_truck.has_trailer)
        {
            objective_state = ObjectiveState.DELIVERING_TRAILER;//we must deliver it

            current_waypoint = current_depot_target.transform;//set new depot as target
            current_depot_target.delivery_area.enabled = true;//allow it to check for delivery
        }
        else
        {
            objective_state = ObjectiveState.INITIAL_TRAILER_PICK_UP;

            if (trailer_prefab == null)
            {
                Debug.LogError("Trailer prefab not set!");
                return;
            }

            current_trailer = Instantiate(trailer_prefab);//spawn trailer at depot
            current_trailer.transform.position = current_depot_target.transform.position;//and set it as target
            current_waypoint = current_trailer.transform;
            current_depot_target.delivery_area.enabled = false;//in case for some reason it's enabled
        }

        GameManager.scene.distance_indicator.SetRoute(last_waypoint, current_waypoint);
    }


    public Depot PickNewDepot()
    {
        Depot closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        CalculateDistanceSquares();

        foreach (Depot depot in depots)
        {
            Vector3 diff = depot.transform.position - position;
            float current_dist = diff.sqrMagnitude;

            if (depot == current_depot_target || depot == last_depot_target)
                continue;

            if (closest == null)//make sure we at least get something TODO find a better solution
                closest = depot;

            if (current_dist < distance && current_dist >= min_squared && current_dist <= max_squared)//if not within range and not closer than last, skip
            {
                closest = depot;
                distance = current_dist;
            }
        }

        if (closest == null)
            return null;

        closest.job_value = 100;//TODO calculate based on distance
        closest.penalty_value = (int)(closest.job_value * 0.5f);

        return closest;
    }


    private void CalculateDistanceSquares()
    {
        min_squared = min_distance_from_last * min_distance_from_last;
        max_squared = max_distance_from_last * max_distance_from_last;
    }


    private void ObjectiveComplete(string _win_reason = "")
    {
        if (current_depot_target != null)
        {
            current_depot_target.delivery_area.enabled = false;
            GameManager.scene.money_panel.LogTransaction(current_depot_target.job_value, _win_reason);
        }

        if (last_depot_target != null)
            last_depot_target.delivery_area.enabled = false;//just in case
           
        SetNewObjective();
    }


    private void ObjectiveFailed(string _fail_reason = "")
    {
        if (current_depot_target != null)
        {
            current_depot_target.delivery_area.enabled = false;
            GameManager.scene.money_panel.LogTransaction(current_depot_target.penalty_value, _fail_reason);
        }

        if (last_depot_target != null)
            last_depot_target.delivery_area.enabled = false;//just in case

        SetNewObjective();         
    }


    public void DetachedTrailer()
    {
        GameManager.scene.distance_indicator.SetTrailerGraphic(false);//should this be set from truck instead?

        if (current_depot_target == null)
            return;

        if (current_depot_target.trailer_delivered)//if trailer in depot
        {
            ObjectiveComplete("Completed Delivery");//job done
            return;
        }

        current_waypoint = current_trailer.transform;//trailer lost, set as destination
        GameManager.scene.distance_indicator.SetRoute(last_waypoint, current_waypoint);//update indicator
        objective_state = ObjectiveState.RETRIEVING_LOST_TRAILER;
    }


    public void AttachedTrailer()
    {
        GameManager.scene.distance_indicator.SetTrailerGraphic(true);

        if (current_depot_target == null)
            return;

        if (objective_state == ObjectiveState.RETRIEVING_LOST_TRAILER)//if retrieved lost trailer
        {
            objective_state = ObjectiveState.DELIVERING_TRAILER;
            current_waypoint = current_depot_target.transform;//set depot back as target
            GameManager.scene.distance_indicator.SetRoute(last_waypoint, current_waypoint);
            return;
        }

        if (objective_state == ObjectiveState.INITIAL_TRAILER_PICK_UP)
        {
            objective_state = ObjectiveState.DELIVERING_TRAILER;
            SetNewObjective();//find new depot delivery point
        }
    }

}
