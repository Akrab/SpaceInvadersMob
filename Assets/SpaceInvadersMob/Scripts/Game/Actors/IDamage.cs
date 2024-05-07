namespace SpaceInvadersMob.Game.Actors
{
    public interface IDamage
    {
        void Damage(float value);
    }

    public interface IDamageValue
    {
        float DamageValue { get; }
    }
}