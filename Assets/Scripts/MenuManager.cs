using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    
    public void SpawnNewBlock(GameObject block)
    {
        Instantiate(block, cameraMovement.pivotPoint.position, Quaternion.identity, cameraMovement.pivotPoint);
    }
}
