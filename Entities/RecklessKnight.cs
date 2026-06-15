using KingdomSim.Models;
using KingdomSim.States;

namespace KingdomSim.Entities
{
    /// <summary>
    /// A Vakmerő Lovag, aki sosem vonul vissza, és halálig harcol az ellenséggel.
    /// </summary>
    public class RecklessKnight : Knight
    {
        public RecklessKnight(string name, int hp, Province currentProvince) : base(name, hp, currentProvince) { }

        public override bool NeedsRetreat() => false;

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

            while (IsAlive() && s.GetEnemyHp() > 0)
            {
                int dmg = Rnd.Next(1, 41);
                TakeDamage(dmg);
                s.SetEnemyHp(s.GetEnemyHp() - dmg);
            }

            if (s.GetEnemyHp() <= 0)
            {
                p.SetState(new NeutralState());
            }
        }
    }
}