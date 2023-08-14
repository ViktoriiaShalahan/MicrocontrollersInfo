using Common.ConsoleIO;
using System;
using System.Collections.Generic;
using MicrocontrollersInfo.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Context.Extensions;

namespace MicrocontrollersInfo.Editor {
    public partial class Editor
    {
        private void FilterHousingTypeByNumberRowsNotUnknown()
        {
            var collection = sortingHousingTypes
                .Where(e => e.numberRows.HasValue);
            Console.Write(collection.ToLineList(
                "\nСписок типів корпусів, для яких вказано кількість ніжок"));
            KeyPressWaiting();
        }
        private void FilterHousingTypeByNumberRowsUnknown()
        {
            var collection = sortingHousingTypes
                .Where(e => !e.numberRows.HasValue);
            Console.Write(collection.ToLineList(
                "\nСписок типів корпусів, для яких не вказано кількість ніжок"));
            KeyPressWaiting();
        }

        private void FilterHousingTypeByStartName()
        {
            string nameStart = Entering.EnterString("\n\tПочаток назви: ");
            var collection = sortingHousingTypes
                .Where(e => e.name.StartsWith(nameStart, StringComparison.InvariantCultureIgnoreCase));
            Console.Write(collection.ToLineList(string.Format(
                "\nСписок типів, що починаються зі \"{0}*\"",
                nameStart)));
            KeyPressWaiting();
        }

        private void FilterHousingTypeByNameFragment()
        {
            string nameSubstring = Entering.EnterString("\n\tФрагмент назви: ");
            var collection = sortingHousingTypes
                .Where(e => e.name.IndexOf(nameSubstring,
                StringComparison.InvariantCultureIgnoreCase) >= 0);
            Console.Write(collection.ToLineList(string.Format(
                "\nСписок типів, що містять \"*{0}*\"",
                nameSubstring)));
            KeyPressWaiting();
        }

        private void FilterMicrocontrollersByPriceMinMax()
        {
            decimal min = Entering.EnterDecimal("Введіть мінімальну ціну", 2M, 14100M);
            decimal max = Entering.EnterDecimal("Введіть максимальну ціну", min, 14100M);
            var collection = sortingMicrocontrollers
                .Where(e => e.price >= min && e.price <= max);
            Console.Write(collection.ToLineList(string.Format("\nСписок мікроконтролерів від {0} до {1} грн", min, max)));
            KeyPressWaiting();
        }

    }
}
