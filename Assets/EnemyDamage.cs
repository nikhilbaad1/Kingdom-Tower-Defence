using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{


    [SerializeField] int Hitpoints = 10;
    [SerializeField] Collider collisionMesh;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        print("I m hit");
        ProcessHit();
        if (Hitpoints <= 0)
        {
            Vector3 temp = transform.position;
            temp.y += 10;
            var vfx =Instantiate(deathParticlePrefab, temp, Quaternion.identity);
            vfx.Play();
            Destroy(vfx.gameObject, vfx.main.duration);
            Destroy(gameObject);
        }
    }

    private void ProcessHit()
    {
        hitParticlePrefab.Play();
        Hitpoints--;
        print("Current hitpoints "+Hitpoints);
    }
}
