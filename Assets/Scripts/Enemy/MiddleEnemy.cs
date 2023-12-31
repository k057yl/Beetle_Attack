public class MiddleEnemy : AbstractEnemy
{
    protected override int MaxHealth => Constants.FIVE_HUNDRED;
    protected override SoundType DeathSoundType => SoundType.DeadMiddle;
    protected override bool CanShoot()
    {
        return true;
    }
}
