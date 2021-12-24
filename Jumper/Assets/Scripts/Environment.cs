using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bonusPrefab;
    public GameObject active;
    // Start is called before the first frame update
    void Start()
    {
        //var currentPrefab = Instantiate(bonusOrEnemy());
        instantiatePrefab();
    }

    // Update is called once per frame
    void Update()
    {
        active = GameObject.Find("active");
        if (!active)
        {
            instantiatePrefab();
        }
    }

    public GameObject bonusOrEnemy()
    {
        int rng = Random.Range(1, 10);
        if (rng < 3)
        {
            return bonusPrefab;
        }
        else
        {
            return enemyPrefab;
        }
    }

    public void instantiatePrefab()
    {
        var currentPrefab = Instantiate(bonusOrEnemy());
        currentPrefab.name = ("active");
    }
}
