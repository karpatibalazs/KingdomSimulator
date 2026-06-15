using KingdomSim.Entities;
using KingdomSim.Models;

namespace KingdomSim.States
{
    /// <summary>
    /// Egy provincia aktuális állapotát reprezentáló interfész az Állapot (State) tervezési minta alapján.
    /// </summary>
    public interface IProvinceState
    {
        /// <summary>
        /// Kezeli a lovag érkezését az adott állapotú provinciába.
        /// </summary>
        /// <param name="knight">A provinciába érkező lovag.</param>
        /// <param name="province">Maga a provincia, ahová a lovag érkezik.</param>
        void Accept(Knight knight, Province province);
    }
}