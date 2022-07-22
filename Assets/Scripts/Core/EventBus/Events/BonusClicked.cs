using Core.Bonuses;
using Core.Bonuses.BonusEntity;

namespace Core.EventBus.Events {
    public readonly struct BonusClicked {
        public readonly Bonus Bonus;

        public BonusClicked(Bonus bonus) {
            Bonus = bonus;
        }
    }
}