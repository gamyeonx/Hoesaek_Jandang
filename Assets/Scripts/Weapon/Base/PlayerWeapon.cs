using UnityEngine;

// Attach one implementation of this class to each equippable weapon prefab.
public abstract class PlayerWeapon : MonoBehaviour
{
    public abstract void Attack(PlayerCombat owner);
}
