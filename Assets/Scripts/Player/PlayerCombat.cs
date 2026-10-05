using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _slot1Weapon;
    [SerializeField] private PlayerWeapon _slot2Weapon;

    public PlayerWeapon Slot1Weapon => _slot1Weapon;
    public PlayerWeapon Slot2Weapon => _slot2Weapon;

    // Called by PlayerController when X is pressed.
    public void AttackSlot1()
    {
        _slot1Weapon?.Attack(this);
    }

    // Called by PlayerController when C is pressed.
    public void AttackSlot2()
    {
        _slot2Weapon?.Attack(this);
    }

    // Call this from inventory/equipment code when a weapon changes.
    public void Equip(WeaponSlot slot, PlayerWeapon weapon)
    {
        if (slot == WeaponSlot.Slot1)
            _slot1Weapon = weapon;
        else
            _slot2Weapon = weapon;
    }
}
