using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    const int maxLevel = 30;
    [Range(1, maxLevel)]
    public int level = 1;
    public float ballVelocityMult = 0.02f;
    public GameObject bluePrefab;
    public GameObject redPrefab;
	public GameObject redXOPrefab;
    public GameObject greenPrefab;
    public GameObject yellowPrefab;
    public GameObject ballPrefab;
    public GameDataScript gameData;
    static bool gameStarted = false;
    AudioSource audioSrc;
    public AudioClip pointSound;
	public bool B3;
    public int b3tester;
	
    static Collider2D[] colliders = new Collider2D[50];
    static ContactFilter2D contactFilter = new ContactFilter2D();

    void CreateBlocks(GameObject prefab, float xMax, float yMax, int count, int maxCount)
    {
        if (count > maxCount)
            count = maxCount;
        for (int i = 0; i < count; i++)
            for (int k = 0; k < 20; k++)
            {
                var obj = Instantiate(prefab,
                new Vector3((UnityEngine.Random.value * 2 - 1) * xMax,
                UnityEngine.Random.value * yMax, 0),
                Quaternion.identity);
                if (obj.GetComponent<Collider2D>()
                .OverlapCollider(contactFilter.NoFilter(), colliders) == 0)
                    break;
                Destroy(obj);
            }
    }

    void SetBackground()
    {
        //level = 4;
        var bg = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        bg.sprite = Resources.Load(level.ToString("d2"),
        typeof(Sprite)) as Sprite;
    }

    public void BallDestroyed()
    {
        gameData.balls--;
        StartCoroutine(BallDestroyedCoroutine());
    }

    int requiredPointsToBall
        { get { return 400 + (level - 1) * 20; } }

    IEnumerator BlockDestroyedCoroutine2()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            audioSrc.PlayOneShot(pointSound, 5 * PlayerPrefs.GetFloat("SoundEffectsVolumeCoef"));
        }
    }

    public void BlockDestroyed(int points)
    {
        gameData.points += points;
        if (gameData.sound)
            audioSrc.PlayOneShot(pointSound, 5 * PlayerPrefs.GetFloat("SoundEffectsVolumeCoef"));
        gameData.pointsToBall += points;
        if (gameData.pointsToBall >= requiredPointsToBall)
        {
            gameData.balls++;
            gameData.pointsToBall -= requiredPointsToBall;
            if (gameData.sound)
                StartCoroutine(BlockDestroyedCoroutine2());
        }
        StartCoroutine(BlockDestroyedCoroutine());
    }
	
	public void BlockMarkChanged()
    {
        GameObject[] XO = GameObject.FindGameObjectsWithTag("XOBlock");
        bool del = true;
        for (int i = 0;i < XO.Length;i++)
        {
            var XOscr = XO[i].GetComponent<BlockXOScript>();
            if (XOscr.mark == 'O' ) 
            { 
                del = false;
            }
        }
        if (del)
        {
            for (int i = 0; i < XO.Length; i++)
            {
                var XOscr = XO[i].GetComponent<BlockXOScript>();
                XOscr.Deleter();
            }
        }

    }

    IEnumerator BallDestroyedCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
            if (gameData.balls > 0)
                CreateBalls();
            else
            {
                gameData.Reset();
                gameData.Save();
                SceneManager.LoadScene("MenuScene");
            }
    }

    IEnumerator BlockDestroyedCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        int blocks = GameObject.FindGameObjectsWithTag("Block").Length;
        int xoblocks = GameObject.FindGameObjectsWithTag("XOBlock").Length;
        if (blocks == 0 && xoblocks == 0)
        {
            if (level < maxLevel)
                gameData.level++;
            SceneManager.LoadScene("SampleScene");
        }

    }

    void CreateBalls()
    {
        int count = 2;
        if (gameData.balls == 1)
            count = 1;

        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(ballPrefab);
            var ball = obj.GetComponent<BallScript>();
            ball.ballInitialForce += new Vector2(10 * i, 0);
            ball.ballInitialForce *= 1 + level * ballVelocityMult;
        }
    }

    void StartLevel()
    {
        SetBackground();
        var yMax = Camera.main.orthographicSize * 0.8f;
        var xMax = Camera.main.orthographicSize * Camera.main.aspect * 0.85f;

        if (B3)
        {
            int togen = 0;
            if ((level > 1) && (level < 6))
                togen = 1;
            if ((level > 5) && (level < 10))
                togen = 2;
            if ((level > 9) && (level < 21))
                togen = 3;
            if (level > 20)
                togen = 4;
            if (b3tester > 0)
                togen = b3tester;
            CreateBlocks(redXOPrefab, xMax, yMax, togen, 4);
        }
        
        CreateBlocks(bluePrefab, xMax, yMax, level, 8);
        CreateBlocks(redPrefab, xMax, yMax, 1 + level, 10);
        CreateBlocks(greenPrefab, xMax, yMax, 1 + level, 12);
        CreateBlocks(yellowPrefab, xMax, yMax, 2 + level, 15);

        

        CreateBalls();
    }

    string OnOff(bool boolVal)
    {
        return boolVal ? "on" : "off";
    }

    void OnGUI()
    {
        GUI.Label(new Rect(5, 4, Screen.width - 10, 100), 
            string.Format(
                "<color=yellow><size=30>Level <b>{0}</b>  Balls <b>{1}</b>" +
                "  Score <b>{2}</b></size></color>",
                gameData.level, gameData.balls, gameData.points));
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(5, 14, Screen.width - 10, 100),
            string.Format(
            "<color=yellow><size=20><color=white>Space</color>-pause {0}" +
            " <color=white>N</color>-new" +
            " <color=white>J</color>-jump" +
            " <color=white>M</color>-music {1}" +
            " <color=white>S</color>-sound {2}" +
            " <color=white>Esc</color>-exit</size></color>",
            OnOff(Time.timeScale > 0), OnOff(!gameData.music),
            OnOff(!gameData.sound)), style);
    }

    void SetMusic()
    {
        if (gameData.music)
            audioSrc.Play();
        else
            audioSrc.Stop();
    }

    void Start()
    {
        gameData.is_reset = false;
        //SetBackground();
        audioSrc = Camera.main.GetComponent<AudioSource>();
        audioSrc.volume = PlayerPrefs.GetFloat("PhoneVolume");

        if (!gameStarted)
        {
            gameStarted = true;
            if (gameData.resetOnStart)
                gameData.Load();
        }


        Cursor.visible = false;
        level = gameData.level;
        SetMusic();
        StartLevel();
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var pos = transform.position;
            pos.x = mousePos.x;
            transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            gameData.music = !gameData.music;
            SetMusic();
        }
        if (Input.GetKeyDown(KeyCode.S))
            gameData.sound = !gameData.sound;

        if (Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync("MenuScene");
                Time.timeScale = 1;
            }
        } 
            

        if (Input.GetKeyDown(KeyCode.N))
        {
            gameData.Reset();
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    void OnApplicationQuit()
    {
        gameData.Save();
    }
}
