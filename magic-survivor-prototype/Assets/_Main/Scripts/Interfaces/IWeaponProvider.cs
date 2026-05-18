using System.Collections.Generic;

/*
IWeaponProvider 는 무기 제공자 라는 역할에 대한 약속
게임 어디선가 이름으로 무기를 꺼내달라는 요청을 처리해줄 누군가가 필요하다.
그 누군가가 어떤 식으로 무기를 보관하는지 (List 인지 Dictionary 인지, 
미리 다 만들어두는지 동적으로 만드는지) 는 사용하는 쪽 입장에선 알 필요가 없다.

그래서 이름으로 IWeapon 을 돌려준다, 전체 목록을 줄 수 있다 라는 약속만
이 interface 로 잡아두고, 실제 구현 (WeaponProvider) 은 자유롭게 작성한다.
나중에 구현 방식을 바꿔도 (예: ScriptableObject 기반으로 갈아끼움) 사용하는 쪽
코드는 안 건드려도 된다.
*/
public interface IWeaponProvider
{
    public IReadOnlyList<IWeapon> AllWeapons { get; }
    public IWeapon NameToIWeapon(string weaponName);
}
