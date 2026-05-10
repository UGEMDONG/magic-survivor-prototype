using System.Collections.Generic;
using UnityEngine;

// 현재 기획 상 플레이어가 하는게 단순 움직임과 간단 공격밖에 없긴 하지만 계속해서 기능이 추가될 것이고
// 또 굳이 플레이어가 무기를 모두 가지고 호출할 이유는 없다고 생각했음. 그리고 분리된 스크립트 또한 플레이어 상속 구조에 포함될 필요도 없을듯(반박시 대머리)
// 따라서 무기 가방같은 집합 역할 &간단한 틱 호출을 하고 결정적으로 모든 무기를 처음부터 참조하는 것이 가장 큰 메리트인 클래스 WeaponHolder

public class WeaponHolder : MonoBehaviour, IWeaponReceiver
{
    [SerializeField] WeaponProvider weaponProvider;
    // 더 간단해진 인터페이스이지만 빛을 발하는 순간: 플레이어에 있던 무기를 홀더가 참조하게 옮기는 동시에 몇몇 메서드들을 삭제/수정 했는데
    // 결론적으로 심플해졌다고 보면 됨. "누가" 초반 기능을 잘 짜놔서 수정할건 별로 없었고 살짝 내 철학이지만
    // 아무리 인터페이스더라도 외부와의 참조를 낮춰야 된다고 생각함(프로퍼티 포함) 프로퍼티는 굳이? 라고 할 수 있지만 애초에 굳이 필요한 상황을 없에서 결합도를 낮추는 패턴.
    // 인터페이스가 뭐 얼마나 간단해져야 하는지 감도 안옴. 차라리 없에지;; 라고 한다면 
    private readonly List<IWeapon> weapons = new();

    public List<IWeapon> Weapons => weapons;
    public Vector2 MoveDirection { get; private set; } = Vector2.up;

    IReadOnlyList<IWeapon> IWeaponReceiver.Weapons => null;//throw new System.NotImplementedException();

    public void AddWeapon(string weaponName)
    {
        var weapon = weaponProvider.NameToIWeapon(weaponName);

        if(weapon == null)
        {
            Debug.LogWarning("이름에 맞는 무기가 없습니다!!");
            return;
        }

        weapon.Initialize(this);
        weapons.Add(weapon);
    }
    public void SetMoveDirection(Vector2 moveDir)
    {
        if(moveDir.sqrMagnitude > 0.001f)
        {
            MoveDirection = moveDir;
        }
    }

    void Start()
    {
        AddWeapon("Bow");
        AddWeapon("Burning");
    }
    void Update()
    {
        foreach (IWeapon weapon in Weapons)
            weapon.Tick(Time.deltaTime);
    }

    public void ReceiveWeapon(IWeapon weapon)
    {
        throw new System.NotImplementedException();
    }
}
