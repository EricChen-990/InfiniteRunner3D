using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameContorller : MonoBehaviour{

    enum GameState{
        MenuScene,
        MainScene,
        // PauseScene,
        // EndScene,
        // QuitScene,
    }

    enum MenuSceneState{
        Begin,
        UIDisplay,
        WiatForPressed
    }

    enum MainSceneState{
        ConstructEnv,
        GetPlayer,
        Wait
    }

    public GameObject []envs;
    public GameObject player;

    private GameObject []enable;
    private int envPoolAmount = 20;
    private Vector3 envInterval = new Vector3(60f, 0f, 0f);
    private Vector3 startPos = new Vector3(0f, 0f, 0f);
    private List<GameObject> envPool = new List<GameObject>();
    private GameState gameState;
    private MenuSceneState menuSceneState;
    private MainSceneState mainSceneState;
    private Canvas canvas;
    private RectTransform menuPanel;
    private Button playBtn;
    private TMP_Text countdownText;
    private bool isPlayPassed = false;
    private bool constructionDone = false;


    private void Awake() {
        instantiate();
    }

    private void instantiate(){
        gameState = GameState.MenuScene;
        menuSceneState = MenuSceneState.Begin;
        mainSceneState = MainSceneState.ConstructEnv;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        menuPanel = canvas.transform.Find("MenuPanel").GetComponent<RectTransform>();
        playBtn = menuPanel.transform.Find("Play").GetComponent<Button>();
        countdownText = menuPanel.transform.Find("Countdown").GetComponent<TextMeshProUGUI>();
        countdownText.gameObject.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start(){
        for(int i = 0 ; i < envPoolAmount ; i++){
            GameObject gobj = Instantiate(envs[UnityEngine.Random.Range(0, envs.Length)]);
            gobj.transform.position = new Vector3(0f, 0f, 0f);
            gobj.SetActive(false);
            envPool.Add(gobj);
        }
    }

    private void startPlayer(){
        float x = UnityEngine.Random.Range(-8f, 8f);
        float z = UnityEngine.Random.Range(-8f, 8f);
        Instantiate(player, new Vector3(x, 2f, z), Quaternion.identity);
    }

    private void ConstructEnv(){
        
        for (int i = 0 ; i < 10 ; i++){
            int r = UnityEngine.Random.Range(0, envPool.Count);
            GameObject gobj = envPool[r];
            envPool.RemoveAt(r);
            gobj.transform.position = startPos;
            gobj.SetActive(true);
            startPos += envInterval;
        }
        constructionDone = true;
    }

    // Update is called once per frame
    void Update(){
        switch(gameState){
            case GameState.MenuScene:
                switch(menuSceneState){
                    case MenuSceneState.Begin:
                        menuSceneState = MenuSceneState.UIDisplay;
                        break;
                    case MenuSceneState.UIDisplay:
                        MenuPanelUiDisplay();
                        menuSceneState = MenuSceneState.WiatForPressed;
                        break;
                    case MenuSceneState.WiatForPressed:
                        if(isPlayPassed){
                            gameState = GameState.MainScene;
                            closeMenuPanelUiDisplay();
                        }
                        break;
                }
                break;
            case GameState.MainScene:
                switch(mainSceneState){
                    case MainSceneState.ConstructEnv:
                        ConstructEnv();
                        while(!constructionDone);
                        mainSceneState = MainSceneState.GetPlayer;
                        break;
                    case MainSceneState.GetPlayer:
                        startPlayer();
                        mainSceneState = MainSceneState.Wait;
                        break;
                    case MainSceneState.Wait:
                        Debug.Log("123");
                        break;
                }
                break;
        }
    }

    private IEnumerator CountDownToPlay(){
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        isPlayPassed = true;
    }

    private void closeMenuPanelUiDisplay(){
        menuPanel.gameObject.SetActive(false);
    }

    private void MenuPanelUiDisplay(){
        menuPanel.gameObject.SetActive(true);
    }

    private void FixedUpdate() {
        float x = player.transform.position.x;
    }

    public void Recycle(GameObject gb){
        gb.SetActive(false);
        envPool.Add(gb);
    }

    public void Retrieve(){
        int j = UnityEngine.Random.Range(0, envPool.Count);
        GameObject gb = envPool[j];
        envPool.RemoveAt(j);
        gb.transform.position = startPos;
        gb.SetActive(true);
        startPos += envInterval;
        //return gb;
    }

    public void PlayPressed(){
        StartCoroutine(CountDownToPlay());
    }
}
