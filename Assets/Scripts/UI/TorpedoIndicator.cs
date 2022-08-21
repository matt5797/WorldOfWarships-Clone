using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOW.Controller;

public class TorpedoIndicator : MonoBehaviour
{
    ShipController controller;
    public Image indicator;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller != null)
        {
            target = controller.TargetPoint;
        }
        target.y = 0;

        //indicator.transform.rotation = Quaternion.LookRotation(target - indicator.transform.position);
        Vector3 dir = (target - transform.position).normalized;
        Vector2 dir2 = V3toV2(target) - V3toV2(transform.position);
        //indicator.transform.up = dir;
        //indicator.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        Debug.DrawRay(transform.position, target - transform.position, Color.blue);
        Debug.DrawRay(indicator.transform.position, indicator.transform.up, Color.red);

        //Vector2 posWorld = V3toV2(indicator.transform.position);
        //indicator.transform.LookAt(dir2);

        //indicator.transform.rotation = LookAt2D(V3toV2(transform.position), V3toV2(target), facingDirection);

        float angle = Vector2.SignedAngle(V3toV2(transform.forward), dir2) * -1 - 180;
        angle = Mathf.Repeat(angle, 360);
        indicator.transform.localRotation = Quaternion.Euler(0, 0, angle);
        //print(Vector2.SignedAngle(V3toV2(transform.forward), dir2));
    }

    Vector2 V3toV2(Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }

    public enum FacingDirection
    {
        UP = 270,
        DOWN = 90,
        LEFT = 180,
        RIGHT = 0
    }
    public FacingDirection facingDirection;
    public Vector3 vector3;

    public Quaternion LookAt2D(Vector2 startingPosition, Vector2 targetPosition, FacingDirection facing)
    {
        Vector2 direction = targetPosition - startingPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= (float)facing;
        return Quaternion.AngleAxis(angle, vector3);
    }
}
