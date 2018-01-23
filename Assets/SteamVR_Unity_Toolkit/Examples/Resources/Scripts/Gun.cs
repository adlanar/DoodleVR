using UnityEngine;
using VRTK;

public class Gun : VRTK_InteractableObject
{
    private GameObject bullet;
    public float bulletSpeed = 150f;
    private float bulletLife = 5f;

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        FireBullet();
    }

    protected override void Start()
    {
        base.Start();
        bullet = transform.Find("Bullet").gameObject;
        bullet.transform.localScale = Vector3.one;
        bullet.SetActive(false);
    }

    private void FireBullet()
    {
        GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
        bulletClone.SetActive(true);
        Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
        rb.AddForce(-bullet.transform.forward * bulletSpeed);
        Destroy(bulletClone, bulletLife);
    }
}