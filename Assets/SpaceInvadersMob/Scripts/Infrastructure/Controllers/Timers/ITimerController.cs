namespace SpaceInvadersMob.Infrastructure.Controllers.Timers
{
    public interface ITimerController
    {
        bool AddTimer(ITimer newTimer);
        void RemoveTimer(ITimer target);
    }
}