using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace WorldStreaming
{
    public class WorldStreamingController : MonoBehaviour
    {
        public static WorldStreamingController instance;
        public WorldStreamingPlayerPosition playerPosition;
        public List<WorldStreamingSector> sectors = new List<WorldStreamingSector>();

        [Header("The distance when Sectors will be activated or deactivated.")]
        [Range(0, 500)]
        public float loadDistance = 200f;
        public void registerSector(WorldStreamingSector sector)
        {
            sectors.Add(sector);
        }

        public virtual void Awake()
        {
            instance = this;
        }

        Queue<WorldStreamingSector> activateQueue = new Queue<WorldStreamingSector>();
        Queue<WorldStreamingSector> deactivateQueue = new Queue<WorldStreamingSector>();
        void Update()
        {
            if(activateQueue.Count != 0)
            {
                activateQueue.Dequeue().gameObject.SetActive(true);
            }
            else if (deactivateQueue.Count != 0)
            {
                deactivateQueue.Dequeue().gameObject.SetActive(false);
            }


            Vector3 playerPos = playerPosition.getPosition();
            foreach(WorldStreamingSector sector in sectors)
            {
                if(Vector3.Distance(sector.transform.position, playerPos) < loadDistance)
                {
                    // activate
                    if(!sector.gameObject.activeSelf)
                    {
                        this.activateQueue.Enqueue(sector);
                    }
                    // sector.setActiveState(true);
                }
                else
                {
                    // deactivate
                    if (sector.gameObject.activeSelf)
                    {
                        this.deactivateQueue.Enqueue(sector);
                    }
                    //sector.setActiveState(false);
                }
            }
        }
    }
}