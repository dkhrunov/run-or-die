using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveableMonster : Monster
{
    // Скорость 
    [SerializeField]
    private float speed = 2.0f;

    // Направление движения 
    private Vector3 direction;

    // Объект спрайта у монстра
    private SpriteRenderer sprite;

    // Значеие есть ли земля под ногами
    private bool isGrounded = false;

    // Получение спрайта и пули
    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Задаем направление движения 
    protected override void Start()
    {
        direction = transform.right;
    }

    // Проверка есть ли что-то под ногами 
    private void FixedUpdate()
    {
        CheckGround();
    }

    // Движение
    protected override void Update()
    {
        Move();
    }

    // Если сталкиваеться наш герой с монстром получаем урон или если пругнуть сверху убиваем монстра 
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            // Если мы пругнули на монстра то его убить
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.5f)
            {
                ReceiveDamage();
            }
            // иначе получаем урон
            else
            {
                unit.ReceiveDamage();
            }
        }
    }

    // Проверка земли под ногами
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.right * direction.x * 0.5f, 0.3F);

        isGrounded = colliders.Length > 1;
    }

    // Движение монстра в пределах (от края до края, или же от кубика до кубика)
    private void Move()
    {
        // От кубика до кубика границы
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x * 1.0f, 0.1f);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>() && !x.GetComponent<Bullet>() && !x.GetComponent<MoveableMonster>()))
        {
            sprite.flipX = direction.x < 0.0f;
            direction *= -1.0f;
            
        }

        // От края земли до края 
        if (!isGrounded)
        {
            sprite.flipX = direction.x < 0.0f;
            direction *= -1.0f;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

    }
}
