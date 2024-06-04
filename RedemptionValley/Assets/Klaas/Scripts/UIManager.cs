using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [Header("Player Ammo")]
    public TextMeshProUGUI ammoText;

    [Header("Player Script")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WeaponClass currentWeapon;

    private void Start()
    {
        gameObject.TryGetComponent<WeaponClass>(out currentWeapon);
        ammoText.text = $"currentWeapon.ammo";
    }


    public void UpdateAmmo()
    {
        ammoText.text = $"{currentWeapon.ammo}";
    }
}
