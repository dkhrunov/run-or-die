using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : Unit 
{
    // Кол-во жизней
    [SerializeField]
    private int lives = 5;

    public int Lives
    {
        set 
        { 
            if (value <= 5) lives = value;
            livesBar.Refresh();
        }
        get { return lives; }
    }
    private LivesBar livesBar;

    // Скорость
    [SerializeField]
    private float speed = 3.0f;
    // Сила прыжка
    [SerializeField]
    private float jumpforce = 15.0f;
    // Земля под ногами
    private bool isGrounded = false;
    // Объект пули
    private Bullet bullet;
    // Объект для новой пули
    private Bullet newBullet;

    // Состояние героя (стоит\бежит\прыгает)
    public CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    // Получение объектов и компонентов героя
    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("HeroBullet");
    }

    // Проверка на землю под ногами
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        // есть емля значит герой стоит
        if (isGrounded) State = CharState.Idle;

        // Если нажали на mlb стреляем
        if (Input.GetButtonDown("Fire1")) 
            shoot();

        // Если нажаты клавиши ходьбы то бежим
        if (Input.GetButton("Horizontal"))
            Run();

        // прыжок только от земли 
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    // Ходьба
    private void Run()
    {
        // Направление движения в зависимости от нажатой кнопки <- или ->
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        // меняем позицию 
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        // в зависимости от направления разворачиваем в нужную сторону спрайт 
        sprite.flipX = direction.x < 0.0f;

        // Бег только если есть земля под ногами
        if (isGrounded) State = CharState.Run;
    }

    // Прыжок
    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
    }

    // Стрельба
    private void shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;

        // Пока существует хоть одна пуля нельзя создать новую
        if (newBullet == null)
        {
            newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
            newBullet.Sprite.flipX = newBullet.Direction.x < 0.0f;
        }
        

    }

    //  Получение урона если кол-во жизней = 0 то смерть
    public override void ReceiveDamage()
    {
        Lives--;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 10.0f, ForceMode2D.Impulse);

        Debug.Log(lives);

        if (lives <= 0)
        {
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene(0);
        }
    }

    // Проверка земли под ногами
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
    }

    // Если столкнулить с какимто таргетом ...
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }
}

// Перечисление состояния героя
public enum CharState
{
    Idle,
    Run,
    Jump
}