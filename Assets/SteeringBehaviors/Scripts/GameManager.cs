using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject seeker;
    public GameObject target;

    public GameObject obstaclePrefab;
    public GameObject flockPrefab;

    public int obstacleCount = 20;
    public int flockCount = 20;

	void Start ()
    {
        PlaceTarget();

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-40, 40), 4f, Random.Range(-40, 40));
            Quaternion rot = Quaternion.Euler(0, Random.Range(0, 90), 0);
            Instantiate(obstaclePrefab, pos, rot);
        }

	    for (int i = 0; i < flockCount; i++)
	    {
	        Vector3 pos = new Vector3(
                50 * Mathf.Cos(Random.Range(0, 2*Mathf.PI)),
                4f,
                50 * Mathf.Sin(Random.Range(0, 2*Mathf.PI))
                );

	        Instantiate(flockPrefab, pos, Quaternion.identity);
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (seeker == null || target == null)
	        return;

	    if (Vector3.Distance(seeker.transform.position, target.transform.position) < 5)
	    {
	        PlaceTarget();
	    }
	}

    private void PlaceTarget()
    {
        target.transform.position = new Vector3(Random.Range(-30, 30), 4f, Random.Range(-30, 30));
    }
}
