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

    [Tooltip("ONLY 3 NUMBERS! Add number of humans by increasing difficulty.")]
    public List<int> humansPerDifficulty;
    private int numHumans = 0;
    private bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        numHumans = humansPerDifficulty[difficulty-1];
        SpawnCharacters();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.GetTimer() < 0.1f)
        {
            if (win == true)
            {
                Debug.Log("Last minute win");
                controller.WinGame();
            }
        }
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

    public void Win()
    {
        Debug.Log("Holy awesome");
        win = true;
        StartCoroutine(WinRoutine());
    }

    public void Lose()
    {
        Debug.Log("Damn you suck");
        StartCoroutine(LoseRoutine());
    }

    IEnumerator WinRoutine()
    {
        // Play Win Animation
        yield return new WaitForSeconds(1f);

        controller.WinGame();
    }

    IEnumerator LoseRoutine()
    {
        // Play Lose Animation
        yield return new WaitForSeconds(1f);

        controller.LoseGame();
    }
}
