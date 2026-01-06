using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Main Spell (fica sempre ativo)")]
    public GameObject mainSpellPrefab;   // prefab do Project
    public Transform castPoint;

    [Header("Projectile (clones)")]
    public GameObject projectilePrefab;  // prefab do Project (OBRIGATÓRIO)
    public float projectileSpeed = 20f;

    [Header("Attack")]
    public KeyCode attackKey = KeyCode.K;
    public Animator animator;

    private GameObject mainSpellInstance;

    void Start()
    {
        EnsureMainSpell();
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            EnsureMainSpell();
            CastProjectile();
        }
    }

    void EnsureMainSpell()
    {
        if (mainSpellInstance != null) return;
        if (mainSpellPrefab == null || castPoint == null) return;

        mainSpellInstance = Instantiate(mainSpellPrefab, castPoint);
        mainSpellInstance.transform.localPosition = Vector3.zero;
        mainSpellInstance.transform.localRotation = Quaternion.identity;
    }

    void CastProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("projectilePrefab está NULL. Provavelmente arrastaste um objeto da cena e ele foi destruído. Mete um Prefab do Project.");
            return;
        }

        if (castPoint == null)
        {
            Debug.LogError("castPoint está NULL.");
            return;
        }

        if (animator != null)
            animator.SetTrigger("Attack");

        Vector3 direction = transform.forward;

        GameObject projectile = Instantiate(
            projectilePrefab,
            castPoint.position,
            Quaternion.LookRotation(direction)
        );

        // Configurar script do projétil (lifetime + impacto) se existir
        SpellProjectile sp = projectile.GetComponent<SpellProjectile>();
        if (sp != null)
        {
            sp.speed = projectileSpeed;
        }

        // Se tiver Rigidbody, movimenta por física
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}
