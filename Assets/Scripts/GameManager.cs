using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : UnitySingleton<GameManager>
{

    public MicrogameJamController controller;

    // public int difficulty = 1;
    [HideInInspector] public int difficulty = 1;

    public AudioSource Music;
    public AudioSource Crowd;
    public GameObject Human;
    public GameObject Bot;
    public GameObject Characters;

    [Tooltip("ONLY 3 NUMBERS! Add number of humans by increasing difficulty.")]
    public List<int> humansPerDifficulty;
    public Bot imposter;
    private int numHumans = 0;
    private bool win = false;
    [HideInInspector] public bool gameOver = false;


    [HideInInspector] public Character deadChar;
    public TMPro.TMP_Text startText;

    // Start is called before the first frame update
    void Start()
    {
        if (controller)
        {
            difficulty = controller.GetDifficulty();
        }
        // delete this
        difficulty = 1;
        if (difficulty >= 3) {
            startText.text = "Find the Imposter!";
        }

        numHumans = humansPerDifficulty[difficulty-1];
        SpawnCharacters();
        
        //Music.Play();
        //Crowd.Play();
        AudioManager.Instance.PlaySound("Music");
        AudioManager.Instance.PlaySound("Crowd");
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
        GameObject character;
        for (int i=0; i <= numHumans; i++)
        {
            character = Instantiate(Human, getRandomPosition(), Quaternion.identity);
            character.transform.parent = Characters.transform;
        }
        character = Instantiate(Bot, getRandomPosition(), Quaternion.identity);
        imposter = character.GetComponent<Bot>();
        character.transform.parent = Characters.transform;
    }

    public Vector3 getRandomPosition()
    {
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        return new Vector3(Random.Range(-bounds.x, bounds.x), Random.Range(-bounds.y, bounds.y), 0);
    }
    
    void PauseCharacters()
    {
        foreach (Transform child in Characters.transform)
        {
            Character chr = child.GetComponent<Character>();
            // Debug.Log(child.transform.position.x < deadChar.transform.position.x);
            chr.Pause();
            if (chr != deadChar)
                chr.CheckFlip(child.transform.position.x < deadChar.transform.position.x);
        }
    }

    public void Win()
    {
        Debug.Log("Holy awesome");
        gameOver = true;
        StartCoroutine(WinRoutine());
    }

    public void Lose()
    {
        Debug.Log("Damn you suck");
        gameOver = true;
        StartCoroutine(LoseRoutine());
    }

    IEnumerator WinRoutine()
    {
        win = true;
        PauseCharacters();
        Music.Stop();
        Crowd.Stop();
        // Play Win Animation
        yield return new WaitForSeconds(2f);

        controller.WinGame();
    }

    IEnumerator LoseRoutine()
    {
        // Play Lose Animation
        PauseCharacters();
        StartCoroutine(imposter.Vent(false));
        Music.Stop();
        Crowd.Stop();
        yield return new WaitForSeconds(2f);

        controller.LoseGame();
    }
}
