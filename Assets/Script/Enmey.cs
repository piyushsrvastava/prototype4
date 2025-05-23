using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enmey : MonoBehaviour
{
    private Rigidbody enrg;
    private GameObject player;
    public float speed = 3;
    private ScoreManager sm;

    // Start is called before the first frame update
    void Start()
    {
        enrg = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        sm = FindObjectOfType<ScoreManager>(); // Get the ScoreManager instance
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        enrg.AddForce(direction * speed);

        // Check if the enemy has fallen below the threshold
        if (transform.position.y <= -10f && sm != null)
        {
          //S  sm.Score(10);
            Destroy(gameObject); // Destroy the enemy
        }
    }

    private void OnDestroy()
    {
        // Update the score when the enemy is destroyed
        if (sm != null)
        {
            sm.Score(10); // Add 10 points to the score
        }
    }
}