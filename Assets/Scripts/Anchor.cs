using UnityEngine;

public class Anchor : MonoBehaviour
{
    public enum Axis {X, Y, Z};
    public Axis anchorAxis;
    float blockRadius;
    private void Start()
    {
        switch (anchorAxis)
        {
            case Axis.X:
                blockRadius = transform.localScale.x/2;
                break;
            case Axis.Y:
                blockRadius = transform.localScale.y/2;
                break;
            case Axis.Z:
                blockRadius = transform.localScale.z/2;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BuildingBlock>())
        {
            Anchor[] anchors = other.GetComponentsInChildren<Anchor>(true);
            float distanceToClosestAnchor = float.MaxValue;
            Vector3 closestAnchorPosition = Vector3.zero;
            foreach(Anchor anchor in anchors)
            {
                Vector3 anchorPosition = anchor.transform.position;
                float distance = Vector3.Distance(transform.position, anchorPosition);
                if (distance < distanceToClosestAnchor)
                {
                    distanceToClosestAnchor = distance;
                    closestAnchorPosition = anchorPosition;
                }
            }
            GetComponentInParent<BuildingBlock>().DropBlock();
            transform.parent.SetPositionAndRotation(closestAnchorPosition - transform.up * blockRadius, transform.localRotation);
        }
    }
}
