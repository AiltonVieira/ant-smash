using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioClip[] audioEnemies;

    [HideInInspector] public int totalScore, totalLifes, enemyCount, highscore;

    private Spawner spawner;

    private UIController uIController;

    public Transform allEnemiesParent;

    private Destroyer destroyer;

    [SerializeField] private AudioSource music;


    private void Awake() {
        uIController = FindObjectOfType<UIController>();
        spawner = FindObjectOfType<Spawner>();
        destroyer = FindAnyObjectByType<Destroyer>();
        highscore = GetScore();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        enemyCount = 0;
        totalLifes = 5;
        spawner.gameObject.GetComponent<Spawner>().enabled = false;
        music.volume = 0.5f;
    }

    public void BackToMainMenu() {
        music.volume = 0.5f;
        totalScore = 0;
        enemyCount = 0;
        uIController.txtScore.text = totalScore.ToString();
        destroyer.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        spawner.gameObject.GetComponent<Spawner>().enabled = false;
        for (int i = 0; i < uIController.imageLifes.Length; i++)
        {
            uIController.imageLifes[i].gameObject.SetActive(true);
        }
        foreach (Transform child in allEnemiesParent.transform){
            Destroy(child.gameObject);
        }
    }

    public void Restart() {
        totalScore = 0;
        enemyCount = 0;
        uIController.txtScore.text = totalScore.ToString();
        spawner.gameObject.GetComponent<Spawner>().enabled = true;
        destroyer.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        foreach (Transform child in allEnemiesParent.transform){
            Destroy(child.gameObject);
        }
    }

    public void StartGame() {
        totalScore = 0;
        enemyCount = 0;
        uIController.txtScore.text = totalScore.ToString();
        spawner.gameObject.GetComponent<Spawner>().enabled = true;
        music.volume = 0.25f;
    }

    public void SaveScore(){
        if(totalScore > highscore) {
            PlayerPrefs.SetInt("highscore", totalScore);
            uIController.txtHighscore.text = "Highscore: " + totalScore.ToString();
        }
    }

    public int GetScore(){
        highscore = PlayerPrefs.GetInt("highscore");
        return highscore;
    }

    public void DestroyEnemy(Collider2D target) {
        enemyCount++;
        if(enemyCount < 5) {
            uIController.imageLifes[enemyCount - 1].gameObject.SetActive(false);
        } else {
            uIController.imageLifes[enemyCount - 1].gameObject.SetActive(false);
            uIController.panelGameover.gameObject.SetActive(true);
            GameOver();
            SaveScore();
        }
        Destroy(target.gameObject);
    }

    public void GameOver() {
        spawner.gameObject.GetComponent<Spawner>().enabled = false;
        destroyer.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        uIController.txtFinalScore.text = "Score: " + totalScore.ToString();
        foreach (Transform child in allEnemiesParent.transform){
            Destroy(child.gameObject);
        }
    }
}
