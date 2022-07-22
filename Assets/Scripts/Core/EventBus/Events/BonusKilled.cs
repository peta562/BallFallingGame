using Core.Bonuses;
using Core.Bonuses.BonusEntity;

namespace Core.EventBus.Events {
    public readonly struct BonusKilled {
        public readonly Bonus Bonus;

        public BonusKilled(Bonus bonus) {
            Bonus = bonus;
        }
    }
}