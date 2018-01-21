using UnityEngine;

namespace WorldStreaming
{
    public class WorldStreamingSector : MonoBehaviour
    {
        void Start()
        {
            if(WorldStreamingController.instance)
            {
                WorldStreamingController.instance.registerSector(this);
                //gameObject.SetActive(false);
            }
        }

        public void setActiveState(bool active)
        {
            this.gameObject.SetActive(active);
        }
    }
}