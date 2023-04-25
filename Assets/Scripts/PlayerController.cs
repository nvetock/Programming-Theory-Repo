using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    //player gun variables
    [SerializeField] Transform firingPoint; //find where the shot will fire from
    [SerializeField] float firingSpeed; //how fast is THIS fired
    private float lastTimeShot = 0;
    [SerializeField] int maxAmmo = 6;
    public int curAmmo;
    [SerializeField] float reloadSpeed;




    void Start()
    {
        curAmmo = maxAmmo;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotationInput();
        HandleShootInput();
        HandleReload();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    void HandleRotationInput()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }
    public void HandleShootInput()
    {
        if (Input.GetButton("Fire1") && curAmmo >= 1)
        {
            if (lastTimeShot + firingSpeed <= Time.time)
            {
                GameObject bullet = BulletPool.SharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = firingPoint.transform.position;
                    bullet.transform.rotation = firingPoint.transform.rotation;
                    bullet.SetActive(true);
                    bullet.GetComponent<Bullet>().Move();
                    curAmmo--;
                    Debug.Log("Ammo: " + curAmmo);
                }
                lastTimeShot = Time.time;
            }
        }
        else if (Input.GetButton("Fire1") && curAmmo <= 0)
        {
            Debug.Log("Need to reload!");
        }
    }

    void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reloadng...");
            curAmmo = maxAmmo;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            int ammoAmt = other.gameObject.GetComponent<Ammo>().amount;

            curAmmo += ammoAmt;
            Debug.Log("Ammo Pickup! Ammo: " + curAmmo);
            Destroy(other.gameObject);
        }
    }

}
