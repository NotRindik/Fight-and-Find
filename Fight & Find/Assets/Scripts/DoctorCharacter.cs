using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DoctorCharacter : Characters
{
    [SerializeField]private Animator maskAnim;
    [SerializeField]private ParticleSystem attackParticle;
    [SerializeField]private float electrysphereAttackTime;
    [SerializeField]private float radius;
    [SerializeField]private LayerMask EnemyMask;

    [SerializeField] private float damage;
    [SerializeField] private float secondPerDamage;
    private float currentSecondPerDamage;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(ElectrysphereAttack());
    }


    protected override void Update()
    {
        base.Update();
        DamageByElectrysphereAttack();
    }

    public void DamageByElectrysphereAttack()
    {
        if (currentSecondPerDamage > 0)
            return;
        radius = maskAnim.transform.localScale.x / 2;
        Collider2D[] colider = Physics2D.OverlapCircleAll(maskAnim.transform.position, radius, EnemyMask); 

        foreach (Collider2D col in colider)
        {
            if(col.TryGetComponent(out Enemy enemy))
            {
                enemy.GetDamage(damage);
                currentSecondPerDamage = secondPerDamage;
            }
        }
    }

    private void FixedUpdate()
    {
        currentSecondPerDamage -= Time.deltaTime; 
    }

    private void OnDrawGizmos()
    {
        radius = maskAnim.transform.localScale.x / 2;
        Gizmos.DrawWireSphere(maskAnim.transform.position, radius);
    }

    IEnumerator ElectrysphereAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(electrysphereAttackTime);
            maskAnim.Play("Mask");
            attackParticle.Emit(1);
        }
    }
}
