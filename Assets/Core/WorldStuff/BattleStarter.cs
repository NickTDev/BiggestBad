using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T02
{
    public class BattleStarter : MonoBehaviour
    {
        bool battleStarted;

        public GameObject encounter;
        public GameObject worldCharacter;
        public GameObject camManager;
        public GameObject barrier;

        private void Start()
        {
            battleStarted = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Player" && !battleStarted)
            {
                battleStarted = true;
                Debug.Log("Battle Start");

                camManager.GetComponent<CameraManager>().swapCameraType();
                encounter.SetActive(true);
                worldCharacter.SetActive(false);
                barrier.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}