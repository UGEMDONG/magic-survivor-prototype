using UnityEngine;

public interface IWeapon
{
    public Vector2 Position { get; }
    // 무기 이름. WeaponProvider 에서 이름으로 무기를 찾아낼 때 사용된다.
    public string WeaponName { get; }
    // 다짜고짜 초기화를 인터페이스도 추상도 아닌 클래스로 해서 놀랐을 수도 있는데
    // 지금 기획에서 무기를 들 건 아직 플레이어 밖에 없고, 적이 든다 해도 기능만 만들면 바로 쓸 수는 있음
    // 단지 WeaponHolder의 상속 구조 자체를 손 볼거면 바꿔야 하지만 기능 자체가 간단히 무기를 다 들고, 틱 호출하는 형태라 괜찮을듯.
    public void Initialize(WeaponHolder weaponHolder);
    public void Tick(float deltaTime);
    public void Upgrade();
}
