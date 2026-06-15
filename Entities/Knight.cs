using System;
using KingdomSim.Models;
using KingdomSim.States;

namespace KingdomSim.Entities
{
    /// <summary>
    /// A királyság lovagjainak absztrakt ősosztálya.
    /// </summary>
    public abstract class Knight
    {
        /// <summary>
        /// Közös véletlenszám-generátor a harci sebzésekhez és gyógyuláshoz.
        /// </summary>
        protected static readonly Random Rnd = new Random();

        /// <summary>
        /// A lovag neve.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// A lovag aktuális életereje.
        /// </summary>
        public int Hp { get; protected set; }

        /// <summary>
        /// A provincia, ahol a lovag éppen tartózkodik.
        /// </summary>
        public Province CurrentProvince { get; set; }

        /// <summary>
        /// Inicializál egy lovagot a megadott névvel, életerővel és kezdeti helyszínnel.
        /// </summary>
        /// <param name="name">A lovag neve.</param>
        /// <param name="hp">Kezdeti életerő.</param>
        /// <param name="currentProvince">Kezdeti provincia.</param>
        /// <exception cref="ArgumentNullException">Dobódik, ha a név vagy a provincia null.</exception>
        protected Knight(string name, int hp, Province currentProvince)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Hp = hp;
            CurrentProvince = currentProvince ?? throw new ArgumentNullException(nameof(currentProvince));
        }

        /// <summary>
        /// Meghatározza, hogy a lovagnak vissza kell-e vonulnia gyógyulni.
        /// </summary>
        /// <returns>Igaz, ha visszavonulás szükséges, különben hamis.</returns>
        public abstract bool NeedsRetreat();

        /// <summary>
        /// Visszavonulás egy biztonságos menedékbe, majd gyógyulás egy 1 és 20 közötti véletlen értékkel.
        /// </summary>
        /// <param name="shelter">A biztonságot nyújtó királyi provincia.</param>
        /// <exception cref="ArgumentNullException">Dobódik, ha a menedék null.</exception>
        public void RetreatAndHeal(Province shelter)
        {
            CurrentProvince = shelter ?? throw new ArgumentNullException(nameof(shelter));
            Heal(Rnd.Next(1, 21));
        }

        /// <summary>
        /// Csökkenti a lovag életerejét a megadott sebzéssel.
        /// </summary>
        /// <param name="amount">A bekapott sebzés mértéke.</param>
        public void TakeDamage(int amount)
        {
            Hp -= amount;
        }

        /// <summary>
        /// Növeli a lovag életerejét a megadott értékkel.
        /// </summary>
        /// <param name="amount">A kapott gyógyítás mértéke.</param>
        public void Heal(int amount)
        {
            Hp += amount;
        }

        /// <summary>
        /// Ellenőrzi, hogy a lovag még életben van-e.
        /// </summary>
        /// <returns>Igaz, ha Hp > 0, különben hamis.</returns>
        public bool IsAlive() => Hp > 0;


        /// <summary>
        /// Interakció egy királyi provinciával.
        /// </summary>
        public abstract void VisitRoyal(RoyalState s, Province p);

        /// <summary>
        /// Interakció egy semleges provinciával.
        /// </summary>
        public abstract void VisitNeutral(NeutralState s, Province p);

        /// <summary>
        /// Interakció egy ellenséges provinciával.
        /// </summary>
        public abstract void VisitHostile(HostileState s, Province p);
    }
}