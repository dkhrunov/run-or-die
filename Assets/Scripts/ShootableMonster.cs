using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monster 
{
    [SerializeField]
    private float rate = 2.0f;

    private Bullet bullet;

    protected override void Awake()
    {
        bullet = Resources.Load<Bullet>("BlackBullet");
    }

	// Use this for initialization
	protected override void Start () 
    {
		InvokeRepeating ("Shoot", rate, rate);
	}
	
	// Update is called once per frame
	protected override void Update () 
    {
		
	}

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;

        // Instantiate делает копию 1) объекта 2) задает для клона позицию 3)орентация объекта
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
    }

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
}
