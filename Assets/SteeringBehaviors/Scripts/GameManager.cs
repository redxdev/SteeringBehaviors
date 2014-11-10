using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject seeker;
    public GameObject target;

    public GameObject obstaclePrefab;

	void Start ()
    {
        PlaceTarget();

        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-40, 40), 4f, Random.Range(-40, 40));
            Quaternion rot = Quaternion.Euler(0, Random.Range(0, 90), 0);
            GameObject o = (GameObject)Instantiate(obstaclePrefab, pos, rot);

            float scale = Random.Range(2f, 5f);
            o.transform.localScale = new Vector3(scale, scale, scale);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
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
