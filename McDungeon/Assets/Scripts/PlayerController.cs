using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private GameObject target;
        private Vector3 movementDirection;
        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                GameObject[] targets = GameObject.FindGameObjectsWithTag("MobHitbox");
                if (targets.Length > 0)
                {
                    target = targets[0];
                    target.GetComponent<IMobController>().TakeDamage(1, EffectTypes.Freeze);
                }
                // GameObject[] spawner = GameObject.FindGameObjectsWithTag("MobSpawner");
                // if (spawner.Length > 0)
                // {
                //     spawner[0].GetComponent<MobManager>().SpawnMobs((MobTypes)Random.Range(0, 5));
                // }
            }

            movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            gameObject.transform.Translate(movementDirection * Time.deltaTime * speed);
        }
    }
}