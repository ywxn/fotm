public class Lotus2 : Enemy
{
    public override float Health { get => base.Health; set => base.Health = 10f; }
    public override float XP
    {
        get { return base.XP * 3f; }
        set { base.XP = value; }
    }
}