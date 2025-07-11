using Game.Scripts.Core;
using ServiceLocator;

namespace Game.Scripts.Services.NumberFormatterService
{
    public interface INumberFormatterService : IGameService
    {
        string Format(BigNumber number, NumberFormatType formatType = NumberFormatType.Standard, int decimalPlaces = 2);
    }
}