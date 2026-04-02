
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private Transform _originTransform;
    [SerializeField] private float _maxAmmo = 30f;
    [SerializeField] private float _currentAmmo = 30f;
    
    public GameObject _fire;
    public GameObject _hitPoint;

    private void Update()
    {
        Shooting();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Shooting()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _currentAmmo = _maxAmmo;
        }
        
        if (_currentAmmo == 0) return;
        
        Debug.DrawRay(_originTransform.position, _originTransform.forward * 100, Color.yellow);
        if (Input.GetButtonDown("Fire1")) {
            Ray ray = new Ray(_originTransform.position, _originTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit)){
                EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                if (enemy != null) enemy.TakeDamage(1);
            }
            GameObject a = Instantiate(_fire, _originTransform.position, Quaternion.identity);
            GameObject b = Instantiate(_hitPoint, hit.point, Quaternion.identity);

            Destroy(a, 0.2f);
            Destroy(b, 2f);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _currentAmmo--;
        }
       
       
        
    }

    

}



