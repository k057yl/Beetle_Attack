public class EasyEnemy : AbstractEnemy
{
    protected override int MaxHealth => Constants.ONE_HUNDRED;
    protected override SoundType DeathSoundType => SoundType.DeadEasy;
}
