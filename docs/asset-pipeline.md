# Project-FM: 에셋 파이프라인 정의

**작성일:** 2025년 4월 11일  
**작성자:** SASHA  
**문서 상태:** 초안 (프로토타입 단계)
**참고 사항:** 이 문서는 에셋 관리 규칙 및 네이밍 컨벤션에 집중합니다. 폴더 구조의 기본 표준은 `project-structure.md` 문서를 참조하세요.

## 1. 개요

이 문서는 Project-FM의 에셋 파이프라인과 리소스 관리 방법을 정의합니다. 프로토타입 단계에서는 텍스트 기반 전투 시스템을 중심으로 필요한 최소한의 에셋 구조와 관리 방법을 설명합니다.

### 1.1 주요 에셋 유형

Project-FM에서는 다음과 같은 주요 에셋 유형을 사용합니다:

- **데이터 에셋** (ScriptableObject)
  - 메카닉 파츠 데이터
  - 파일럿 데이터
  - 전투 파라미터
  - 게임 설정

- **UI 에셋**
  - 텍스트 로그 UI 요소
  - 스탯 및 상태 표시 UI
  - 버튼 및 인터랙션 요소

- **개발 도구**
  - 프로토타이핑 테스트 씬
  - 디버깅 도구
  - Odin Inspector 커스텀 에디터

### 1.2 에셋 파이프라인 목표

- **간소화된 데이터 중심 접근법**: 프로토타입 단계에서는 ScriptableObject를 활용한 데이터 중심 설계
- **빠른 이터레이션**: 쉽게 수정하고 테스트할 수 있는 에셋 구조
- **확장성**: 향후 3D/그래픽 기반 시스템으로 전환 시 확장 가능한 구조
- **모듈성**: 독립적으로 개발하고 통합할 수 있는 에셋 모듈

## 2. 폴더 구조 및 네이밍 컨벤션

### 2.1 폴더 구조 참고

기본 폴더 구조는 `project-structure.md` 문서에 정의된 표준을 따릅니다. 이 섹션에서는 에셋 유형별로 추가적인 하위 폴더 구조만 정의합니다.

### 2.2 에셋 네이밍 컨벤션

#### 2.2.1 일반 규칙

- **PascalCase** 사용
- 접두사로 에셋 유형을 표시 (예: `SO_`, `UI_`, `PRF_`)
- 특수 문자 사용 금지
- 의미 있는 이름 사용 (예: `UI_BattleLog` 대신 `UI_BattleLogPanel`)

#### 2.2.2 에셋 유형별 접두사

| 에셋 유형 | 접두사 | 예시 |
|----------|-------|------|
| ScriptableObject | SO_ | SO_AssaultRifle, SO_DefensePilot |
| 프리팹 | PRF_ | PRF_LogEntry, PRF_StatDisplay |
| UI 스프라이트 | UI_ | UI_ButtonNormal, UI_PanelBackground |
| 씬 | SCN_ | SCN_PrototypeBattle, SCN_PartEditor |
| 애니메이션 | ANIM_ | ANIM_ButtonPress, ANIM_PanelFade |

#### 2.2.3 ScriptableObject 데이터 인스턴스 네이밍

```
SO_[카테고리]_[이름]_[변형]
```

예시:
- `SO_MechPart_AssaultRifle_v1`
- `SO_MechPart_AssaultRifle_v2`
- `SO_Pilot_Scout_Default`
- `SO_BattleParams_Standard`

## 3. ScriptableObject 데이터 관리

### 3.1 데이터 에셋 구조

프로토타입 단계에서는 ScriptableObject를 다음과 같이 구성합니다:

```
Data/
├── MechParts/
│   ├── Weapons/             # 무기 파츠
│   ├── Armor/               # 방어 파츠
│   ├── Mobility/            # 이동 파츠
│   ├── Special/             # 특수 파츠
│   └── DefaultSets/         # 기본 파츠 세트
├── Pilots/
│   ├── PlayerPilots/        # 플레이어 파일럿
│   └── EnemyPilots/         # 적 파일럿
├── BattleParameters/
│   ├── CombatRules/         # 전투 규칙
│   ├── DifficultySettings/  # 난이도 설정
│   └── BalanceProfiles/     # 밸런스 프로필
└── GameSettings/
    ├── Graphics/            # 그래픽 설정
    ├── Sound/               # 사운드 설정
    └── Gameplay/            # 게임플레이 설정
```

### 3.2 ScriptableObject 생성 메뉴

Unity 에디터에서 ScriptableObject를 쉽게 생성할 수 있도록 메뉴 설정:

```csharp
// 메카닉 파트 생성 메뉴 예시
[CreateAssetMenu(fileName = "SO_MechPart_New", menuName = "ProjectFM/MechPart/Generic")]
public class MechPartData : ScriptableObject, IMechPartData
{
    // 구현...
}

[CreateAssetMenu(fileName = "SO_MechPart_Weapon", menuName = "ProjectFM/MechPart/Weapon")]
public class WeaponPartData : MechPartData
{
    // 무기 특화 구현...
}
```

### 3.3 데이터 에셋 프리로딩

런타임에 데이터 에셋을 효율적으로 로드하기 위한 시스템 구현:

```csharp
// IDataAssetRepository 인터페이스 (데이터-플로우.md 문서와 일관성 유지)
public interface IDataAssetRepository<T> where T : ScriptableObject
{
    T GetById(string id);
    IReadOnlyList<T> GetAll();
    IReadOnlyList<T> FindByCategory(string category);
}

// 메카닉 파트 리포지토리 구현 예시
public class MechPartRepository : IDataAssetRepository<MechPartData>
{
    private Dictionary<string, MechPartData> _partsById = new Dictionary<string, MechPartData>();
    private Dictionary<string, List<MechPartData>> _partsByCategory = new Dictionary<string, List<MechPartData>>();

    public void LoadAllAssets()
    {
        // Resources.LoadAll 또는 주소록 기반 로드
        MechPartData[] allParts = Resources.LoadAll<MechPartData>("Data/MechParts");
        
        foreach (var part in allParts)
        {
            _partsById[part.PartID] = part;
            
            if (!_partsByCategory.ContainsKey(part.Category))
            {
                _partsByCategory[part.Category] = new List<MechPartData>();
            }
            
            _partsByCategory[part.Category].Add(part);
        }
    }
    
    // IDataAssetRepository 인터페이스 구현
}
```

## 4. UI 에셋 관리

### 4.1 텍스트 기반 UI 구조

프로토타입 단계에서는 최소한의 UI 요소를 사용합니다:

```
UI/
├── Prefabs/
│   ├── BattleLog/           # 전투 로그 UI
│   │   ├── PRF_LogPanel.prefab
│   │   ├── PRF_LogEntry.prefab
│   │   └── PRF_LogFilterButton.prefab
│   ├── StatusDisplay/       # 상태 표시 UI
│   │   ├── PRF_MechStatus.prefab
│   │   ├── PRF_PilotStatus.prefab
│   │   └── PRF_PartStatus.prefab
│   └── CommandInput/        # 명령 입력 UI
│       ├── PRF_CommandPanel.prefab
│       ├── PRF_ActionButton.prefab
│       └── PRF_TargetSelector.prefab
└── Sprites/
    ├── Backgrounds/         # 배경 스프라이트
    ├── Buttons/             # 버튼 스프라이트
    └── Icons/               # 아이콘 스프라이트
```

### 4.2 텍스트 스타일 설정

TextMesh Pro를 활용한 일관된 텍스트 스타일 정의:

```
TextStyles/
├── TMP_BattleLog.asset      # 전투 로그용 TMP 설정
├── TMP_CommandText.asset    # 명령어 텍스트용 TMP 설정
├── TMP_StatusNormal.asset   # 일반 상태 텍스트용 TMP 설정
├── TMP_StatusWarning.asset  # 경고 상태 텍스트용 TMP 설정
└── TMP_StatusCritical.asset # 치명적 상태 텍스트용 TMP 설정
```

### 4.3 UI 재사용 컴포넌트

재사용 가능한 UI 컴포넌트 설계:

```csharp
// UI 팩토리 인터페이스
public interface IUIFactory
{
    GameObject CreateLogEntry(string message, LogEntryType type);
    GameObject CreateStatusDisplay(IUnit unit);
    GameObject CreateActionButton(IAction action);
}

// UI 팩토리 구현
public class UIFactory : IUIFactory
{
    private GameObject _logEntryPrefab;
    private GameObject _statusDisplayPrefab;
    private GameObject _actionButtonPrefab;
    
    public UIFactory()
    {
        // 프리팹 로드
        _logEntryPrefab = Resources.Load<GameObject>("UI/Prefabs/BattleLog/PRF_LogEntry");
        _statusDisplayPrefab = Resources.Load<GameObject>("UI/Prefabs/StatusDisplay/PRF_MechStatus");
        _actionButtonPrefab = Resources.Load<GameObject>("UI/Prefabs/CommandInput/PRF_ActionButton");
    }
    
    // IUIFactory 인터페이스 구현
}
```

## 5. Odin Inspector를 활용한 프로토타이핑 도구

### 5.1 커스텀 인스펙터 및 에디터 윈도우

Odin Inspector를 활용하여 게임 데이터를 쉽게 편집하고 테스트할 수 있는 도구:

```csharp
// 메카닉 파트 에디터 윈도우 예시
[Serializable]
public class MechPartEditorSettings
{
    [TableList]
    public List<MechPartData> AvailableParts = new List<MechPartData>();
    
    [InlineEditor(InlineEditorModes.GUIAndPreview)]
    public MechPartData SelectedPart;
    
    [Button("Create New Part")]
    private void CreateNewPart()
    {
        // 새 파트 생성 로직
    }
}

// Odin 에디터 윈도우
public class MechPartEditorWindow : OdinEditorWindow
{
    [MenuItem("ProjectFM/Mech Part Editor")]
    private static void OpenWindow()
    {
        GetWindow<MechPartEditorWindow>().Show();
    }
    
    [SerializeField]
    private MechPartEditorSettings settings = new MechPartEditorSettings();
    
    // 윈도우 구현...
}
```

### 5.2 런타임 디버깅 도구

게임 플레이 중 데이터를 실시간으로 확인하고 수정할 수 있는 도구:

```csharp
// 디버그 매니저 인터페이스
public interface IDebugManager
{
    void ToggleDebugPanel();
    void SetUnitValues(IUnit unit, Dictionary<StatType, int> newValues);
    void SimulateBattle(IUnit unitA, IUnit unitB, int turns);
}

// 디버그 매니저 MonoBehaviour
[TypeInfoBox("Runtime debugging tools for ProjectFM")]
public class DebugManager : MonoBehaviour, IDebugManager
{
    [FoldoutGroup("Battle Simulation")]
    [TableList]
    public List<BattleSimulation> SavedSimulations = new List<BattleSimulation>();
    
    [Button("Run All Simulations")]
    private void RunAllSimulations()
    {
        // 시뮬레이션 실행 로직
    }
    
    // IDebugManager 인터페이스 구현
}
```

## 6. 에셋 임포트 설정

### 6.1 텍스처 및 스프라이트 임포트 설정

```
TextureImporter 설정:
- 스프라이트 압축: 프로토타입에서는 Compressed
- 최대 텍스처 크기: 프로토타입에서는 1024
- 밉맵 생성: 프로토타입에서는 Off
- 스프라이트 메시 타입: 프로토타입에서는 FullRect
```

### 6.2 TextMesh Pro 폰트 에셋 설정

```
- 글꼴 아틀라스 해상도: 1024x1024
- 글꼴 패딩: 5
- 글꼴 포인트 크기: 프로토타입에서는 기본 12pt
- 글꼴 스타일: Regular/Bold/Italic 필요에 따라 사용
```

### 6.3 프리팹 변형 관리

```
- 프리팹 변형 모드: 네스티드
- 프리팹 레이아웃: 변형 오버라이드 허용
- 프리팹 업데이트 정책: 자동 업데이트
```

## 7. 텍스트 기반 전투 시스템을 위한 최소 시각적 요소

### 7.1 로그 엔트리 스타일링

전투 로그 텍스트 스타일링을 위한 시각적 요소:

```csharp
// 로그 엔트리 시각적 정의
[Serializable]
public class LogEntryVisualSettings
{
    [BoxGroup("Text Style")]
    public TMP_FontAsset Font;
    
    [BoxGroup("Text Style")]
    public Color TextColor = Color.white;
    
    [BoxGroup("Text Style")]
    [Range(8, 24)]
    public int FontSize = 12;
    
    [BoxGroup("Background")]
    public Color BackgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);
    
    [BoxGroup("Background")]
    [Range(0, 10)]
    public float CornerRadius = 4f;
    
    [BoxGroup("Animation")]
    [Range(0.1f, 2f)]
    public float FadeInDuration = 0.3f;
    
    [BoxGroup("Animation")]
    [Range(0.1f, 2f)]
    public float FadeOutDuration = 0.5f;
}

// 로그 엔트리 타입별 스타일링
[CreateAssetMenu(fileName = "SO_LogVisualSettings", menuName = "ProjectFM/UI/LogVisualSettings")]
public class LogVisualSettingsAsset : ScriptableObject
{
    [TableList]
    public List<LogEntryTypeStyle> TypeStyles = new List<LogEntryTypeStyle>();
    
    [Serializable]
    public class LogEntryTypeStyle
    {
        public LogEntryType Type;
        [InlineProperty]
        public LogEntryVisualSettings VisualSettings;
    }
}
```

### 7.2 상태 표시 시각적 요소

유닛 및 파트 상태 표시를 위한 시각적 요소:

```csharp
// 상태 표시 바 스타일
[Serializable]
public class StatusBarStyle
{
    public Color FillColor = Color.green;
    public Color EmptyColor = Color.red;
    public Color BackgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
    
    [Range(0, 10)]
    public float CornerRadius = 2f;
    
    [Range(0, 1)]
    public float BorderWidth = 0.5f;
    
    public Color BorderColor = Color.black;
}

// 상태 효과 아이콘
[Serializable]
public class StatusEffectIcon
{
    public StatusEffectType EffectType;
    public Sprite IconSprite;
    public Color IconColor = Color.white;
    
    [Range(0.5f, 2f)]
    public float PulseSpeed = 1f;
    
    public bool UseAnimation = true;
}
```

## 8. 프로젝트 품질 관리

### 8.1 에셋 퀄리티 체크리스트

프로토타입 단계에서도 유지해야 할 최소한의 에셋 품질 기준:

- **네이밍 컨벤션**: 모든 에셋이 정의된 네이밍 규칙을 따르는지
- **폴더 구조**: 에셋이 올바른 폴더에 저장되어 있는지
- **의존성**: 불필요한 외부 의존성이 없는지
- **메타 데이터**: 모든 에셋이 올바른 메타데이터를 가지고 있는지
- **파일 크기**: 에셋이 적절한 크기를 유지하는지

### 8.2 에셋 버전 관리

```
- 데이터 에셋 버전 관리: _v1, _v2 등의 접미사 사용
- 메이저 변경 시 새 파일로 저장
- 레거시 에셋 'Legacy_' 접두사 추가
```

## 9. 향후 확장 계획

프로토타입 이후 단계에서 고려할 에셋 파이프라인 확장:

### 9.1 그래픽 요소 확장

```
- 메카닉 3D 모델 및 텍스처
- 파일럿 캐릭터 아트
- 전투 이펙트 및 파티클
- 환경 및 맵 그래픽
```

### 9.2 사운드 에셋 통합

```
- 무기 및 액션 효과음
- 배경 음악
- UI 사운드
- 보이스오버 및 대사
```

### 9.3 애니메이션 시스템

```
- 메카닉 장비 애니메이션
- 전투 애니메이션
- UI 전환 애니메이션
```

## 10. 프로토타입 단계 에셋 체크리스트

프로토타입 구현을 위한 필수 에셋 목록:

### 10.1 ScriptableObject 데이터 에셋

- [ ] 기본 메카닉 파트 세트 (최소 5종 이상)
- [ ] 테스트용 파일럿 프로필 (플레이어/적 각 2명)
- [ ] 기본 전투 규칙 파라미터
- [ ] 게임 설정 프로필

### 10.2 UI 프리팹

- [ ] 전투 로그 패널 및 엔트리
- [ ] 유닛 상태 표시 UI
- [ ] 파트 상태 및 데미지 UI
- [ ] 명령 선택 및 실행 UI

### 10.3 개발 도구

- [ ] 메카닉 파트 에디터
- [ ] 전투 시뮬레이터
- [ ] 디버그 콘솔 및 로거

---

**참고**: 이 문서는 프로토타입 단계의 에셋 파이프라인 설계를 설명합니다. 실제 개발 과정에서 필요에 따라 수정될 수 있습니다.