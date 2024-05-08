using SpaceInvadersMob.Game.Actors.Enemy;

namespace SpaceInvadersMob.Infrastructure
{
    public class MessageKillEnemy
    {
        public EnemyType Data { get; private set; }

        private MessageKillEnemy(EnemyType data)
        {
            this.Data = data;
        }

        public static MessageKillEnemy Create(
            EnemyType data)
        {
            return new MessageKillEnemy(data);
        }
    }

    public class MessageOnPauseGame
    {
        public bool Data { get; private set; }

        private MessageOnPauseGame(bool data)
        {
            this.Data = data;
        }

        public static MessageOnPauseGame Create(
            bool data)
        {
            return new MessageOnPauseGame(data);
        }
    }
    
    public class MessageRestartGame
    {
        private MessageRestartGame()
        {

        }

        public static MessageRestartGame Create()
        {
            return new MessageRestartGame();
        }
    }
    
    public class MessageOnGameEnd
    {
        private MessageOnGameEnd()
        {
       
        }

        public static MessageOnGameEnd Create()
        {
            return new MessageOnGameEnd();
        }
    }
}