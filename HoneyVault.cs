using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal static class HoneyVault
    {
        /// <summary>
        /// Константа, яка визначає швидкість перетворення нектару в мед.
        /// </summary>
        private const float NECTAR_CONVERSION_RATIO = 0.19f;
        /// <summary>
        /// Константа для позначення низького рівня меду чи нектару.
        /// </summary>
        private const float LOW_LEVEL_WARNING = 10f;

        /// <summary>
        /// Кількість меду в сховищі.
        /// </summary>
        private static float honey = 25f;
        /// <summary>
        /// Кількість нектару в сховищі.
        /// </summary>
        private static float nectar = 100f;

        /// <summary>
        /// Виводить повідомлення з кількістю меду і нектару та попередженнями.
        /// </summary>
        public static string StatusReport
        {
            get
            {
                string text = $"{honey:0.0} units of honey\n{nectar:0.0} units of nectar";
                if (honey < LOW_LEVEL_WARNING) text += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                if (nectar < LOW_LEVEL_WARNING) text += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";
                return text;
            }
        }

        /// <summary>
        /// Перевіряє, чи вистачає бджолі меду для виконання роботи, і якщо так, віднімає необхіднимй об'єм меду зі сховища.
        /// </summary>
        /// <param name="amount">Необхідна кількість меду.</param>
        /// <returns>Логічне значення можливості бджоли виконувати роботу.</returns>
        public static bool ConsumeHoney(float amount)
        {
            if (amount <= honey)
            {
                honey -= amount;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Збільшує кількість нектару в сховищі.
        /// </summary>
        /// <param name="amount">Додавана кількість нектару.</param>
        public static void CollectNectar(float amount)
        {
            if (amount >= 0f) nectar += amount;
        }
        /// <summary>
        /// Перетворює нектар в мед (віднімає частину нектару зі сховища або використовує весь залишений в сховищі нектар).
        /// </summary>
        /// <param name="amount">Використовувана кількість нектару.</param>
        public static void ConvertNectarToHoney(float amount)
        {
            if (amount > nectar)
            {
                amount = nectar;
                nectar = 0f;
            }
            else
            {
                nectar -= amount;
            }
            honey += amount * NECTAR_CONVERSION_RATIO;
        }
    }
}
