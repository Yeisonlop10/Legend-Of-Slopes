using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private LayerMask layerMask;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private Animator anim;
    private BoxCollider[] swordColliders;
    private GameObject fireTrail;
    private ParticleSystem fireTrailParticles;
    private ParticleSystem attackParticles;
    private GameObject attackTrail;


    // Use this for initialization
    void Start ()
    {
        fireTrail = GameObject.FindWithTag("Fire") as GameObject;
        fireTrail.SetActive (false);
        attackTrail = GameObject.FindWithTag("Attacktrail") as GameObject;
        attackTrail.SetActive(false);
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.instance.GameOver)
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.SimpleMove(moveDirection * moveSpeed);

            if (moveDirection == Vector3.zero)
            {
                anim.SetBool("IsWalking", false);
            }
            else
            {
                anim.SetBool("IsWalking", true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                anim.Play("DoubleChop");
                StartCoroutine(attackTrailRoutine());
            }
            if (Input.GetMouseButtonDown(1))
            {
                anim.Play("SpinAttack");
                StartCoroutine(attackTrailRoutine());

            }
        }
	}

    void FixedUpdate()
    {
        if (!GameManager.instance.GameOver)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);

            if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.point != currentLookTarget)
                {
                    currentLookTarget = hit.point;
                }

                Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
        }
        
        
    }
    public void BeginAttack()
    {
        foreach (var weapon in swordColliders)
        {
            weapon.enabled = true;
        }
    }
    public void EndAttack()
    {
        foreach (var weapon in swordColliders)
        {
            weapon.enabled = false;
        }
    }

    public void SpeedPowerUp()
    {
        StartCoroutine(fireTrailRoutine());
    }

    IEnumerator fireTrailRoutine()
    {
        fireTrail.SetActive(true);
        moveSpeed = 10f;
        yield return new WaitForSeconds(10f);
        moveSpeed = 6f;
        fireTrailParticles = fireTrail.GetComponent<ParticleSystem>();
        var em = fireTrailParticles.emission;
        em.enabled = false;
        yield return new WaitForSeconds(3f);
        em.enabled = true;
        fireTrail.SetActive(false);
        
    }
    IEnumerator attackTrailRoutine()
    {
        attackTrail.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackParticles = attackTrail.GetComponent<ParticleSystem>();
        var emu = attackParticles.emission;
        emu.enabled = false;
        yield return new WaitForSeconds(0.5f);
        emu.enabled = true;
        attackTrail.SetActive(false);

    }
}
