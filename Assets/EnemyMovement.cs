using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementPeriod=0.5f;
    [SerializeField] ParticleSystem goalParticle;
    // Start is called before the first frame update
    void Start()
    {
        
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }
    
    private IEnumerator FollowPath(List<Waypoint> Path)
    {
        foreach(Waypoint waypoint in Path)
        {
            print(waypoint.name);
            this.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        SelfDestruct();
        yield return new WaitForSeconds(1f); ;
    }
   
    void SelfDestruct()
    {
        Vector3 temp = transform.position;
        temp.y += 10;
        var vfx = Instantiate(goalParticle, temp, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
