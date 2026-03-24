
using System;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private Transform _originTransform;


    private void Update() {
        Debug.DrawRay(_originTransform.position, _originTransform.forward * 100, Color.yellow);
        if (Input.GetButtonDown("Fire2")) {
            Ray ray = new Ray(_originTransform.position, _originTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit)){
                TargetController target = hit.transform.GetComponent<TargetController>();
                if (target != null) target.TakeDamage(1);
            }
        }
    }
    
       

}



