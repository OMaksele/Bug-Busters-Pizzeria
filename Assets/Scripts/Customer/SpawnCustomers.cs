using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomers : MonoBehaviour
{
    public List<GameObject> seats;
    public List<GameObject> availableSeats;

    public GameObject prefab;
    public Transform spawnPoint;

    public bool SpawnMore;

    public int Customers;

    void Awake()
    {

        seats = new List<GameObject>(GameObject.FindGameObjectsWithTag("Chair"));
        availableSeats = new List<GameObject>(GameObject.FindGameObjectsWithTag("Chair"));

    }

    async private void Update()
    {
        if (SpawnMore)
        {
            for (int x = 0; x < 1; x++)
            {
                Spawn();
            }
            await new WaitForSeconds(2);
        }
    }

    void Spawn()
    {
        GameObject customer = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        GameObject customerTarget = customer.GetComponent<Customers>().target = availableSeats[Random.Range(0, availableSeats.Count)];
        print(customerTarget);
        Customers++;
        availableSeats.Remove(customerTarget);
    }
}
