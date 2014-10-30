using UnityEngine;
using System.Collections;

public class MovingObstacleTrigger : MonoBehaviour
{
    public MovingObstacleObject parent;

    public void OnTriggerEnter(Collider other)
    {
        parent.OnTriggerEnter(other);
        Debug.Log("gameObject.tag is " + gameObject.tag);
    }
}
