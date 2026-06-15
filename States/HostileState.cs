using KingdomSim.Entities;
using KingdomSim.Models;

namespace KingdomSim.States
{
    /// <summary>
    /// Egy ellenséges (Hostile) provincia állapotát reprezentálja.
    /// </summary>
    public class HostileState : IProvinceState
    {
        private int _enemyHp;

        /// <summary>
        /// Inicializál egy új ellenséges állapotot a megadott ellenséges életerővel.
        /// </summary>
        /// <param name="initialEnemyHp">Az ellenség kezdeti életereje.</param>
        public HostileState(int initialEnemyHp)
        {
            _enemyHp = initialEnemyHp;
        }

        /// <summary>
        /// Visszaadja az ellenség aktuális életerejét.
        /// </summary>
        /// <returns>Az ellenség életereje.</returns>
        public int GetEnemyHp() => _enemyHp;

        /// <summary>
        /// Beállítja az ellenség aktuális életerejét.
        /// </summary>
        /// <param name="hp">Az új életerő.</param>
        public void SetEnemyHp(int hp) => _enemyHp = hp;

        /// <summary>
        /// Fogadja a lovagot, és meghívja a lovag ellenséges provinciára vonatkozó interakcióját.
        /// </summary>
        /// <param name="knight">A provinciába érkező lovag.</param>
        /// <param name="province">A provincia, ahová érkezett.</param>
        public void Accept(Knight knight, Province province)
        {
            knight.VisitHostile(this, province);
        }
    }
}