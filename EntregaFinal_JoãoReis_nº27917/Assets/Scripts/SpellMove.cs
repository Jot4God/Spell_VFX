using UnityEngine;
using System.Collections;

public class SpellMove : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 20f;

    [Header("Lifetime")]
    public float lifeTime = 5f;

    [Header("Impact")]
    public GameObject impactVFX;   // prefab do impacto
    public float impactDestroyTime = 2f;

    private bool hasImpacted = false;

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

        if (impactVFX != null)
        {
            GameObject impact = Instantiate(
                impactVFX,
                transform.position,
                Quaternion.identity
            );

            Destroy(impact, impactDestroyTime);
        }
    }
}
