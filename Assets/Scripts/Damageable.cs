using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public float HitPoints = 0;
    [HideInInspector] public string TargetType;
    public TargetManager Manager;

    public void CheckHit(float damageDealt, GameObject hitGO)
    {
        switch (hitGO.tag)
        {
            case "BulletPistol":
                if (TargetType == "Pistol")
                    Hit(damageDealt);
                break;
            case "BulletMachineGun":
                if (TargetType == "MachineGun")
                    Hit(damageDealt);
                break;
            case "BulletGranade":
                Hit(damageDealt);
                break;
        }
    }

    private void Hit(float damageDealt)
    {
        HitPoints -= damageDealt;
        CheckHP();
    }

    private void CheckHP()
    {
        if (HitPoints <= 0)
        {
            Manager.DeactivateTarget(this);
        }
    }
}