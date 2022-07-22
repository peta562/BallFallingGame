using Core.Bonuses;
using Core.Bonuses.BonusEntity;

namespace Core.EventBus.Events {
    public readonly struct BonusFell {
        public readonly Bonus Bonus;

        public BonusFell(Bonus bonus) {
            Bonus = bonus;
        }
    }
}