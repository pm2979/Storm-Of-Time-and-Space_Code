# Multi-Player Ship Management System (Unity & Photon)

이 프로젝트는 **Unity**와 **Photon PUN2**를 사용하여 개발된 멀티플레이어 환경에서의 핵심 게임 루프(아이템 획득/사용, 재화 관리, 스탯 강화)를 처리하는 아키텍처 설계 예시입니다.

## 🚀 Key Highlights
- **Data-Driven Design**: `ScriptableObject`를 활용하여 아이템 스탯 및 강화 테이블을 코드와 분리, 유지보수성과 기획 확장성을 극대화했습니다.
- **Decoupled Architecture**: `EventBus`를 활용하여 데이터와 UI 간의 결합도를 최소화했습니다.
- **Network Synchronization**: `Photon RPC`를 통해 모든 클라이언트 간의 상태(체력, 아이템 사용, 강화)를 동기화합니다.
- **Optimization**: `Update` 내 연산을 지양하고 이벤트 기반 업데이트를 통해 CPU 효율을 최적화했습니다.

---

## 🏗️ Architecture Analysis

### 1. Data-Driven Design (ScriptableObject)
- **ItemInfo.cs**: 아이템의 이름, 설명, 프리팹, 타입뿐만 아니라 `ItemStatModifier` 리스트를 통해 아이템이 미치는 영향(HP, Speed, Atk 등)을 에디터에서 유연하게 설정할 수 있습니다.
- **UpgradeTableSO.cs**: 강화 단계별 비용(Cost)과 수치(Value)를 테이블화하여 관리합니다. 이를 통해 복잡한 강화 로직 없이도 데이터 기반의 밸런싱이 가능합니다.

### 2. Centralized Context (Ship.cs)
`Ship` 클래스는 모든 서브 컴포넌트(`Inventory`, `CoinWallet`, `Condition` 등)를 소유하고 관리하는 루트 컨텍스트 역할을 수행합니다. `Awake` 시점에서 의존성을 주입하고 초기화합니다.

### 3. Interaction & Network Logic
- **InventoryController**: 사용자의 입력(Slot 이동, 아이템 사용)을 처리하며, `RPC_UseItem`을 통해 네트워크 상의 모든 플레이어에게 동일한 효과를 적용합니다.
- **ShopController**: `UpgradeTableSO`에서 현재 레벨에 맞는 데이터를 가져와 `CoinWallet`의 재화를 검증한 후, `ApplyStat`을 통해 선박의 성능을 실시간으로 강화합니다.

---

## 🛠️ Tech Stack
- **Engine**: Unity 2022.x+
- **Network**: Photon PUN2
- **Patterns**: Singleton, Observer (EventBus), Strategy (Item System), Data-Driven (ScriptableObject)

---

## 📝 Coding Convention & Quality
- **PascalCase** (Public), **_camelCase** (Private) 네이밍 컨벤션 준수
- **ScriptableObject**를 활용한 데이터와 로직의 분리
- **SerializeField**를 통한 캡슐화 유지 및 인스펙터 접근성 확보
- **Network Authority**: MasterClient를 통한 대미지 판정 및 재화 검증으로 데이터 무결성 유지
