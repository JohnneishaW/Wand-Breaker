using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveCounter : MonoBehaviour
{
    public GameObject player;
    public GameObject spawningManager;
    private SpawningManager spawningManagerScript;
    private int waveCount;
    private Text countText;
    private bool gameOver;
    private int maxWaveCount;
    private int enemiesLeft;
    private bool gameWon = false;

    // Start is called before the first frame update
    void Start()
    {
        countText = GetComponent<Text>();
        gameOver = false;
        gameWon = false;
        spawningManagerScript = spawningManager.GetComponent<SpawningManager>();
        maxWaveCount = spawningManagerScript.waveSizes.Length-1;
    }

    // Update is called once per frame
    void Update()
    {
        waveCount = spawningManagerScript.currentWave;
        countText.text = "Wave " + waveCount + " / " + maxWaveCount;

        if(spawningManagerScript.gameWon) 
        {
            if(!gameWon) 
            {
                Time.timeScale = 0;
                SceneManager.LoadScene("GameWon", LoadSceneMode.Additive);
                Debug.Log("Load GWS");
                gameWon = true;
            }
            if (Input.GetKeyDown(KeyCode.R) && gameWon) 
            {
                Time.timeScale = 1;
                SceneManager.UnloadSceneAsync("GameWon");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if(player.GetComponent<PlayerControl>().health == 0f) 
        {
            if (!gameOver) 
            {
                Time.timeScale = 0;
                SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
                Debug.Log("Load GOS");
                gameOver = true;
            }

            if (Input.GetKeyDown(KeyCode.R) && gameOver) 
            {
                Time.timeScale = 1;
                SceneManager.UnloadSceneAsync("GameOver");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
