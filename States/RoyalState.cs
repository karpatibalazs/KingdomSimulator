using KingdomSim.Entities;
using KingdomSim.Models;

namespace KingdomSim.States
{
    /// <summary>
    /// Egy királyi (Royal) provincia állapotát reprezentálja.
    /// </summary>
    public class RoyalState : IProvinceState
    {
        /// <summary>
        /// Fogadja a lovagot, és meghívja a lovag királyi provinciára vonatkozó interakcióját.
        /// </summary>
        /// <param name="knight">A provinciába érkező lovag.</param>
        /// <param name="province">A provincia, ahová érkezett.</param>
        public void Accept(Knight knight, Province province)
        {
            knight.VisitRoyal(this, province);
        }
    }
}