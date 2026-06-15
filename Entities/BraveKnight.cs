using KingdomSim.Models;
using KingdomSim.States;

namespace KingdomSim.Entities
{
    /// <summary>
    /// A Bátor Lovag, aki alacsony életerőnél visszavonul, és maximum két ütésváltást vív egy harcban.
    /// </summary>
    public class BraveKnight : Knight
    {
        public BraveKnight(string name, int hp, Province currentProvince) : base(name, hp, currentProvince) { }

        public override bool NeedsRetreat() => Hp <= 40;

        public override void VisitRoyal(RoyalState s, Province p)
        {
            CurrentProvince = p;
        }

        public override void VisitNeutral(NeutralState s, Province p)
        {
            CurrentProvince = p;
            p.SetState(new RoyalState());
        }

        public override void VisitHostile(HostileState s, Province p)
        {
            CurrentProvince = p;

            int dmg = Rnd.Next(1, 41);
            TakeDamage(dmg);
            s.SetEnemyHp(s.GetEnemyHp() - dmg);

            if (IsAlive() && s.GetEnemyHp() > 0)
            {
                int dmg2 = Rnd.Next(1, 41);
                TakeDamage(dmg2);
                s.SetEnemyHp(s.GetEnemyHp() - dmg2);
            }

            if (s.GetEnemyHp() <= 0)
            {
                p.SetState(new NeutralState());
            }
        }
    }
}