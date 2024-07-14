using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{
    [SerializeField] Button selectPistol;
    [SerializeField] Button selectShotgun;
    [SerializeField] PlayerShooter shooter;

    private void Start()
    {
        selectPistol.onClick.RemoveAllListeners();
        selectPistol.onClick.AddListener(() =>
        {
            shooter.SelectGun(AllGunsHolder.Instance.SelectGun<Pistol>());
        });

        selectShotgun.onClick.RemoveAllListeners();
        selectShotgun.onClick.AddListener(() =>
        {
            shooter.SelectGun(AllGunsHolder.Instance.SelectGun<Shotgun>());
        });
    }
}
