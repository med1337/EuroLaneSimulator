using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private ObjectiveState objective_state = ObjectiveState.INITIAL_TRAILER_PICK_UP;//player starts with a trailer
    [SerializeField] private float play_time = 5 * 60;
    [SerializeField] private Text time_display;
    [SerializeField] private Text current_cargo;

    private CountdownTimer timer = new CountdownTimer();

    private Depot[] depots;
    private Depot last_depot_target = null;//will be a depot script instead
    private Depot current_depot_target = null;

    private Transform last_waypoint = null;
    private Transform current_waypoint = null;//could be switched to trailer instead of depot
    private Trailer current_trailer = null;


    void Start ()
	{
	    depots = FindObjectsOfType<Depot>();
        timer.InitCountDownTimer(play_time);

	    if (GameManager.scene.player_truck == null)
	    {
            Debug.LogWarning("Player Truck not in scene! destroying objective manager!");
	        Destroy(this);
	        return;
	    }

        SetPlayerStart();
	}


    void Update()
    {
        if (time_display != null)
        {
            System.TimeSpan t = System.TimeSpan.FromSeconds(Mathf.Clamp(timer.current_time, 0, Mathf.Infinity));
            time_display.text = string.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
        }

        if (timer.UpdateTimer())
            Invoke("TriggerGameOver", 5);
    }


    private void TriggerGameOver()
    {
        GameManager.GameOver();
    }



    private void SetPlayerStart()
    {
        current_depot_target = PickNewDepot();//find next optimal depot 
        last_depot_target = current_depot_target;
        current_waypoint = current_depot_target.transform;
        last_waypoint = last_depot_target.transform;

        if (GameManager.scene.player_trailer != null)
            current_trailer = GameManager.scene.player_trailer;

        SetNewObjective();
        GameManager.scene.player_truck.transform.position = current_waypoint.transform.position + new Vector3(5,0);
    }


    public void SetNewObjective()
    {
        last_depot_target = current_depot_target;
        last_waypoint = last_depot_target.transform;
        current_depot_target = PickNewDepot();//find next optimal depot

        if (current_depot_target == null)
            return;

        if (GameManager.scene.player_truck.AttachedTrailer != null)
        {
            objective_state = ObjectiveState.DELIVERING_TRAILER;//we must deliver it
            current_waypoint = current_depot_target.transform;//set new depot as target
            current_depot_target.delivery_area.enabled = true;//allow it to check for delivery

            string cargo_message = GameManager.scene.chat_display.DisplayPickupMessage();

            if (current_cargo != null)
            {
                current_cargo.text = "Current Cargo:\n" + cargo_message;
            }
        }
        else
        {
            objective_state = ObjectiveState.INITIAL_TRAILER_PICK_UP;
            MoveTrailerToDepot();
            current_depot_target.delivery_area.enabled = false;//in case for some reason it's enabled
        }

        GameManager.scene.distance_indicator.SetRoute(current_waypoint, last_waypoint);
    }


    private void MoveTrailerToDepot()
    {
        if (current_trailer == null)
            return;

        current_trailer.ResetPosition(current_depot_target.transform);
        current_waypoint = current_trailer.transform;
    }


    public Depot PickNewDepot()
    {
        Depot closest = null;

        depots.Shuffle();

        foreach (Depot depot in depots)
        {
            if (depot == current_depot_target || depot == last_depot_target)
                continue;

            closest = depot;
            break;
        }

        if (closest == null)
            closest = last_depot_target;

        closest.job_value = (int)TransactionTypes.DELIVERY;//TODO calculate based on distance
        closest.penalty_value = (int)(closest.job_value * 0.5f);

        return closest;
    }


    private void ObjectiveComplete(string _win_reason = "")
    {
        if (current_depot_target != null)
        {
            current_depot_target.delivery_area.enabled = false;
            current_depot_target.trailer_delivered = false;
            GameManager.scene.money_panel.LogTransaction(current_depot_target.job_value, _win_reason);

            GameManager.scene.chat_display.DisplayDeliveryMessage();
        }

        if (last_depot_target != null)
        {
            last_depot_target.delivery_area.enabled = false; //just in case
            last_depot_target.trailer_delivered = false; //just in case
        }

        SetNewObjective();
    }


    private void ObjectiveFailed(string _fail_reason = "")
    {
        if (current_depot_target != null)
        {
            current_depot_target.delivery_area.enabled = false;
            current_depot_target.trailer_delivered = false;
            GameManager.scene.money_panel.LogTransaction(current_depot_target.penalty_value, _fail_reason);
        }

        if (last_depot_target != null)
        {
            last_depot_target.delivery_area.enabled = false; //just in case
            last_depot_target.trailer_delivered = false; //just in case
        }

        SetNewObjective();         
    }


    public void DetachedTrailer(bool _intentional)
    {
        GameManager.scene.distance_indicator.SetTrailerGraphic(false);//should this be set from truck instead?

        if (current_depot_target == null)
            return;

        if (current_depot_target.trailer_delivered && _intentional)//if trailer in depot
        {
            ObjectiveComplete("Completed Delivery");//job done
            return;
        }

        current_waypoint = current_trailer.transform;//trailer lost, set as destination
        GameManager.scene.distance_indicator.SetRoute(current_waypoint, last_waypoint);//update indicator
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
            GameManager.scene.distance_indicator.SetRoute(current_waypoint, last_waypoint);
            return;
        }

        if (objective_state == ObjectiveState.INITIAL_TRAILER_PICK_UP)
        {
            objective_state = ObjectiveState.DELIVERING_TRAILER;
            SetNewObjective();//find new depot delivery point
        }
    }


   

}


public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}