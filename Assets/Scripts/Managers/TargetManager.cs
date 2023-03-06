using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{

    #region Singleton
    public static TargetManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject target;

    LerpManager lerp;

    Animator animator;

    private void Start()
    {
        lerp = LerpManager.instance;
        animator = GameController.instance.player.GetComponent<Animator>();
    }

    private void Update()
    {
        ClickVerification();
    }
    void ClickVerification()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = null;
            animator.SetBool("isAttack", false);
            GameController.instance.weaponActivate = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            FindClosestTarget();
        }
    }

    public void FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        //float closestDistance = Mathf.Infinity;
        float closestDistance = 3f;

        GameObject currentGround = null;

        foreach (var targetCurrent in targets)
        {
            float currentDistance;

            GameObject player = GameController.instance.player;

            Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z + 0.5f);
            currentDistance = Vector3.Distance(playerPos, targetCurrent.transform.position);

            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;

                currentGround = targetCurrent;

            }
        }

        if (currentGround != null)
        {
            target = currentGround;

            StartCoroutine(RotationAndTakeTarget(target, 4f));
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }

    public IEnumerator RotationAndTakeTarget(GameObject target, float speedRotate)
    {
        Vector3 dir = target.transform.position - GameController.instance.player.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(dir);

        yield return StartCoroutine(lerp.LerpRot(GameController.instance.player, GameController.instance.player.transform.eulerAngles, new Vector3(0f, toRotation.eulerAngles.y, 0f), speedRotate));


        if (target.GetComponent<Barn>())
        {
            BlockManager.instance.PumpingBlock(target.transform.position);
        }
        else
        {
            animator.SetBool("isAttack", true);
        }
    }
}
