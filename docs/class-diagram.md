# Project-FM: 클래스 다이어그램

**작성일:** 2025년 4월 11일  
**작성자:** SASHA  
**문서 상태:** 초안 (프로토타입 단계)
**참고 사항:** 이 문서는 프로젝트의 주요 클래스 및 인터페이스 관계에 집중합니다. 폴더 구조는 `project-structure.md` 문서를 참조하세요.

## 1. 다이어그램 개요

이 문서는 Project-FM의 주요 인터페이스와 클래스 간의 관계를 다이어그램으로 시각화합니다. 프로젝트의 아키텍처(`architecture.md`)와 데이터 흐름(`data-flow.md`) 문서를 기반으로 작성되었으며, 폴더 구조는 `project-structure.md`에 정의된 표준을 따릅니다.

## 2. 클래스 구조 표기법

이 문서에서는 클래스 다이어그램을 표현하기 위해 다음과 같은 표기법을 사용합니다:

```
+-------------------------+
| 클래스/인터페이스 이름   |
+-------------------------+
| + 공개 필드/프로퍼티     |
| # 보호 필드/프로퍼티     |
| - 비공개 필드/프로퍼티   |
+-------------------------+
| + 공개 메서드()          |
| # 보호 메서드()          |
| - 비공개 메서드()        |
+-------------------------+
```

관계 표현:
- 상속/구현: ────▷
- 합성: ────♦
- 집합: ────◇
- 사용: ────→

## 3. 핵심 시스템 클래스 다이어그램

### 3.1 서비스 로케이터 및 구조적 패턴 (Core 네임스페이스)

```
+----------------------+       +-------------------+
| ServiceLocator       |<>-----| IService          |
+----------------------+       +-------------------+
| - services           |       | + Initialize()    |
+----------------------+       +-------------------+
| + Register<T>()      |              ▲
| + Resolve<T>()       |              |
| + Initialize()       |       +------+------+
+----------------------+       |             |
                              |             |
                     +---------------+ +---------------+
                     | GameManager   | | UIManager     |
                     +---------------+ +---------------+
                     | + Initialize()| | + Initialize()|
                     +---------------+ +---------------+
```

네임스페이스 구조:
```csharp
namespace ProjectFM.Core.Interfaces
{
    public interface IService
    {
        void Initialize();
    }
}

namespace ProjectFM.Core
{
    public class ServiceLocator
    {
        // 구현
    }
}
```

### 3.2 메카닉 시스템 클래스 다이어그램 (Mecha 네임스페이스)

```
                   +----------------------+
                   | IMechaUnit           |
                   +----------------------+
                   | + UnitName           |
                   | + Level              |
                   | + IsDestroyed        |
                   | + Body, Arms, Legs   |
                   | + CurrentPilot       |
                   +----------------------+
                   | + Initialize()       |
                   | + EquipPart()        |
                   | + AssignPilot()      |
                   | + CalculateStats()   |
                   | + TakeDamage()       |
                   +----------------------+
                            ▲
                            |
                    +---------------+
                    | MechaUnitBase |
                    +---------------+
                    | - unitName    |
                    | - level       |
                    | - parts       |
                    | - pilot       |
                    +---------------+
                    | # OnPartDamage|
                    +---------------+
                            |
                            ◇
                            |
+----------------------+    |    +------------------------+
| IMechaPart           |<---+----| IPilot                 |
+----------------------+         +------------------------+
| + PartName           |         | + PilotName            |
| + Type               |         | + Level                |
| + Durability         |         | + Stats                |
| + IsDestroyed        |         | + Skills               |
| + StatModifiers      |         +------------------------+
+----------------------+         | + Initialize()         |
| + Initialize()       |         | + CalculateStats()     |
| + TakeDamage()       |         | + ApplySkill()         |
| + OnDestruction()    |         +------------------------+
| + Repair()           |
+----------------------+
           ▲
           |
  +----------------+
  | IWeaponSystem  |
  +----------------+
  | + WeaponType   |
  | + Damage       |
  | + Range        |
  +----------------+
  | + Fire()       |
  +----------------+
```

네임스페이스 구조:
```csharp
namespace ProjectFM.Mecha.Interfaces
{
    public interface IMechaUnit
    {
        // 구현
    }
    
    public interface IMechaPart
    {
        // 구현
    }
}

namespace ProjectFM.Mecha.Abstract
{
    public abstract class MechaUnitBase : IMechaUnit
    {
        // 구현
    }
}
```

### 3.3 데이터 관리 클래스 다이어그램 (ScriptableObjects 구조)

```
+------------------+     +------------------+     +------------------+
| IMechaPartData   |     | IPilotData       |     | IWeaponData      |
+------------------+     +------------------+     +------------------+
| + PartID         |     | + PilotID        |     | + WeaponID       |
| + PartName       |     | + PilotName      |     | + WeaponName     |
| + Type           |     | + Class          |     | + WeaponType     |
| + MaxDurability  |     | + BaseLevel      |     | + BaseDamage     |
| + StatModifiers  |     | + BaseStats      |     | + StatRequirements|
+------------------+     | + Skills         |     +------------------+
                         +------------------+
                                  ▲
                                  |
                           [ScriptableObject]
                                  |
                         +------------------+
                         | DataRepository   |
                         +------------------+
                         | + MechaParts     |
                         | + Pilots         |
                         | + Weapons        |
                         +------------------+
                         | + LoadAll()      |
                         | + GetPartByID()  |
                         | + GetPilotByID() |
                         +------------------+
```

ScriptableObject 구현:
```csharp
// project-structure.md에 정의된 네이밍 컨벤션을 따름
namespace ProjectFM.Data
{
    [CreateAssetMenu(fileName = "NewMechaPart", menuName = "ProjectFM/MechaPart")]
    public class MechaPartData : ScriptableObject, IMechaPartData
    {
        [SerializeField] private string _partID;
        [SerializeField] private string _partName;
        
        // IMechaPartData 인터페이스 구현
        public string PartID => _partID;
        public string PartName => _partName;
        // 추가 구현...
    }
}
```

### 3.4 전투 시스템 클래스 다이어그램 (Battle 네임스페이스)

```
+----------------+     +----------------+     +----------------+
| IBattleSystem  |<>---| ITurnManager   |<>---| IActionSystem  |
+----------------+     +----------------+     +----------------+
| + Initialize() |     | + StartTurn()  |     | + Execute()    |
| + StartBattle()|     | + EndTurn()    |     | + CalculateHit()|
| + EndBattle()  |     | + NextActor()  |     +----------------+
+----------------+     +----------------+
        |                     |                      |
        ◇                     ◇                      ◇
        |                     |                      |
+----------------+     +----------------+     +----------------+
| IBattleLogger  |<>---| IUnitAction    |<>---| IBattleResult  |
+----------------+     +----------------+     +----------------+
| + LogAction()  |     | + Execute()    |     | + ApplyResult()|
| + GetLogEntry()|     | + CanExecute() |     | + IsVictory    |
+----------------+     +----------------+     +----------------+
        |
        ◇
        |
+----------------+
| ITextLogView   |
+----------------+
| + AppendLog()  |
| + ClearLogs()  |
| + ScrollToEnd()|
+----------------+
```

전투 시스템 네임스페이스:
```csharp
namespace ProjectFM.Battle.Interfaces
{
    public interface ITurnManager
    {
        bool IsBattleActive { get; }
        bool CanRewind { get; }
        void StartBattle();
        void ResetBattle();
        void AdvanceTurn();
        void RewindTurn();
    }
    
    public interface IBattleSystem
    {
        // 구현
    }
}
```

### 3.5 UI 시스템 클래스 다이어그램 (UI 네임스페이스)

```
+----------------+     +----------------+     +----------------+
| IUIManager     |<>---| IUIPanel       |<>---| IUIElement    |
+----------------+     +----------------+     +----------------+
| + OpenPanel()  |     | + Initialize() |     | + Initialize() |
| + ClosePanel() |     | + Open()       |     | + Refresh()    |
| + GetPanel()   |     | + Close()      |     | + GetValue()   |
+----------------+     | + Refresh()    |     | + SetValue()   |
                       +----------------+     +----------------+
                               ▲                      ▲
                               |                      |
                       +--------------+       +--------------+
                       | MechaPanel   |       | TextLogPanel |
                       +--------------+       +--------------+
                       | + ShowMecha()|       | + AppendLog()|
                       +--------------+       +--------------+
```

UI 시스템 네임스페이스:
```csharp
namespace ProjectFM.UI.Interfaces
{
    public interface IUIPanel
    {
        void Initialize();
        void Open();
        void Close();
        void Refresh();
    }
    
    public interface IUIElement
    {
        void Initialize();
        void Refresh();
        object GetValue();
        void SetValue(object value);
    }
}
```

### 3.6 저장 시스템 클래스 다이어그램 (Utils 네임스페이스)

```
+----------------+     +----------------+     +----------------+
| ISaveSystem    |<>---| ISaveData      |<>---| ISaveDataItem  |
+----------------+     +----------------+     +----------------+
| + SaveGame()   |     | + GameVersion  |     | + Serialize()  |
| + LoadGame()   |     | + SaveDate     |     | + Deserialize()|
| + GetSaveList()|     | + PlayerName   |     +----------------+
+----------------+     +----------------+           ▲
                       | + Serialize()  |           |
                       | + Deserialize()|      +--------------+
                       +----------------+      | IMechaState  |
                             ▲                 +--------------+
                             |                 | + MechaID    |
                      +--------------+         | + Parts      |
                      | GameSaveData |         | + Pilot      |
                      +--------------+         +--------------+
                      | - _mechaUnits |
                      | - _pilots     |
                      | - _inventory  |
                      +--------------+
```

저장 시스템 네임스페이스:
```csharp
namespace ProjectFM.Utils.Interfaces
{
    public interface ISaveSystem
    {
        void SaveGame(string saveName);
        bool LoadGame(string saveName);
        string[] GetSaveList();
    }
}
```

## 4. Scripts 패키지 구조 다이어그램

```
+---------------------+     +---------------------+     +---------------------+
| Core                |<>---| Data                |<>---| UI                  |
+---------------------+     +---------------------+     +---------------------+
| - ServiceLocator    |     | - ScriptableObjects |     | - UIManager         |
| - EventSystem       |     | - DataRepository    |     | - UIPanels          |
| - GameManager       |     | - SaveSystem        |     | - UIElements        |
| - InputManager      |     +---------------------+     +---------------------+
+---------------------+                |
          |                            |
          |                            |
          ▼                            ▼
+---------------------+     +---------------------+     +---------------------+
| Mecha               |<>---| Battle              |<>---| TextLog             |
+---------------------+     +---------------------+     +---------------------+
| - IMechaUnit        |     | - IBattleSystem     |     | - ITextLogGenerator |
| - IMechaPart        |     | - ITurnManager      |     | - ITextLogEntry     |
| - IPilot            |     | - IActionSystem     |     | - TextLogPanel      |
+---------------------+     +---------------------+     +---------------------+
```

이 패키지 구조는 `project-structure.md`에 정의된 Scripts 폴더 구조를 따릅니다:

```
Scripts/
├── Core/
│   ├── Interfaces/
│   └── Abstract/
├── Mecha/
│   ├── Interfaces/
│   ├── Abstract/
│   ├── Parts/
│   └── Customization/
├── Battle/
│   ├── Interfaces/
│   ├── Abstract/
│   ├── Actions/
│   └── Grid/
├── Pilots/
│   ├── Interfaces/
│   └── Abstract/
├── UI/
│   ├── Interfaces/
│   └── Abstract/
└── Utils/
    ├── Interfaces/
    └── Extensions/
```

## 5. 인터페이스 관계 요약

이 다이어그램들은 Project-FM의 주요 인터페이스와 클래스 간의 관계를 나타냅니다. 주요 특징:

1. **인터페이스 기반 디자인**: 모든 주요 시스템은 인터페이스를 통해 정의되어 느슨한 결합 유지
2. **서비스 로케이터 패턴**: 주요 서비스를 등록하고 해결하기 위한 중앙 접근점 제공
3. **컴포지션 우선**: 상속보다 합성을 선호하여 시스템 간의 유연한 관계 구축
4. **ScriptableObject 데이터 관리**: 데이터는 ScriptableObject를 기반으로 관리되어 데이터와 로직 분리
5. **이벤트 기반 통신**: 직접적인 참조를 최소화하고 이벤트를 통한 느슨한 통신 지향

## 6. 클래스 구현 지침

다이어그램의 인터페이스를 구현할 때 `project-structure.md`에 정의된 다음 지침을 따라야 합니다:

### 6.1 파일 네이밍

* **인터페이스**: I접두사 + PascalCase, 기능 설명
  * 예: `IMechaUnit.cs`, `IBattleGrid.cs`

* **추상 클래스**: PascalCase + Base, 기능 설명
  * 예: `MechaUnitBase.cs`, `BattleManagerBase.cs`

### 6.2 변수 네이밍

* **인터페이스 속성**: PascalCase
  * 예: `string UnitName { get; }`

* **private 변수**: camelCase, 밑줄(_) 접두사
  * 예: `private int _currentHealth;`

* **상수**: 대문자 스네이크 케이스(UPPER_SNAKE_CASE)
  * 예: `public const int MAX_PARTS = 5;`

### 6.3 네임스페이스 구조

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

이 설계 원칙을 따르면 유지보수하기 쉽고 확장 가능한 코드베이스를 구축할 수 있습니다. 