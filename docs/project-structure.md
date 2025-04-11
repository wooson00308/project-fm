# Project-FM: 유니티 프로젝트 구조 설계

**작성일:** 2025년 4월 11일  
**작성자:** SASHA  
**문서 상태:** 초안 (프로토타입 단계)

## 1. 프로젝트 개요

Project-FM은 메카닉 커스터마이징 요소가 핵심인 턴제 전략 게임입니다. 본 문서는 유니티 프로젝트의 구조, 폴더 조직, 네이밍 컨벤션에 대한 표준을 정의합니다.

## 2. 프로토타이핑 중심 초기 개발 단계

### 2.1 프로토타입 우선 접근법
초기 개발 단계에서는 완전한 구조 설계보다 핵심 기능의 빠른 프로토타이핑에 집중합니다. 이를 위해:

- **기능 우선 폴더링**: 초기에는 엄격한 폴더 구조보다 기능 중심의 간소화된 폴더 구조 사용
- **빠른 테스트**: 핵심 기능 검증을 위한 테스트 환경 우선 구축
- **확장성 준비**: 기본 구조를 유지하되, 향후 확장 가능한 방식으로 설계

### 2.2 Odin Inspector 활용
Odin Inspector를 활용하여 프로토타이핑 과정을 가속화합니다:

- **테스트 프레임워크**: Odin의 [Button], [ButtonGroup] 속성을 활용한 테스트 프레임워크 구축
- **에디터 확장**: 편리한 개발을 위한 커스텀 에디터 UI 구성
- **플러그인 활용**: 필요한 에셋과 플러그인을 통합하여 개발 효율성 향상

## 3. 프로젝트 구조

### 3.1 최상위 폴더 구조

```
Assets/
├── Art/                     # 모든 시각적 에셋
├── Audio/                   # 오디오 에셋
├── Packages/                # 써드파티 패키지 (Odin Inspector 포함)
├── Prefabs/                 # 프리팹 에셋
├── Prototypes/              # 프로토타입 기능 및 테스트
│   ├── MechaSystem/         # 메카닉 시스템 프로토타입
│   ├── BattleSystem/        # 전투 시스템 프로토타입
│   └── UITests/             # UI 테스트
├── Resources/               # 런타임에 로드되는 리소스
├── Scenes/                  # 씬 파일
├── ScriptableObjects/       # 게임 데이터 ScriptableObjects
├── Scripts/                 # C# 스크립트
├── Settings/                # 프로젝트 설정 파일
└── ThirdParty/              # 기타 써드파티 플러그인 및 에셋
```

### 3.2 Scripts 폴더 구조

인터페이스와 추상화 중심의 실용적인 구조:

```
Scripts/
├── Core/                       # 핵심 시스템 및 매니저
│   ├── Interfaces/             # 핵심 인터페이스
│   │   ├── IManager.cs         # 매니저 인터페이스
│   │   └── IGameState.cs       # 게임 상태 인터페이스
│   └── Abstract/               # 핵심 추상 클래스
│       └── ManagerBase.cs      # 매니저 추상 클래스
├── Mecha/                      # 메카닉 관련 스크립트
│   ├── Interfaces/             # 메카닉 인터페이스
│   │   ├── IMechaUnit.cs       # 메카닉 유닛 인터페이스
│   │   └── IMechaPart.cs       # 메카닉 파트 인터페이스
│   ├── Abstract/               # 메카닉 추상 클래스
│   │   ├── MechaUnitBase.cs    # 메카닉 유닛 추상 클래스
│   │   └── MechaPartBase.cs    # 메카닉 파트 추상 클래스
│   ├── Parts/                  # 파트 인터페이스 및 타입
│   │   ├── IBodyPart.cs        # 보디 파트 인터페이스
│   │   ├── IArmPart.cs         # 암즈 파트 인터페이스
│   │   └── ILegPart.cs         # 레그 파트 인터페이스
│   └── Customization/          # 커스터마이징 시스템 인터페이스
├── Battle/                     # 전투 시스템 스크립트
│   ├── Interfaces/             # 전투 인터페이스
│   │   ├── ITurnManager.cs     # 턴 관리 인터페이스
│   │   ├── IBattleGrid.cs      # 전투 그리드 인터페이스
│   │   └── IBattleAction.cs    # 전투 액션 인터페이스
│   ├── Abstract/               # 전투 추상 클래스
│   │   └── BattleActionBase.cs # 전투 액션 추상 클래스
│   ├── Actions/                # 전투 액션 인터페이스
│   └── Grid/                   # 전투 그리드 인터페이스
├── Pilots/                     # 파일럿 관련 스크립트
│   ├── Interfaces/             # 파일럿 인터페이스
│   │   ├── IPilot.cs           # 파일럿 인터페이스
│   │   └── IPilotSkill.cs      # 파일럿 스킬 인터페이스
│   └── Abstract/               # 파일럿 추상 클래스
│       └── PilotBase.cs        # 파일럿 추상 클래스
├── UI/                         # UI 관련 스크립트
│   ├── Interfaces/             # UI 인터페이스
│   │   ├── IUIPanel.cs         # UI 패널 인터페이스
│   │   └── IUIElement.cs       # UI 요소 인터페이스
│   └── Abstract/               # UI 추상 클래스
│       └── UIPanelBase.cs      # UI 패널 추상 클래스
├── Utils/                      # 유틸리티 스크립트
│   ├── Interfaces/             # 유틸리티 인터페이스
│   │   └── IPoolable.cs        # 오브젝트 풀링 인터페이스
│   └── Extensions/             # 확장 메서드
└── Editor/                     # 에디터 확장 스크립트
    └── OdinExtensions/         # Odin Inspector 확장
```

### 3.3 Prototypes 폴더 구조

프로토타입 테스트를 위한 구조:

```
Prototypes/
├── MechaSystem/                # 메카닉 시스템 프로토타입
│   ├── Scenes/                 # 테스트 씬
│   ├── Scripts/                # 프로토타입 스크립트
│   │   ├── Interfaces/         # 프로토타입 인터페이스
│   │   └── TestImplementations/# 테스트용 구현체
│   └── Prefabs/                # 테스트용 프리팹
├── BattleSystem/               # 전투 시스템 프로토타입
│   ├── Scenes/
│   ├── Scripts/
│   │   ├── Interfaces/         # 프로토타입 인터페이스
│   │   └── TestImplementations/# 테스트용 구현체
│   └── Prefabs/
└── UITests/                    # UI 테스트
    ├── Scenes/
    └── Scripts/
        ├── Interfaces/         # UI 테스트 인터페이스
        └── TestImplementations/# 테스트용 UI 구현체
```

## 4. Odin Inspector 활용 가이드

### 4.1 인터페이스 기반 프로토타입 속성 사용법

```csharp
// 메카닉 파트 테스트 컴포넌트 (인터페이스 기반)
public class MechaPartTester : MonoBehaviour
{
    [BoxGroup("파트 정보")]
    public string partName;
    
    [BoxGroup("파트 정보")]
    [ValueDropdown("partTypes")]
    public string partType;
    
    [BoxGroup("파트 정보")]
    [Range(1, 100)]
    [OnValueChanged("UpdateDurability")]
    public int maxDurability = 100;
    
    [BoxGroup("파트 정보")]
    [ProgressBar(0, "maxDurability")]
    [ReadOnly]
    public int currentDurability = 100;
    
    [TabGroup("스탯", "공격력")]
    [Range(1, 50)]
    public int attackPower = 10;
    
    [TabGroup("스탯", "방어력")]
    [Range(1, 50)]
    public int defensePower = 10;
    
    [TabGroup("스탯", "이동력")]
    [Range(1, 20)]
    public int mobility = 5;
    
    [FoldoutGroup("내부 부품")]
    [HideLabel]
    [InlineEditor]
    public IInternalComponent internalComponent;
    
    private string[] partTypes = new string[] {
        "Body", "LeftArm", "RightArm", "Legs", "Backpack"
    };
    
    private void UpdateDurability()
    {
        currentDurability = maxDurability;
    }
    
    [Button("파트 초기화")]
    private void ResetPart()
    {
        currentDurability = maxDurability;
    }
    
    [Button("데미지 적용")]
    private void ApplyDamage(int amount)
    {
        currentDurability = Mathf.Max(0, currentDurability - amount);
        if (currentDurability == 0)
        {
            Debug.Log($"{partName} 파트가 파괴되었습니다!");
            // 인터페이스 메서드 호출을 통한 이벤트 핸들링
            if (internalComponent != null)
                internalComponent.OnDestruction();
        }
    }
}

// 내부 부품 인터페이스
public interface IInternalComponent
{
    string ComponentName { get; }
    void OnDestruction();
    void OnRepair();
}
```

### 4.2 인터페이스 기반 그룹화 및 버튼 테스트

```csharp
// 전투 테스트 매니저 (인터페이스 기반)
public class BattleTestManager : MonoBehaviour
{
    [BoxGroup("시뮬레이션 설정")]
    [Range(1, 10)]
    public int simulationRounds = 3;
    
    [BoxGroup("시뮬레이션 설정")]
    public bool includeRandomness = true;
    
    [FoldoutGroup("아군 유닛", expanded: true)]
    [ListDrawerSettings(ShowItemCount = true, ShowIndexLabels = true)]
    public List<IMechaUnit> playerUnits = new List<IMechaUnit>();
    
    [FoldoutGroup("적군 유닛")]
    [ListDrawerSettings(ShowItemCount = true, ShowIndexLabels = true)]
    public List<IMechaUnit> enemyUnits = new List<IMechaUnit>();
    
    [SerializeReference, BoxGroup("전투 시스템")]
    public ITurnManager turnManager;
    
    [SerializeReference, BoxGroup("전투 시스템")]
    public IBattleGrid battleGrid;
    
    [HorizontalGroup("테스트 버튼", Width = 0.5f)]
    [Button("전투 시작", ButtonSizes.Large)]
    [GUIColor(0, 0.8f, 0)]
    private void StartBattle()
    {
        Debug.Log("전투 시뮬레이션을 시작합니다...");
        // 인터페이스를 통한 호출
        if (turnManager != null)
            turnManager.StartBattle();
    }
    
    [HorizontalGroup("테스트 버튼")]
    [Button("테스트 리셋", ButtonSizes.Large)]
    [GUIColor(0.8f, 0, 0)]
    private void ResetTest()
    {
        Debug.Log("테스트 설정을 초기화합니다...");
        // 인터페이스를 통한 호출
        if (turnManager != null)
            turnManager.ResetBattle();
    }
    
    [ButtonGroup("턴 컨트롤")]
    [Button("다음 턴")]
    [EnableIf("IsBattleActive")]
    private void NextTurn()
    {
        Debug.Log("다음 턴으로 진행합니다...");
        // 인터페이스를 통한 호출
        if (turnManager != null)
            turnManager.AdvanceTurn();
    }
    
    [ButtonGroup("턴 컨트롤")]
    [Button("이전 턴")]
    [EnableIf("CanRewindTurn")]
    private void PreviousTurn()
    {
        Debug.Log("이전 턴으로 돌아갑니다...");
        // 인터페이스를 통한 호출
        if (turnManager != null)
            turnManager.RewindTurn();
    }
    
    private bool IsBattleActive()
    {
        return turnManager != null && turnManager.IsBattleActive;
    }
    
    private bool CanRewindTurn()
    {
        return turnManager != null && turnManager.CanRewind;
    }
}

// 턴 관리자 인터페이스
public interface ITurnManager
{
    bool IsBattleActive { get; }
    bool CanRewind { get; }
    void StartBattle();
    void ResetBattle();
    void AdvanceTurn();
    void RewindTurn();
}

// 전투 그리드 인터페이스
public interface IBattleGrid
{
    int Width { get; }
    int Height { get; }
    void InitializeGrid();
    bool IsPositionValid(Vector2Int position);
}
```

## 5. 코드 컨벤션

### 5.1 파일 네이밍

* **인터페이스**: I접두사 + PascalCase, 기능 설명
  * 예: `IMechaUnit.cs`, `IBattleGrid.cs`

* **추상 클래스**: PascalCase + Base, 기능 설명
  * 예: `MechaUnitBase.cs`, `BattleManagerBase.cs`

* **테스트 스크립트**: PascalCase, Test + 기능설명
  * 예: `TestMechaAssembly.cs`, `TestBattleSequence.cs`

* **에디터 스크립트**: PascalCase, 대상 + Editor
  * 예: `MechaUnitEditor.cs`, `BattleGridEditor.cs`

### 5.2 변수 네이밍

* **인터페이스 속성**: PascalCase
  * 예: `string UnitName { get; }`

* **private 변수**: camelCase, 밑줄(_) 접두사
  * 예: `private int _currentHealth;`

* **상수**: 대문자 스네이크 케이스(UPPER_SNAKE_CASE)
  * 예: `public const int MAX_PARTS = 5;`

### 5.3 함수 네이밍

* **인터페이스 메소드**: PascalCase, 동사 + 목적어
  * 예: `void Initialize();`, `int CalculateDamage();`

* **private 함수**: camelCase
  * 예: `private void updateStats()`, `private bool checkDestructionStatus()`

### 5.4 네임스페이스 구조

```csharp
namespace ProjectFM.Core.Interfaces
{
    public interface IManager
    {
        // 인터페이스 내용
    }
}

namespace ProjectFM.Mecha.Interfaces
{
    public interface IMechaUnit
    {
        // 인터페이스 내용
    }
}
```

## 6. 프로토타입 테스트 계획

### 6.1 핵심 테스트 영역
1. 메카닉 파트 조립 및 분리 메커니즘
2. 부위별 데미지 적용 및 파괴 효과
3. 턴 기반 전투 시스템 기본 흐름
4. 파일럿 능력치와 메카닉 성능 연동

### 6.2 테스트 씬 구성
- `PartAssemblyTest.unity`: 메카닉 파트 조립 테스트
- `BattleSystemTest.unity`: 간소화된 전투 시스템 테스트
- `PilotSkillTest.unity`: 파일럿 스킬 효과 테스트

## 7. 향후 구조 확장 계획

현재의 프로토타입 구조는 초기 개발과 테스트에 최적화되어 있습니다. 향후 프로젝트가 성장함에 따라:

1. 더 복잡한 인터페이스 계층 구조 도입
2. 컴포넌트 간 의존성 주입 패턴 적용
3. 자동화된 테스트 프레임워크 통합
4. 모듈식 서브시스템 인터페이스 설계

---

## 부록: 인터페이스 관계 다이어그램

```
[IMechaUnit] ◄── [IMechaPart]
    │                 ▲
    │                 │
    │            ┌────┴────┬────────┬────────┐
    │            │         │        │        │
    │     [IBodyPart] [IArmPart] [ILegPart] [IBackpackPart]
    │
    ├──► [IPilot] ◄── [IPilotSkill]
    │
    └──► [IWeaponSystem] ◄── [IWeapon]
```

**참고**: 이 문서는 프로토타입 단계의 지침이며, 개발 진행에 따라 업데이트될 수 있습니다.