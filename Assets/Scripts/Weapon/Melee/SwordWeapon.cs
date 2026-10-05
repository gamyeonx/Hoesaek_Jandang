using UnityEngine;

// Sample weapon assigned to PlayerCombat Slot 1.
public class SwordWeapon : PlayerWeapon
{
    [SerializeField] private int _damage = 10;

    public override void Attack(PlayerCombat owner)
    {
        Debug.Log($"Sword attack: {_damage} damage", owner);
    }
}
