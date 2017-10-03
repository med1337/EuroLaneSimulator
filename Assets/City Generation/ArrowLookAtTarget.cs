using UnityEngine;

public class ArrowLookAtTarget : MonoBehaviour
{
    private GameObject target;

	// Update is called once per frame
	void Update ()
    {
        // If we have a valid target, uppdate
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
	}


    public void DisableArrow()
    {
        this.gameObject.SetActive(false);
    }


    public void EnableArrow()
    {
        this.gameObject.SetActive(true);
    }


    public void SetTarget(GameObject new_target)
    {
        target = new_target;
    }
}
