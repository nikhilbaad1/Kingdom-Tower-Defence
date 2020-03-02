using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    //parameters of tower
    public Waypoint baseWaypoint; /// <summary>
    /// what the tower standing on
    /// </summary>
    [SerializeField] Transform objectToPan;
    int attackRange=30;
    [SerializeField] ParticleSystem bullets;

    //state of tower
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (target)
        {
            Vector3 temp = target.position;
            temp.y = target.position.y + 10;
            objectToPan.LookAt(temp);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }

    }

    private void SetTargetEnemy()
    {
         var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) return;

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);

        }

        target = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if (distToA < distToB)
        {
            return transformA;
        }

        return transformB;
    }

    private void FireAtEnemy()
    {
        float diatanceToEnemy = Vector3.Distance(target.transform.position, gameObject.transform.position);
        if (diatanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = gameObject.GetComponentInChildren<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
    }
}
