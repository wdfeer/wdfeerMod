using System;
namespace wdfeerMod
{
    public enum ProcType
    {
        Slash,
        Electricity
    }
    public class StackableProc
    {
        public ProcType type; //0 for Slash, 1 for Electro
        public int timeLeft = 300;
        public int dmg = 0;
        public Action OnEnd;
        public StackableProc(ProcType Type, int damage, Action OnDurationEnd, int duration)
        {
            type = Type;
            timeLeft = duration;
            dmg = damage;
            OnEnd = OnDurationEnd;
        }

        public void Update()
        {
            timeLeft -= 1;
            if (timeLeft <= 0)
            {
                OnEnd();
            }
        }
    }
}
