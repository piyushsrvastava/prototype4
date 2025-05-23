using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rd;
    public GameObject focalpoint;
    public bool powerUp = false;
    private float powerstrength = 20f;
    public GameObject powerIndicator;
    public bool gameOver = false;
    private ScoreManager score;
    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log(gameOver);
        rd = GetComponent<Rigidbody>();
        focalpoint = GameObject.Find("focalPoint");
        score = FindObjectOfType<ScoreManager>();
        powerIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float fwd = Input.GetAxis("Vertical");
        rd.AddForce(focalpoint.transform.forward * fwd * speed * Time.deltaTime);
        powerIndicator.transform.position = transform.position + new Vector3(0, -0.25f, 0);
        GameOver();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            powerUp = true;
            powerIndicator.SetActive(true);
            score.power();
            Destroy(other.gameObject);

            StartCoroutine(powerUpCountDown());
        }
    }
     IEnumerator powerUpCountDown()
    {
        yield return new WaitForSeconds(7);
        powerUp = false;
        powerIndicator.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && powerUp)
        {
            Rigidbody enemyrigi = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 AwayEnemy = collision.transform.position - transform.position;

            Debug.Log("Collide with" + gameObject.name + "with power set up"+ powerUp);
            enemyrigi.AddForce(AwayEnemy * powerstrength , ForceMode.Impulse);
            score.highPeak();
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            score.highPeak();
        }
    }
   public void GameOver()
    {
        if (transform.position.y < -10f && gameObject.CompareTag("Player"))
        {
            //  Destroy(gameObject);
            gameOver = true;
            score.GameOver();
         //   Debug.Log("Game Over" + gameOver);
        }
    }
}
