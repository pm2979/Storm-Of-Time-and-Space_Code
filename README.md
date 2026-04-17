# Multi-Player Ship Management System (Unity & Photon)

이 프로젝트는 **Unity**와 **Photon PUN2**를 사용하여 개발된 멀티플레이어 환경에서의 핵심 게임 루프(아이템 획득/사용, 재화 관리, 스탯 강화)를 처리하는 아키텍처 설계 예시입니다.

## 🚀 Key Highlights
- **Decoupled Architecture**: `EventBus`를 활용하여 데이터와 UI 간의 결합도를 최소화했습니다.
- **Network Synchronization**: `Photon RPC`를 통해 모든 클라이언트 간의 상태(체력, 아이템 사용, 강화)를 동기화합니다.
- **Extensible Item System**: 추상 클래스(`ItemData`)와 다형성을 활용하여 소모품, 재화 등 다양한 아이템 타입을 쉽게 확장할 수 있도록 설계했습니다.
- **Optimization**: `Update` 메서드 내 불필요한 연산을 지양하고, 캐싱 및 이벤트 기반 업데이트를 통해 CPU 효율을 높였습니다.

---

## 🏗️ Architecture Analysis

### 1. Centralized Context (Ship.cs)
`Ship` 클래스는 모든 서브 컴포넌트(`Inventory`, `CoinWallet`, `Condition` 등)를 소유하고 관리하는 루트 컨텍스트 역할을 수행합니다. `Awake` 시점에서 의존성을 주입하고 초기화합니다.

### 2. State & Data Management
- **Inventory & CoinWallet**: 순수 C# 클래스로 작성되어 데이터 로직에 집중하며, 상태 변화 발생 시 `EventBus`를 통해 UI나 타 시스템에 알립니다.
- **ShipCondition**: 선박의 체력과 생존 상태를 관리하며, `MasterClient` 기반의 대미지 판정 로직으로 데이터 무결성을 유지합니다.

### 3. Interaction & Network Logic
- **BaseInteractionController**: 공통 인터렉션 로직을 추상화하여 확장성을 확보했습니다.
- **InventoryController**: 사용자의 입력(Slot 이동, 아이템 사용)을 처리하며, `RPC_UseItem`을 통해 네트워크 상의 모든 플레이어에게 동일한 효과를 적용합니다.
- **ShopController**: `ScriptableObject` 기반의 강화 테이블을 사용하여 데이터 관리가 용이하며, 재화 소모 및 능력치 적용(ApplyStat)을 실시간으로 처리합니다.

---

## 🛠️ Tech Stack
- **Engine**: Unity 2022.3.17f1
- **Network**: Photon PUN2
- **Language**: C#
- **Patterns**: Singleton (GameManager), Observer (EventBus), Strategy (Item System)

---

## 📝 Coding Convention & Quality
- **PascalCase** (Public), **_camelCase** (Private) 네이밍 컨벤션 준수
- **ScriptableObject**를 활용한 데이터와 로직의 분리
- **SerializeField**를 통한 캡슐화 유지 및 인스펙터 접근성 확보
- **TrySpendCoin**과 같은 패턴을 통한 예외 처리 및 로직 안정성 강화
