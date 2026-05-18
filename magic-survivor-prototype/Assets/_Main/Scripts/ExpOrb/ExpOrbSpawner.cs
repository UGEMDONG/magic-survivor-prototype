using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExpOrbSpawner : MonoBehaviour
{
    [SerializeField] private int poolLimit = 200;
    [SerializeField] private GameObject expOrbPrefab;
    private ExpOrb expOrb;
    private Queue<GameObject> orbPool = new Queue<GameObject>();
    private float spawnCooldown = 1f;
    private float currentCooldown = 0f;
    
    public GameObject SpawnExpOrb(Vector3 position, int expLevel)
    {
        if (orbPool.Count > 0)
        {
            GameObject orb = orbPool.Dequeue();
            orb.transform.position = position;
            orb.GetComponent<ExpOrb>().SetExpLevel(expLevel);
            orb.SetActive(true);
            Debug.Log("Spawned ExpOrb at " + position + " with expLevel " + expLevel);
            return orb;
        }
        else
        {
            return null;
        }
    }
    public void ReturnToPool(GameObject orb)
    {
        orb.SetActive(false);
        orbPool.Enqueue(orb);
    }
    public void Start()
    {
        for (int i = 0; i < poolLimit; i++)
        {
            GameObject orb = Instantiate(expOrbPrefab);
            orb.SetActive(false);
            orbPool.Enqueue(orb);
        }
        expOrb = expOrbPrefab.GetComponent<ExpOrb>();
    }
    public void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown > spawnCooldown)
        {
            currentCooldown = 0f;
            Vector2 spawnPosition = new Vector2(Random.Range(-30f, 30f), Random.Range(-30f, 30f));
            SpawnExpOrb(spawnPosition, Random.Range(1, 4));
        }
    }
}
