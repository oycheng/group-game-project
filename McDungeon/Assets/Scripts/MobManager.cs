using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs{
    public class MobManager : MonoBehaviour
    {
        [SerializeField]
        private int mobCount = 4;
        [SerializeField]
        private GameObject slimePrefab;
        [SerializeField]
        private GameObject skeletonPrefab;
        [SerializeField]
        private GameObject gNomePrefab;
        [SerializeField]
        private GameObject magePrefab;
        private List<GameObject> mobsList = new List<GameObject>();
        private GameObject player;

        void Start()
        {
            GameObject[] playerObjects;
            playerObjects = GameObject.FindGameObjectsWithTag("PlayerHitbox");
            if (playerObjects.Length == 0)
            {
                Debug.Log("Player not found.");
            }
            else
            {
                this.player = playerObjects[0];
            }
        }

        public void SpawnMobs(MobTypes type)
        {
            var mobPrefab = this.getMobPrefab(type);

            for (int i = 0; i < this.mobCount; i++) {
                var newMob = (GameObject)Instantiate(mobPrefab, this.gameObject.transform);
                this.Subscribe(newMob);
                newMob.transform.position = new Vector3(Random.Range(-10,10), Random.Range(-10, 10), 0);
            }

            this.Notify();
        }

        private GameObject getMobPrefab(MobTypes type)
        {
            switch (type)
            {
                case MobTypes.Slime:
                    return this.slimePrefab;
                case MobTypes.Skeleton:
                    return this.skeletonPrefab;
                case MobTypes.GNome:
                    return this.gNomePrefab;
                case MobTypes.Mage:
                    return this.magePrefab;
                default:
                    Debug.Log("Invalid Mob Type. Returning Slime.");
                    return slimePrefab;
            }
        }

        public void Subscribe(GameObject mob)
        {
            mobsList.Add(mob);
        }

        public void Unsubscribe(GameObject mob)
        {
            mobsList.Remove(mob);
        }

        public void Notify()
        {
            foreach (GameObject mob in mobsList)
            {
                mob.GetComponent<IMobController>().GetPlayer(this.player);
            }
        }

        public List<GameObject> GetMobs()
        {
            return this.mobsList;
        }
    }
}