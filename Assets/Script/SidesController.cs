using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SideEnum
{
    Up,
    Down,
    Left,
    Right
}

public class SidesController : MonoBehaviour
{
    [SerializeField] private SideEnum _side;

    [HideInInspector] public RaycastHit2D hit;

    private const float DISTANCE = 0.3f;

    public SideEnum Side
    {
        get { return _side; }
    }

    private RaycastHit2D GetLastHit(Vector3 direction)
    {
        var hits = Physics2D.RaycastAll(transform.position, direction, DISTANCE);
        if (hits.Length != 0)
            return hits[hits.Length - 1];
        else
            return new RaycastHit2D();
    }

    void FixedUpdate()
    {
        switch (_side)
        {
            case SideEnum.Up:
                hit = GetLastHit(transform.up);
                break;
            case SideEnum.Down:
                hit = GetLastHit(-transform.up);
                break;
            case SideEnum.Left:
                hit = GetLastHit(-transform.right);
                break;
            case SideEnum.Right:
                hit = GetLastHit(transform.right);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position + transform.position * 0f, transform.position);
        switch (_side)
        {
            case SideEnum.Up:
                Gizmos.DrawRay(transform.position, transform.up * DISTANCE);
                break;
            case SideEnum.Down:
                Gizmos.DrawRay(transform.position, -transform.up * DISTANCE);
                break;
            case SideEnum.Left:
                Gizmos.DrawRay(transform.position, -transform.right * DISTANCE);
                break;
            case SideEnum.Right:
                Gizmos.DrawRay(transform.position, transform.right * DISTANCE);
                break;
        }
    }
}
