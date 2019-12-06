using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static Player instance;

    public AudioSource one;
    public AudioSource two;

    public static Vector2 velocity;
    public static int hp;
    public static int laserDamage;

    public GameObject effectView;
    public RectTransform LeftButton;
    public RectTransform RightButton;
    public RectTransform PausePanel;
    public RectTransform TutorialRect;
    public RectTransform SkipTutorialRect;
    public Text pauseText;
    public Text tutorialText;
    bool paused;
    public static bool isInTutorial;
    public static bool flashing;
    public static bool tripleShoot;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        Debug.Log("Playing Level " + DataBase.ins.XmlDataBase.gameDB.status);
    }

    class PlayerControls
    {
        public KeyCode up, down, right, left, shoot, esc, pause;
        public PlayerControls(KeyCode up, KeyCode down, KeyCode right, KeyCode left, KeyCode shoot, KeyCode esc, KeyCode pause)
        {
            this.up = up;
            this.down = down;
            this.right = right;
            this.left = left;
            this.shoot = shoot;
            ///
            this.esc = esc;
            this.pause = pause;
        }
    }
    PlayerControls Controls;

    class TutorialInfo
    {
        int ask = 1;
        int ans = 0;
        int messageNumber = 0;
        public bool needUpdate = false;
        static string[] Messages = new string[]
        {
            "¡Bienvenido a\nFinding Planet Nine!\n\nUsa la flecha hacia arriba para desplazarte por el espacio",
            "¡Bien hecho!\n\nUsa la flecha hacia abajo para desplazarte en otra dirección",
            "¡Perfecto!\n\nUsa la flecha hacia la derecha para avanzar",
            "¡Lo haces bastante bien!\n\nAhora usa la flecha hacia la izquierda para mover la nave en reversa",
            "Pulsa la barra espaciadora para disparar rayos láser a los obstáculos\n\n¡Dispara y derriba a alguno de ellos!",
            "¡Excelente!\nDurante tu aventura encontrarás mejoras que podrás tomar al pasar sobre ellas, inténtalo",
            "Puedes pausar el juego si necesitas un descanso.\nPulsa la tecla \"P\" para pausar el juego",
            "¡Felicidades, has\ncompletado el tutorial!\n\nPresiona Esc para ir a la pantalla de selección de nivel",
        };
        PlayerControls playerControls;
        public GameObject obstacleSpawner;
        public GameObject powerupSpawner;
        public TutorialInfo(PlayerControls playerControls)
        {
            this.playerControls = playerControls;
        }
        public int keyCodeToInt(KeyCode k)
        {
            if(k == playerControls.up)
                return 1;
            if (k == playerControls.down)
                return 2;
            if (k == playerControls.right)
                return 4;
            if (k == playerControls.left)
                return 8;
            if (k == playerControls.pause)
                return 64;
            return -1;
        }
        public bool set(KeyCode k)
        {
            int x = keyCodeToInt(k);
            if (x == -1)
            {
                return false;
            }
            //Debug.Log("ask: " + ask + ", x: " + x);
            if((ask & x) != 0)
            {
                ans |= x;
                if(ans == ask)
                {
                    needUpdate = true;
                    return true;
                }
            }
            return false;
        }
        public bool set(int x)
        {
            if(x != 16 && x != 32)
            {
                return false;
            }
            if ((ask & x) != 0)
            {
                ans |= x;
                if (ans == ask)
                {
                    needUpdate = true;
                    return true;
                }
            }
            return false;
        }
        public string getCurrentMessage()
        {
            return Messages[messageNumber];
        }
        public bool advanceToNextMessage()
        {
            messageNumber++;
            if(messageNumber >= Messages.Length)
            {
                messageNumber--;
                return false;
            }
            return true;
        }
        public void update()
        {
            if(ask == keyCodeToInt(playerControls.left))
            {
                obstacleSpawner.gameObject.SetActive(true);
            }
            else if(ask == 16)
            {
                obstacleSpawner.gameObject.SetActive(false);
                var obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
                foreach(var obstacle in obstacles)
                {
                    Destroy(obstacle);
                }
                powerupSpawner.gameObject.SetActive(true);
                powerupSpawner.gameObject.SendMessage("StartTutorialRoutine", SendMessageOptions.DontRequireReceiver);
            }
            ask <<= 1;
            ans = 0;
            advanceToNextMessage();
            needUpdate = false;
        }
    }
    TutorialInfo tutorialInfo;

    private bool isDead = false;
    int direction = 0;
    private Rigidbody2D rb2d;

    public GameObject laser;
    public float laserDelay;
    bool canShoot = true;


    void Start()
    {
        laserDamage = 1;
        flashing = false;
        tripleShoot = false;
        hp = DataBase.ins.XmlDataBase.playerDB.hp;
        velocity = DataBase.ins.XmlDataBase.playerDB.velocity;
        Controls = new PlayerControls(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.Space, KeyCode.Escape, KeyCode.P);

        if (DataBase.ins.XmlDataBase.gameDB.status == 0)
        {
            isInTutorial = true;
            tutorialInfo = new TutorialInfo(Controls);
            tutorialInfo.obstacleSpawner = GameObject.Find("ObstacleSpawner");
            tutorialInfo.obstacleSpawner.gameObject.SetActive(false);
            tutorialInfo.powerupSpawner = GameObject.Find("PowerUpSpawner");
            tutorialInfo.powerupSpawner.gameObject.SetActive(false);
            tutorialText.text = tutorialInfo.getCurrentMessage();
            TutorialRect.gameObject.SetActive(true);
            SkipTutorialRect.gameObject.SetActive(true);
        }
        else
        {
            isInTutorial = false;
        }

        laserDelay = 0.3f;
        /*
        Sprite playerSprite = Resources.Load<Sprite>("Sprites/"+DataBase.ins.XmlDataBase.playerDB.texture);
        GetComponent<SpriteRenderer>().sprite = playerSprite;

        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.transform.localScale *= DataBase.ins.XmlDataBase.playerDB.dimensions.width / playerSprite.rect.width;
        */
        rb2d = GetComponent<Rigidbody2D>();

        one.volume = PlayerPrefs.GetFloat("SfxVolume", 0.75f);
        two.volume = PlayerPrefs.GetFloat("SfxVolume", 0.75f);

        paused = false;
    }

    void Update()
    {
        if (isInTutorial)
        {
            if (Input.GetKey(Controls.up))
            {
                tutorialInfo.set(Controls.up);
            }
            if (Input.GetKey(Controls.down))
            {
                tutorialInfo.set(Controls.down);
            }
            if (Input.GetKey(Controls.right))
            {
                tutorialInfo.set(Controls.right);
            }
            if (Input.GetKey(Controls.left))
            {
                tutorialInfo.set(Controls.left);
            }
            if (Input.GetKeyDown(Controls.shoot))
            {
                tutorialInfo.set(Controls.shoot);
            }
            if (Input.GetKey(Controls.esc))
            {
                SceneManager.LoadScene("LevelSelectionScreen");
            }
            if (Input.GetKeyDown(Controls.pause))
            {
                tutorialInfo.set(Controls.pause);
            }
            if (tutorialInfo.needUpdate)
            {
                tutorialInfo.update();
                tutorialText.text = tutorialInfo.getCurrentMessage();
            }
        }
        if (isDead == false)
        {
            if (Input.GetKeyDown(Controls.pause))
            {
                if (paused)
                {
                    PausePanel.gameObject.SetActive(false);
                    LeftButton.gameObject.SetActive(false);
                    RightButton.gameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                    paused = false;
                }
                else
                {
                    pauseText.text = "PAUSA";
                    PausePanel.gameObject.SetActive(true);
                    LeftButton.gameObject.SetActive(true);
                    RightButton.gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                    paused = true;
                }
            }
            if (Input.GetKey(Controls.up))
            {
                float x = rb2d.velocity.x;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(x, velocity.x));
                direction = 1;
            }
            else if (direction == 1)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (Input.GetKey(Controls.right))
            {
                float y = rb2d.velocity.y;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(velocity.y, y));
                direction = 2;
            }
            else if (direction == 2)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (Input.GetKey(Controls.left))
            {
                float y = rb2d.velocity.y;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(-velocity.y, y));
                direction = 3;
            }
            else if (direction == 3)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (Input.GetKey(Controls.down))
            {
                float x = rb2d.velocity.x;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(x, -velocity.x));
                direction = 4;
            }
            else if (direction == 4)
            {
                rb2d.velocity = Vector2.zero;
                direction = 0;
            }
            if (direction == 0)
            {
                Vector2 v = rb2d.velocity;
                v.x = 0;
                v.y = (v.y > 0 ? 0 : v.y);
                rb2d.velocity = v;
            }
            if (Input.GetKey(Controls.esc))
            {
                DataBase.ins.XmlDataBase.gameDB.status = -1;
                SceneManager.LoadScene("LevelSelectionScreen");
            }
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }
    void LateUpdate()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, Camera.main.transform.position.z));
        Vector3 playerSize = GetComponent<Renderer>().bounds.size;
        //Debug.Log("Screen bounds: " + screenBounds.x + ", " + screenBounds.y);

        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + playerSize.x/ 2, screenBounds.x - playerSize.x / 2);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + playerSize.y / 2, screenBounds.y - playerSize.y / 2);
        transform.position = viewPos;

        if(isDead == false)
        {
            if (canShoot && Input.GetKeyDown(Controls.shoot))
            {
                Shoot();
            }
        }
    }
    void Shoot()
    {
        canShoot = false;
        for(int i = 0; i < (tripleShoot ? 3 : 1); i++)
        {

            GameObject laserInstance = Instantiate(laser, new Vector2(transform.position.x + 70, transform.position.y - 10), transform.rotation);
            laserInstance.SendMessage("SetType", Projectile.ProjectileType.PlayerProjectile, SendMessageOptions.DontRequireReceiver);
            laserInstance.SendMessage("SetDamage", laserDamage, SendMessageOptions.DontRequireReceiver);
            if (laserDamage == 3)
            {
                laserInstance.transform.localScale *= 2;
            }
           
            if(i == 1)
            {
                Vector3 moveDirection = new Vector2(1, 0.6f).normalized;
                laserInstance.GetComponent<Rigidbody2D>().velocity = moveDirection * laserInstance.GetComponent<Rigidbody2D>().velocity.x;
                Vector3 laserSize = GetComponent<Renderer>().bounds.size;
                laserInstance.transform.position = new Vector2(laserInstance.transform.position.x + 10, laserInstance.transform.position.y + 10);
                if (moveDirection != Vector3.zero)
                {
                    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                    laserInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
            else if(i == 2)
            {
                Vector3 moveDirection = new Vector2(1, -0.6f).normalized;
                laserInstance.GetComponent<Rigidbody2D>().velocity = moveDirection * laserInstance.GetComponent<Rigidbody2D>().velocity.x;

                if (moveDirection != Vector3.zero)
                {
                    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                    laserInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }
        StartCoroutine(NoFire());
        playOne();
    }

    void playOne() {
        
        one.Play();
    }

    public void playTwo(bool collided){
        if (!collided && isInTutorial)
        {
            tutorialInfo.set(16);
        }
        two.Play();
    }
    IEnumerator NoFire()
    {
        yield return new WaitForSeconds(laserDelay);
        canShoot = true;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            hp--;
            if(hp <= 0)
            {
                Die();
            }
        }

        if (other.gameObject.tag == "PowerUp") {
            Debug.Log("tomó un powerUp");
        }
    }
    
    public void MakeDamage(int damage)
    {
        if (isInTutorial || flashing)
        {
            return;
        }
        hp = hp - damage;
        Debug.Log("Player hp: " + hp);
        if (hp <= 0)
        {
            Die();
        }
        else if(hp < 100)
        {
            if (!flashing)
            {
                StartCoroutine(Flash(GetComponent<SpriteRenderer>(), 2.0f, 0.2f, true));
            }
        }
    }

    IEnumerator Flash(SpriteRenderer sr, float duration, float delay, bool isPlayer)
    {
        if(isPlayer)
            flashing = true;
        float startTime = Time.time;
        Color color = sr.color;
        while(Time.time - startTime < duration)
        {
            if(color.a == 1)
            {
                color.a = 0.5f;
            }
            else
            {
                color.a = 1;
            }
            sr.color = color;
            yield return new WaitForSeconds(delay);
        }
        color.a = 1;
        sr.color = color;
        if(isPlayer)
            flashing = false;
    }

    public void Die()
    {
        if (isDead) return;
        Debug.Log("Die player");
        rb2d.velocity = Vector2.zero;
        isDead = true;
        GameControl.instance.PlayerDied();
    }

    public void DieFinish()
    {
        if (isDead) return;
        Debug.Log("DieFinish player");
        rb2d.velocity = Vector2.zero;
        isDead = true;
        GameControl.instance.PlayerDiedFinish();
    }

    void OnBecameInvisible()
    {
        if (!isDead)
        {
            Die();
        }
    }

    void ActivateShield(float duration)
    {
        effectView.gameObject.SetActive(true);
        StartCoroutine(ShieldControl(duration));
    }

    IEnumerator ShieldControl(float duration)
    {
        yield return new WaitForSeconds(duration - 2.0f);
        StartCoroutine(Flash(effectView.GetComponent<SpriteRenderer>(), 2.0f, 0.2f, false));
    }

    void DeactivateShield()
    {
        effectView.gameObject.SetActive(false);
    }

    public void SetPowerUpOfTutorial()
    {
        tutorialInfo.set(32);
    }
}
