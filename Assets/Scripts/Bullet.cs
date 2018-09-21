using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    private GameObject parent;
    public GameObject Parent 
    { 
        set { parent = value; }
        get { return parent; }
    }

    private float speed = 10.0f;

    private SpriteRenderer sprite;

    public SpriteRenderer Sprite
    {
        set { sprite = value; }
        get { return sprite; }
    }

    private Vector3 direction;

    public Vector3 Direction 
    { 
        set { direction = value; }
        get { return direction; }
    }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

	// Очистка при старте
	void Start () 
    {
        Destroy(gameObject, 0.7f);
	}
	
	// Полет пули в соответсвие с направление героя 
	void Update () 
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
	}

    // Когда во что то врезаеться (монстры), и это не коллайдер того объекта из которого мы выпустили пулю, то удаляем (убиваем) этот объект
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit.gameObject != parent)
        {
            if ( unit is Monster && !(unit is MoveableMonster || unit is ShootableMonster) ) unit.ReceiveDamage();
            Destroy(gameObject);
        }
    }
}
