using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject gameOverMenu;
    public float speed = 6;
    public GameObject colum;
    public GameObject bar;
    public GameObject enemy2;
    public Renderer background;
    public bool gameOver = false;
    public bool start = false;
    public int move;
    public int mode;
    public Button easyMode;
    public Button hardMode;
    public Button quit;
    public Text timerText;
    private float startTime;
    public bool whenStart = false;

    public List<GameObject> cls;
    public List<GameObject> obs_bar;
    public List<GameObject> obs_en;
    // Start is called before the first frame update
    void Start()
    {
        //Frontground
        for (int i=0; i<21; i++)
        {
            cls.Add(Instantiate(colum, new Vector2(-10 + i, -3), Quaternion.identity));
        }

        //Obstacles
        obs_bar.Add(Instantiate(bar, new Vector2(10, -2), Quaternion.identity));
        obs_en.Add(Instantiate(enemy2, new Vector2(15, -2), Quaternion.identity));
        obs_bar.Add(Instantiate(bar, new Vector2(20, -1), Quaternion.identity));
        obs_en.Add(Instantiate(enemy2, new Vector2(25, -2), Quaternion.identity));

        //Button
        Button btn = easyMode.GetComponent<Button>();
        btn.onClick.AddListener(easy);
        Button btn2 = hardMode.GetComponent<Button>();
        btn2.onClick.AddListener(hard);
        Button btn3 = quit.GetComponent<Button>();
        btn3.onClick.AddListener(salir);

        //Timer
        //startTime = Time.time;
    }

    public void salir()
    {
        Application.Quit();
        Debug.Log("Ha salido");
    }

    void easy()
    {
        mode = 1;
        startTime = Time.time;
    }

    void hard()
    {
        mode = 2;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false)
        {
            if (mode == 1)
            {
                move = -1;
                start = true;
            }

            if (mode == 2)
            {
                move = -2;
                start = true;
            }
        }

        if (start == true && gameOver == true)
        {
            gameOverMenu.SetActive(true);

            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (start == true && gameOver == false)
        {
            startMenu.SetActive(false);
            background.material.mainTextureOffset = background.material.mainTextureOffset + new Vector2(0.02f, 0) * Time.deltaTime;
            

            //Timer
         
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f0");
            timerText.text = minutes + ":" + seconds;
            

            //Move columns
            for (int i = 0; i < cls.Count; i++)
            {
                if (cls[i].transform.position.x <= -10)
                {
                    cls[i].transform.position = new Vector3(10, -3, 0);
                }
                cls[i].transform.position = cls[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * speed;
            }

            //Move bar
            for (int i = 0; i < obs_bar.Count; i++)
            {
                if (obs_bar[i].transform.position.x <= -10)
                {
                    float randomX = Random.Range(10f, 20f);
                    float randomY = Random.Range(-1f, -2f);
                    obs_bar[i].transform.position = new Vector3(randomX, randomY, 0);
                }
                obs_bar[i].transform.position = obs_bar[i].transform.position + new Vector3(move, 0, 0) * speed * Time.deltaTime;
            }

            //Mover enemy
            for (int i = 0; i < obs_en.Count; i++)
            {
                if (obs_en[i].transform.position.x <= -10)
                {
                    float randomOb = Random.Range(10f, 20f);
                    obs_en[i].transform.position = new Vector3(randomOb, -2, 0);
                }
                obs_en[i].transform.position = obs_en[i].transform.position + new Vector3(move, 0, 0) * speed * Time.deltaTime;
            }

        }
    }
}
