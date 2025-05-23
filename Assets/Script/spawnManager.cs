using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnX = 9f;
    public float spawnZ = 9f;
    public int enemyCount;
    public int spawnenemy = 1;
    public GameObject Powerup;
    private PlayerController pc;

    void Start()
    {
        pc = FindObjectOfType<PlayerController>(); // Get the PlayerController instance
        leveup(spawnenemy);
        Instantiate(Powerup, GenerateSpawn(), Powerup.transform.rotation);
    }

    void leveup(int itemspawn)
    {
        for (int i = 0; i < itemspawn; i++)
        {
            Instantiate(enemy, GenerateSpawn(), enemy.transform.rotation);
        }
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enmey>().Length;

        // Check if the game is not over
        if (pc != null && !pc.gameOver)
        {
            if (enemyCount == 0)
            {
                spawnenemy++;
                Instantiate(Powerup, GenerateSpawn(), Powerup.transform.rotation);
                leveup(spawnenemy);
            }
        }
    }

    private Vector3 GenerateSpawn()
    {
        float SpawnX = Random.Range(-spawnX, spawnX);
        float SpawnZ = Random.Range(-spawnZ, spawnZ);
        Vector3 pso = new Vector3(SpawnX, 0, SpawnZ);
        return pso;
    }
}