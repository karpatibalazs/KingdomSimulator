using KingdomSim.Entities;
using KingdomSim.Models;

namespace KingdomSim.States
{
    /// <summary>
    /// Egy semleges (Neutral) provincia állapotát reprezentálja.
    /// </summary>
    public class NeutralState : IProvinceState
    {
        /// <summary>
        /// Fogadja a lovagot, és meghívja a lovag semleges provinciára vonatkozó interakcióját.
        /// </summary>
        /// <param name="knight">A provinciába érkező lovag.</param>
        /// <param name="province">A provincia, ahová érkezett.</param>
        public void Accept(Knight knight, Province province)
        {
            knight.VisitNeutral(this, province);
        }
    }
}