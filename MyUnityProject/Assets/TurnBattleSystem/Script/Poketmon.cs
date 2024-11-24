using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poketmon : MonoBehaviour
{
    // === 포켓몬 기본 정보 ===
    public string pokemonName;        // 포켓몬 이름
    public int hp = 100;             // 체력
    public int attackPower = 20;     // 공격력
    public bool hasActed = false;    // 이번 턴에 행동했는지 여부
    public bool isEnemy = false;     // true면 적 포켓몬

    // === UI 컴포넌트 참조 ===
    public Button selectButton;       // 포켓몬 선택용 버튼
    public Button targetButton;       // 타겟 선택용 버튼
    public Text hpText;               // HP 표시할 텍스트
    
    // 배틀 시스템 참조
    private BattleSystem battleSystem;

    // ===애니메이션 제어 컴포넌트 및 상태(이동관련) 변수 == 
    private Animator animator;            //애니메이터 컴포넌트 참조
    private Vector3 originalPosition;     //포켓몬의 최초 위치 저장
    private Quaternion originalRotation;  // 초기 회전값 저장
    private bool isAnimating = false;     //애니메이션 중복 실행 방지

    //===에니메이터 사용할 파라미터 이름 상수 정의 ===          // 상수로 정해 놓으면 작업 환경에서 오타 방지가 편하고 뭔가
                                                             // 문제가 생기면 컴파일에 오류가 떠서 오류 확인에 용의함
    private static readonly string ANI_ATTACK = "Attack";    //공격
    private static readonly string ANI_IDLE = "Idle";        //대기
    private static readonly string ANI_HIT = "Hit";          // 피격 애니메이션
    void Start()
    {
        // 씬에서 BattleSystem 찾아서 참조
        battleSystem = FindObjectOfType<BattleSystem>();
        animator = GetComponent<Animator>(); //애니메이션 컴포넌트 가져오기
        originalPosition = transform.position;   //시작위치 설정
        originalRotation = transform.rotation;  // 초기 회전값 저장
        // 적 포켓몬이면 선택 버튼 비활성화
        if (isEnemy)
        {
            selectButton.gameObject.SetActive(false);
        }

        UpdateHPDisplay(); // 초기 HP 표시
    }
    //전체 공격 애니메이션을 담당하는 코루틴
    public IEnumerator ManageAllAttacks(Poketmon target)
    {
        //애니메이션에 중복 상태 확인
        if(isAnimating) yield break;
        isAnimating = true;

        //타켓 위치 계산
        Vector3 targetPos = target.transform.position;
        //타켓 위치까지 이동 거리계산(겹치지 않도록)
        Vector3 attackPos = Vector3.Lerp(originalPosition, targetPos, 0.9f);

        //이동시간 설정
        float moveTime = 1f; //전체 이동시간
        float elapsed = 0f;  //경과 시간 초기화
                            
        // 걷기 애니메이션 시작 
        animator.SetBool("IsWalking", true);

        //부드러운 이동을 위한 보간 처리 및 타겟을 향해 전진
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;             //매 프레임마다 경과 시간 증가 0~1까지
            float progress = elapsed / moveTime;   //0에서 1 사이의 진행도 계산

            // progress를 사용하여 부드라운 이동 구현 
            transform.position = Vector3.Lerp(originalPosition, attackPos, progress);
            yield return null; // 다음 프레임까지 대기
        }

        // 걷기 애니메이션 종료
        animator.SetBool("IsWalking", false);

        // 이동 완료 후 공격 애니메이션 실행
        animator.SetTrigger(ANI_ATTACK);
        //공격 애니메이션 재생 시간 대기
        yield return new WaitForSeconds(1f);
        target.TakeDamage(attackPower);
        // 나머지 공격 애니메이션 완료
        yield return new WaitForSeconds(1f);

        // 180도 회전 뒤돌기
        float rotateTime = 0.5f;
        //원래 위치로 복귀     
        elapsed = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        // 걷기 애니메이션 시작 (귀환)
        animator.SetBool("IsWalking", true);

        //부드러운 회전실행
        while (elapsed < rotateTime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / rotateTime;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);
            yield return null;
        }
        elapsed = 0f;
        //뒤돌아선 상태로 원위치로 부드럽게 이동
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
        //다시 정면으로 회전
        while (elapsed < rotateTime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / rotateTime;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);
            yield return null;
        }

        // 걷기 애니메이션 종료
        animator.SetBool("IsWalking", false);
        //모든 애니메이션 종료 및 상태 초기화      
        animator.SetTrigger(ANI_IDLE);            //대기 상태 애니메이션으로 전환
        transform.position = originalPosition;    //위치 완벽하게 보정
        transform.rotation = originalRotation;    //회전 완벽하게 보정
        isAnimating =false;                       //애니메이션 상태 플래그 해제
    }

    // 피격 애니메이션을 실행하는 코루틴
    public IEnumerator PlayHitAnimation()
    {
        animator.SetTrigger(ANI_HIT);
        // 피격 애니메이션 재생 시간만큼 대기
        yield return new WaitForSeconds(0.5f);
    }

    // 포켓몬 선택 가능/불가능 설정
    public void EnableSelection(bool enable)
    {
        // 이미 행동했으면 선택 불가능
        selectButton.interactable = enable && !hasActed;
    }

    // 타겟 버튼 활성화/비활성화
    public void EnableTargetButton(bool enable)
    {
        targetButton.gameObject.SetActive(enable);
    }

    // 포켓몬 선택시 호출되는 함수
    public void OnSelect()
    {
        battleSystem.OnPoketmonSelect(this);
    }

    // 타겟으로 선택시 호출되는 함수
    public void OnTargetSelect()
    {
        battleSystem.OnTargetSelect(this);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        UpdateHPDisplay();
        StartCoroutine(PlayHitAnimation());
        Debug.Log($"{pokemonName}이(가) {damage} 데미지를 받았습니다. 현재 HP: {hp}");
    }

    // HP 표시 업데이트
    void UpdateHPDisplay()
    {
        if (hpText != null)
        {
            hpText.text = $"HP: {hp}";
        }
    }
}


