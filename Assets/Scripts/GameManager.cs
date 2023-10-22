using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{

    public MicrogameJamController controller;

    public int difficulty = 1;
    // private int difficulty = controller.GetDifficulty();

    public GameObject Human;
    public GameObject Bot;

    [Tooltip("First integer is the difficulty (1, 2, 3) and the second is the number of humans")]
    public Dictionary<int, int> humansPerDifficulty = new Dictionary<int, int> {};
    private int numHumans = 0;

    // Start is called before the first frame update
    void Start()
    {
        numHumans = humansPerDifficulty[difficulty];
        SpawnCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCharacters()
    {
        for (int i=0; i <= numHumans; i++)
        {
            Instantiate(Human, getRandomPosition(), Quaternion.identity);
        }

        Instantiate(Bot, getRandomPosition(), Quaternion.identity);
    }

    Vector3 getRandomPosition()
    {
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        return new Vector3(Random.Range(-bounds.x, bounds.x), Random.Range(-bounds.y, bounds.y), 0);
    }
}
