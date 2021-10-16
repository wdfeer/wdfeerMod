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

        public StackableProc(ProcType Type, int damage, int duration = 300)
        {
            type = Type;
            timeLeft = duration;
            dmg = damage;
        }

        public void Update()
        {
            if (dmg == 0) return;
            timeLeft -= 1;
            if (timeLeft <= 0) dmg = 0;
        }
    }
}
