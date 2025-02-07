using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnCounter;
    [SerializeField] private float spawnRadius;
    [SerializeField] private int spawnableNum;

    private void Update()
    {
        if (spawnCounter > spawnRate)
        {
            Spawn();
            spawnCounter = 0;       
        }
        else
        {
            spawnCounter += Time.deltaTime;
        }
    }

    private void Spawn()
    {
        Vector2 rndSpawn = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPoint = new Vector3(rndSpawn.x, rndSpawn.y, 0) + this.transform.position;
        //Task spawning = Task.Run(() => SeekerManager.Instance.AddSeeker(spawnPoint));
        Profiler.BeginSample("spawning");
        SeekerManager.Instance.AddSeeker(spawnPoint,spawnableNum);
        Profiler.EndSample();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, spawnRadius);
    }
}
