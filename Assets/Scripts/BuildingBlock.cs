using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material selectedMaterial;
    public Material staticMaterial;
    public Anchor[] anchors;

    public void PickUpBlock()
    {
        meshRenderer.material = selectedMaterial;
        foreach(Anchor anchor in anchors)
        {
            anchor.gameObject.SetActive(true);
        }
    }
    public void DropBlock()
    {
        meshRenderer.material = staticMaterial;
        transform.parent = null;
        foreach(Anchor anchor in anchors)
        {
            anchor.gameObject.SetActive(false);
        }
    }
}
