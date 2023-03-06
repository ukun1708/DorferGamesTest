using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    float ySpeed;
    float originalStepOffset;

    CharacterController characterController;

    DynamicJoystick joystick;

    Animator animator;

    float maximumSpeed;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        joystick = GameController.instance.joystick;
        animator = GetComponent<Animator>();
        maximumSpeed = GameController.instance.playerMaxSpeed;
    }
    private void Update()
    {
        MovementPlayer();
    }

    public void MovementPlayer()
    {
        float hor = joystick.Horizontal;
        float ver = joystick.Vertical;

        Vector3 movedirection = new Vector3(hor, 0f, ver);

        float inputMagnitude = Mathf.Clamp01(movedirection.magnitude);
        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
        float speed = inputMagnitude * maximumSpeed;
        movedirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
        }
        else
        {
            characterController.stepOffset = 0f;
        }

        Vector3 velocity = movedirection * speed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movedirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movedirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 850 * Time.deltaTime);

            animator.SetBool("isAttack", false);
        }
    }
    public void Attack()
    {
        GameController.instance.weaponActivate = true;
    }
    public void StepSound()
    {
        SoundManager.instance.PlaySound(SoundManager.AudioType.step, 1f, Random.Range(0.95f, 1.1f), false);
    }
}
