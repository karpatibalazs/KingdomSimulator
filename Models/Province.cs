using System;
using KingdomSim.Entities;
using KingdomSim.States;

namespace KingdomSim.Models
{
    /// <summary>
    /// A királyság egy provinciáját reprezentáló osztály.
    /// </summary>
    public class Province
    {
        /// <summary>
        /// A provincia neve.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A provincia aktuális állapota (Royal, Neutral, Hostile).
        /// </summary>
        public IProvinceState State { get; private set; }

        /// <summary>
        /// Inicializál egy új provinciát a megadott névvel és kezdeti állapottal.
        /// </summary>
        /// <param name="name">A provincia neve.</param>
        /// <param name="initialState">A provincia kezdőállapota.</param>
        /// <exception cref="ArgumentNullException">Dobódik, ha a név vagy az állapot null.</exception>
        public Province(string name, IProvinceState initialState)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            State = initialState ?? throw new ArgumentNullException(nameof(initialState));
        }

        /// <summary>
        /// Megváltoztatja a provincia aktuális állapotát.
        /// </summary>
        /// <param name="newState">Az új állapot, amelybe a provincia lép.</param>
        /// <exception cref="ArgumentNullException">Dobódik, ha az új állapot null.</exception>
        public void SetState(IProvinceState newState)
        {
            State = newState ?? throw new ArgumentNullException(nameof(newState));
        }

        /// <summary>
        /// Fogad egy érkező lovagot, és továbbítja a kérést az aktuális állapotnak.
        /// </summary>
        /// <param name="knight">A provinciába érkező lovag.</param>
        /// <exception cref="ArgumentNullException">Dobódik, ha a lovag null.</exception>
        public void Accept(Knight knight)
        {
            if (knight == null) throw new ArgumentNullException(nameof(knight));

            State.Accept(knight, this);
        }
    }
}