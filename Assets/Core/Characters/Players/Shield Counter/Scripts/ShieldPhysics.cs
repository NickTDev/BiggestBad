// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Handles collision of the shield object.
    /// </summary>
    public class ShieldPhysics : MonoBehaviour
    {
        private void OnTriggerEnter(Collider col)
        {
            //if (col.CompareTag("Projectile"))
            //{
            //    Destroy(col.gameObject);
            //}
        }
    }
}