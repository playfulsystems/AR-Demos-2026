using UnityEngine;

public class FaceballSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    float spawnFreq = 2f;
    float spawnCountdown;

    void Start()
    {
        spawnCountdown = spawnFreq;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCountdown < 0)
        {
            // create balls 
            GameObject newBall = Instantiate(ballPrefab);
            newBall.transform.position = transform.position;
            spawnCountdown = spawnFreq;

            // set velocity to the direction the camera is facing 
            Rigidbody rb = newBall.GetComponent<Rigidbody>();
            rb.linearVelocity = transform.forward * 0.5f;
        }

        spawnCountdown -= Time.deltaTime;
    }
}
