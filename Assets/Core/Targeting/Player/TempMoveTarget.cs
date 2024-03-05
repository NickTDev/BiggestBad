using UnityEngine;

public class TempMoveTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Enemy")
        {
            Vector3 newPos = new Vector3(other.transform.position.x, other.transform.position.y + other.GetComponent<BoxCollider>().center.y, other.transform.position.z);
            transform.position = newPos + (Vector3.up * ((other.GetComponent<BoxCollider>().size.y / 2) + 0.05f));
        }
    }
}
