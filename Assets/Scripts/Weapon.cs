using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [Header("Setup")] 
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject bulletGO;

    [Header("Rapid fire options")] [Range(0, 10)] 
    [SerializeField] private float fireRate;
    [SerializeField] private bool rapidFire;

    [Header("Ammo")] 
    [SerializeField] private bool useAmmo;
    [SerializeField] private int ammoAmount;

    [Header("Zooming")] 
    [SerializeField] private bool canZoom;
    [SerializeField] private float fovZoom;
    private float _fovDefault;

    private WaitForSeconds _fireRateWait;
    private Animator _anim;


    private void Awake()
    {
        _fireRateWait = new WaitForSeconds(1 / fireRate);
        _anim = GetComponent<Animator>();

        if (canZoom)
            _fovDefault = 0;
    }

    public void Shoot()
    {
        if (useAmmo && ammoAmount <= 0)
            return;

        Instantiate(bulletGO, firingPoint.position, firingPoint.rotation);
        _anim.Play("WeaponShooting");
    }

    public IEnumerator RapidFire()
    {
        if (rapidFire)
        {
            while (true)
            {
                Shoot();
                yield return _fireRateWait;
            }
        }
        else
        {
            Shoot();
            yield return null;
        }
    }

    public void ZoomIn(Camera camera)
    {
        if (canZoom)
        {
            if (_fovDefault == 0)
                _fovDefault = camera.fieldOfView;

            camera.fieldOfView = fovZoom;
        }
            
    }

    public void ZoomOut(Camera camera)
    {
        if (canZoom)
        {
            camera.fieldOfView = _fovDefault;
        }
    }
}