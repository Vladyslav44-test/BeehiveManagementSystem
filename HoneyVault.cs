using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal static class HoneyVault
    {
        private const float NECTAR_CONVERSION_RATIO = 0.19f;
        private const float LOW_LEVEL_WARNING = 10f;

        private static float honey = 25f;
        private static float nectar = 100f;

        public static string StatusReport
        {
            get
            {
                string text = $"Vault report:\n{honey} units of honey\n{nectar} units of nectar";
                if (honey < LOW_LEVEL_WARNING) text += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                if (nectar < LOW_LEVEL_WARNING) text += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";
                return text;
            }
        }

        public static bool ConsumeHoney(float amount)
        {
            if (amount <= honey)
            {
                honey -= amount;
                return true;
            }
            return false;
        }
        public static void CollectNectar(float amount)
        {
            if (amount >= 0) nectar += amount;
        }
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
