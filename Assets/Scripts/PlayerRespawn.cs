using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void Update()
    {
        // Check if player y position is less than 10 then teleport him to respawn point
        if (transform.position.y < -10f)
        {
            transform.position = respawnPoint.position;
        }
    }
}
