using System;
using System.Collections.Generic;
using System.Linq;
using KingdomSim.Entities;
using KingdomSim.Exceptions;
using KingdomSim.States;

namespace KingdomSim.Models
{
    /// <summary>
    /// A királyságot reprezentáló menedzser osztály, amely összefogja a provinciákat és a lovagokat,
    /// valamint vezérli a szimulációs ciklust.
    /// </summary>
    public class Kingdom
    {
        private readonly List<Province> _provinces;
        private readonly List<Knight> _knights;

        /// <summary>
        /// Inicializálja a királyságot a beolvasott provinciákkal és lovagokkal.
        /// </summary>
        /// <param name="provinces">A királyság provinciáinak listája.</param>
        /// <param name="knights">A küldetésre induló lovagok listája.</param>
        /// <exception cref="ArgumentNullException">Dobódik, ha valamelyik lista null.</exception>
        /// <exception cref="ArgumentException">Dobódik, ha a provinciák listája üres.</exception>
        public Kingdom(List<Province> provinces, List<Knight> knights)
        {
            _provinces = provinces ?? throw new ArgumentNullException(nameof(provinces));
            _knights = knights ?? throw new ArgumentNullException(nameof(knights));

            if (_provinces.Count == 0)
            {
                throw new ArgumentException("A királyságnak legalább egy provinciával kell rendelkeznie.");
            }
        }

        /// <summary>
        /// Lefuttatja a teljes szimulációt addig, amíg minden lovag meg nem hal, vagy az összes ellenséget le nem győzik.
        /// Körönként naplózza a konzolra a történéseket.
        /// </summary>
        public void RunFullSimulation()
        {
            int round = 1;
            bool missionSuccess = false;

            while (_knights.Any(k => k.IsAlive()) && _provinces.Any(p => p.State is HostileState))
            {
                Console.WriteLine($"\n--- {round}. KÖR ---");

                foreach (var k in _knights.Where(k => k.IsAlive()))
                {
                    if (k.NeedsRetreat())
                    {
                        var shelter = GetNearestRoyalProvince(k.CurrentProvince);
                        k.RetreatAndHeal(shelter);
                        Console.WriteLine($"[VISSZAVONULÁS] {k.Name} visszatért a(z) {shelter.Name} királyi tartományba gyógyulni. Jelenlegi HP: {k.Hp}");
                    }
                    else
                    {
                        var nextProv = GetNextProvince(k.CurrentProvince);
                        string prevStateName = GetStateName(nextProv.State);

                        nextProv.Accept(k);

                        if (k.IsAlive())
                        {
                            Console.WriteLine($"[HALADÁS] {k.Name} áthaladt a(z) {nextProv.Name} ({prevStateName}) területen. Jelenlegi HP: {k.Hp}");
                        }
                        else
                        {
                            Console.WriteLine($"[HALÁL] {k.Name} hősi halált halt a(z) {nextProv.Name} ({prevStateName}) területen vívott harcban!");
                        }
                    }
                }
                round++;
            }

            missionSuccess = !_provinces.Any(p => p.State is HostileState);

            Console.WriteLine("\n=========================================");
            Console.WriteLine("SZIMULÁCIÓ VÉGE");
            Console.WriteLine("=========================================");
            Console.WriteLine($"Küldetés eredménye: {(missionSuccess ? "SIKERES! A királyság megtisztult." : "ELBUKOTT! Maradtak ellenséges területek.")}");

            PrintFallenHeroes();
        }

        /// <summary>
        /// Megemlékezik a halott lovagokról a feladatkiírás követelményei szerint.
        /// </summary>
        private void PrintFallenHeroes()
        {
            var deadKnights = _knights.Where(k => !k.IsAlive()).ToList();

            Console.WriteLine("\nKötelességünk méltó tisztelettel megemlékezni hősi halottainkról:");
            if (deadKnights.Any())
            {
                foreach (var hero in deadKnights)
                {
                    Console.WriteLine($"- {hero.Name}, aki az életét adta a királyságért.");
                }
            }
            else
            {
                Console.WriteLine("- Szerencsére a küldetés során egyetlen lovag sem esett el.");
            }
        }

        /// <summary>
        /// Visszaadja a provincia állapotának olvasható magyar nevét a konzolos naplózáshoz.
        /// </summary>
        private string GetStateName(IProvinceState state)
        {
            if (state is RoyalState) return "Királyi";
            if (state is NeutralState) return "Semleges";
            if (state is HostileState) return "Ellenséges";
            return "Ismeretlen";
        }

        /// <summary>
        /// Megkeresi a legközelebbi királyi provinciát a kiindulási ponthoz képest.
        /// A keresés körkörös listaként kezeli a provinciákat (előre és hátra is mér).
        /// </summary>
        /// <param name="from">A kiindulási provincia.</param>
        /// <returns>A legközelebbi királyi provincia.</returns>
        /// <exception cref="ArgumentException">Dobódik, ha a kiindulási provincia nincs a listában.</exception>
        /// <exception cref="ProvinceNotFoundException">Dobódik, ha egyáltalán nincs királyi provincia.</exception>
        private Province GetNearestRoyalProvince(Province from)
        {
            int startIndex = _provinces.IndexOf(from);
            if (startIndex == -1)
            {
                throw new ArgumentException("A kiindulási provincia nem található a királyságban.", nameof(from));
            }

            var royalProvinces = _provinces.Where(p => p.State is RoyalState).ToList();

            if (!royalProvinces.Any())
            {
                throw new ProvinceNotFoundException("Nincs elérhető királyi provincia a visszavonuláshoz!");
            }

            Province? nearest = null;
            int minDistance = int.MaxValue;
            int totalProvinces = _provinces.Count;

            foreach (var royal in royalProvinces)
            {
                int targetIndex = _provinces.IndexOf(royal);

                int forwardDistance = (targetIndex - startIndex + totalProvinces) % totalProvinces;
                int backwardDistance = (startIndex - targetIndex + totalProvinces) % totalProvinces;

                int shortestDistance = Math.Min(forwardDistance, backwardDistance);

                if (shortestDistance < minDistance)
                {
                    minDistance = shortestDistance;
                    nearest = royal;
                }
            }

            return nearest ?? throw new InvalidOperationException("Kritikus hiba: A legközelebbi királyi provincia kiszámítása sikertelen."); ;
        }

        /// <summary>
        /// Visszaadja a sorban következő provinciát a haladáshoz (körkörös listaként kezelve).
        /// </summary>
        /// <param name="from">A jelenlegi provincia.</param>
        /// <returns>A soron következő provincia.</returns>
        private Province GetNextProvince(Province from)
        {
            int currentIndex = _provinces.IndexOf(from);

            if (currentIndex == -1) return _provinces[0];

            int nextIndex = (currentIndex + 1) % _provinces.Count;
            return _provinces[nextIndex];
        }
    }
}