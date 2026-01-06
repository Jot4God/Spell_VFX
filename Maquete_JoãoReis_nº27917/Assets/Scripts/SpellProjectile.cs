using UnityEngine;
using System.Collections;

public class SpellProjectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    public GameObject impactVFX;
    public float impactDestroyTime = 2f;

    private bool hasImpacted;

    void Start()
    {
        StartCoroutine(LifeRoutine());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        TriggerImpact();
        Destroy(gameObject);
    }

    void TriggerImpact()
    {
        if (hasImpacted) return;
        hasImpacted = true;

        if (impactVFX == null) return;

        GameObject impact = Instantiate(impactVFX, transform.position, Quaternion.identity);
        Destroy(impact, impactDestroyTime);
    }
}
