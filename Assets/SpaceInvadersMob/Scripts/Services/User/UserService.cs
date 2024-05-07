namespace SpaceInvadersMob.Services
{
    public class UserService : IUserService
    {
        public int Currency { get; private set; }

        public UserService()
        {
            Currency = 1000;
        }

        public void AddCurrency(int delta)
        {
            Currency += delta;
        }

        public void ReduceCurrency(int delta)
        {
            Currency -= delta;
        }

        public bool HasCurrency(int amount)
        {
            return Currency >= amount;
        }
    }
}
