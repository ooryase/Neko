using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockfall : MonoBehaviour
{
    [SerializeField] private PlayerFollow myCamera = null;
    private GameObject rock = null;
    private List<GameObject> rocks = new List<GameObject>();
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rock = transform.GetChild(0).gameObject;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rocks.Count);
        if (rocks.Count != 0)
        {
            for(int i = 0; i < rocks.Count; i++)
            {
                if (rocks[i].transform.position.y < -10.0f)
                {
                    Destroy(rocks[i]);
                    rocks.RemoveAt(i);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        myCamera.Shake(0.3f);
        source.PlayOneShot(source.clip);
        for(int i = 0; i < 2; i++)
        {
            var r = Instantiate(rock, rock.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0, 5.0f), 0), Quaternion.Euler(Random.Range(0, 180), 90.0f, 270.0f));
            r.GetComponent<Rigidbody>().useGravity = true;
            r.GetComponent<AudioSource>().Play();
            rocks.Add(r);
        }
    }
}
