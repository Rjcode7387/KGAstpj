using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Animator),typeof(RigBuilder))]
public class PlayerAction : MonoBehaviour
{
   Animator animator;
   Rig rig;
    private WaitUntil untilReload;
    private WaitUntil untilGrenade;


    public AnimationClip reloadclip;
    public AnimationClip grenadeclip;

    private bool isReloading = false;
    private bool isFiring = false;
    private bool isThrowingGrenade = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<RigBuilder>().layers[0].rig;
    }
    private void Start()
    {
        untilReload = new WaitUntil(()=>isReloading);
        untilGrenade = new WaitUntil(()=> isThrowingGrenade);
        StartCoroutine(ReloadCouroutine());
        StartCoroutine(GrenadeCouroutine());
    }


    private void Update()
    {
        if (false == isReloading && Input.GetKeyDown(KeyCode.R))
        {
            rig.weight = 0f;
            isReloading = true;
            animator.SetTrigger("Reload");
        }
        if (Input.GetMouseButton(0) && !isReloading && !isThrowingGrenade)
        {
            if (!isFiring)
            {
                isFiring = true;
                animator.SetBool("IsFiring", true);
            }
        }
        else if (isFiring)
        {
            isFiring = false;
            animator.SetBool("IsFiring", false);
        }
        print($"{Input.GetKeyDown(KeyCode.T)}, {!isReloading}, {isThrowingGrenade}");
        if (Input.GetKeyDown(KeyCode.T) && !isReloading && !isThrowingGrenade)
        {
            rig.weight = 0f;
            isThrowingGrenade = true;
            animator.SetTrigger("ThrowGrenade");
        }
    }
    public void OnReloadEnd()
    {
        ////�ܺο��� ���۵� FBX ���� �ִϸ��̼ǿ��� �̺�Ʈ�� �߰��Ҽ� �ִµ�
        //rig.weight = 1f;//������ Animation Rig�� �ִϸ��̼� �̺�Ʈ�� weight�� ������ �� ����
        isReloading = false;
    }
    public void OnGrenadeEnd()
    {
        isThrowingGrenade = false;
    }
    IEnumerator ReloadCouroutine()
    {
        while (true)
        {
            yield return untilReload;
            yield return new WaitForSeconds(reloadclip.length);
            isReloading = false;
            rig.weight = 1f;
        }
    }
    IEnumerator GrenadeCouroutine()
    {
        while (true)
        {
            yield return untilGrenade;
            yield return new WaitForSeconds(grenadeclip.length);
            isReloading = false;
            rig.weight = 1f;
        }
    }
}
