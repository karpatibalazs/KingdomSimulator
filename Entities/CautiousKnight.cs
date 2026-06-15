using KingdomSim.Models;
using KingdomSim.States;

namespace KingdomSim.Entities
{
    /// <summary>
    /// Az Óvatos Lovag, aki hamar visszavonul, és harcban is csak egyet üt kisebb sebzéssel.
    /// </summary>
    public class CautiousKnight : Knight
    {
        public CautiousKnight(string name, int hp, Province currentProvince) : base(name, hp, currentProvince) { }

        public override bool NeedsRetreat() => Hp <= 90;

        public override void VisitRoyal(RoyalState s, Province p)
        {
            CurrentProvince = p;
        }

        public override void VisitNeutral(NeutralState s, Province p)
        {
            CurrentProvince = p;
        }

        public override void VisitHostile(HostileState s, Province p)
        {
            CurrentProvince = p;

            int dmg = Rnd.Next(1, 21);
            TakeDamage(dmg);
            s.SetEnemyHp(s.GetEnemyHp() - dmg);

            if (s.GetEnemyHp() <= 0)
            {
                p.SetState(new NeutralState());
            }
        }
    }
}