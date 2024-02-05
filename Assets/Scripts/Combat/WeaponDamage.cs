using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider userCollider;
    private List<Collider> alreadyHit = new List<Collider>();
    private int damage;
    private float knockback;

    private void OnEnable()
    {
        alreadyHit.Clear();
    }

    //This method makes sure that the player does not get damaged by their own sword, as well as
    //making sure the same enemy doesn't get damage dealt to them multiple times for a single swing
    private void OnTriggerEnter(Collider other)
    {
        if (other == userCollider) return;

        if (alreadyHit.Contains(other)) return;
        else alreadyHit.Add(other);

        if(other.TryGetComponent<Health>(out Health currHealth))
        {
            currHealth.DealDamage(damage);
        }
        if(other.TryGetComponent<ForceReciever>(out ForceReciever forceReciever))
        {
            Vector3 forceDirection = (other.transform.forward - userCollider.transform.position).normalized;
            forceReciever.AddForce(forceDirection * knockback);
        }
    }

    public void SetDamage(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
