using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poketmon : MonoBehaviour
{
    // === ���ϸ� �⺻ ���� ===
    public string pokemonName;        // ���ϸ� �̸�
    public int hp = 100;             // ü��
    public int attackPower = 20;     // ���ݷ�
    public bool hasActed = false;    // �̹� �Ͽ� �ൿ�ߴ��� ����
    public bool isEnemy = false;     // true�� �� ���ϸ�

    // === UI ������Ʈ ���� ===
    public Button selectButton;       // ���ϸ� ���ÿ� ��ư
    public Button targetButton;       // Ÿ�� ���ÿ� ��ư
    public Text hpText;               // HP ǥ���� �ؽ�Ʈ
    
    // ��Ʋ �ý��� ����
    private BattleSystem battleSystem;

    // ===�ִϸ��̼� ���� ������Ʈ �� ����(�̵�����) ���� == 
    private Animator animator;            //�ִϸ����� ������Ʈ ����
    private Vector3 originalPosition;     //���ϸ��� ���� ��ġ ����
    private Quaternion originalRotation;  // �ʱ� ȸ���� ����
    private bool isAnimating = false;     //�ִϸ��̼� �ߺ� ���� ����

    //===���ϸ����� ����� �Ķ���� �̸� ��� ���� ===          // ����� ���� ������ �۾� ȯ�濡�� ��Ÿ ������ ���ϰ� ����
                                                             // ������ ����� �����Ͽ� ������ ���� ���� Ȯ�ο� ������
    private static readonly string ANI_ATTACK = "Attack";    //����
    private static readonly string ANI_IDLE = "Idle";        //���
    private static readonly string ANI_HIT = "Hit";          // �ǰ� �ִϸ��̼�
    void Start()
    {
        // ������ BattleSystem ã�Ƽ� ����
        battleSystem = FindObjectOfType<BattleSystem>();
        animator = GetComponent<Animator>(); //�ִϸ��̼� ������Ʈ ��������
        originalPosition = transform.position;   //������ġ ����
        originalRotation = transform.rotation;  // �ʱ� ȸ���� ����
        // �� ���ϸ��̸� ���� ��ư ��Ȱ��ȭ
        if (isEnemy)
        {
            selectButton.gameObject.SetActive(false);
        }

        UpdateHPDisplay(); // �ʱ� HP ǥ��
    }
    //��ü ���� �ִϸ��̼��� ����ϴ� �ڷ�ƾ
    public IEnumerator ManageAllAttacks(Poketmon target)
    {
        //�ִϸ��̼ǿ� �ߺ� ���� Ȯ��
        if(isAnimating) yield break;
        isAnimating = true;

        //Ÿ�� ��ġ ���
        Vector3 targetPos = target.transform.position;
        //Ÿ�� ��ġ���� �̵� �Ÿ����(��ġ�� �ʵ���)
        Vector3 attackPos = Vector3.Lerp(originalPosition, targetPos, 0.9f);

        //�̵��ð� ����
        float moveTime = 1f; //��ü �̵��ð�
        float elapsed = 0f;  //��� �ð� �ʱ�ȭ
                            
        // �ȱ� �ִϸ��̼� ���� 
        animator.SetBool("IsWalking", true);

        //�ε巯�� �̵��� ���� ���� ó�� �� Ÿ���� ���� ����
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;             //�� �����Ӹ��� ��� �ð� ���� 0~1����
            float progress = elapsed / moveTime;   //0���� 1 ������ ���൵ ���

            // progress�� ����Ͽ� �ε��� �̵� ���� 
            transform.position = Vector3.Lerp(originalPosition, attackPos, progress);
            yield return null; // ���� �����ӱ��� ���
        }

        // �ȱ� �ִϸ��̼� ����
        animator.SetBool("IsWalking", false);

        // �̵� �Ϸ� �� ���� �ִϸ��̼� ����
        animator.SetTrigger(ANI_ATTACK);
        //���� �ִϸ��̼� ��� �ð� ���
        yield return new WaitForSeconds(1f);
        target.TakeDamage(attackPower);
        // ������ ���� �ִϸ��̼� �Ϸ�
        yield return new WaitForSeconds(1f);

        // 180�� ȸ�� �ڵ���
        float rotateTime = 0.5f;
        //���� ��ġ�� ����     
        elapsed = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        // �ȱ� �ִϸ��̼� ���� (��ȯ)
        animator.SetBool("IsWalking", true);

        //�ε巯�� ȸ������
        while (elapsed < rotateTime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / rotateTime;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);
            yield return null;
        }
        elapsed = 0f;
        //�ڵ��Ƽ� ���·� ����ġ�� �ε巴�� �̵�
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / moveTime;
            transform.position = Vector3.Lerp(attackPos, originalPosition, progress);
            yield return null;
        }
        elapsed = 0f;
        startRotation = transform.rotation;
        targetRotation = originalRotation;
        //�ٽ� �������� ȸ��
        while (elapsed < rotateTime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / rotateTime;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);
            yield return null;
        }

        // �ȱ� �ִϸ��̼� ����
        animator.SetBool("IsWalking", false);
        //��� �ִϸ��̼� ���� �� ���� �ʱ�ȭ      
        animator.SetTrigger(ANI_IDLE);            //��� ���� �ִϸ��̼����� ��ȯ
        transform.position = originalPosition;    //��ġ �Ϻ��ϰ� ����
        transform.rotation = originalRotation;    //ȸ�� �Ϻ��ϰ� ����
        isAnimating =false;                       //�ִϸ��̼� ���� �÷��� ����
    }

    // �ǰ� �ִϸ��̼��� �����ϴ� �ڷ�ƾ
    public IEnumerator PlayHitAnimation()
    {
        animator.SetTrigger(ANI_HIT);
        // �ǰ� �ִϸ��̼� ��� �ð���ŭ ���
        yield return new WaitForSeconds(0.5f);
    }

    // ���ϸ� ���� ����/�Ұ��� ����
    public void EnableSelection(bool enable)
    {
        // �̹� �ൿ������ ���� �Ұ���
        selectButton.interactable = enable && !hasActed;
    }

    // Ÿ�� ��ư Ȱ��ȭ/��Ȱ��ȭ
    public void EnableTargetButton(bool enable)
    {
        targetButton.gameObject.SetActive(enable);
    }

    // ���ϸ� ���ý� ȣ��Ǵ� �Լ�
    public void OnSelect()
    {
        battleSystem.OnPoketmonSelect(this);
    }

    // Ÿ������ ���ý� ȣ��Ǵ� �Լ�
    public void OnTargetSelect()
    {
        battleSystem.OnTargetSelect(this);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        UpdateHPDisplay();
        StartCoroutine(PlayHitAnimation());
        Debug.Log($"{pokemonName}��(��) {damage} �������� �޾ҽ��ϴ�. ���� HP: {hp}");
    }

    // HP ǥ�� ������Ʈ
    void UpdateHPDisplay()
    {
        if (hpText != null)
        {
            hpText.text = $"HP: {hp}";
        }
    }
}


