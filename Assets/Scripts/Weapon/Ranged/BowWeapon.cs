using UnityEngine;

// Sample weapon assigned to PlayerCombat Slot 2.
public class BowWeapon : PlayerWeapon
{
    [SerializeField] private int _damage = 7;

    public override void Attack(PlayerCombat owner)
    {
        Debug.Log($"Bow attack: {_damage} damage", owner);
    }
}
