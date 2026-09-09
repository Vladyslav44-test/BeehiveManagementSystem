using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal class Quenn : Bee
    {
        /// <summary>
        /// Константа для збільшення поля eggs.
        /// </summary>
        private const float EGGS_PER_SHIFT = 0.45f;
        /// <summary>
        /// Константа для споживання меду всіма незайнятими бджолами.
        /// </summary>
        private const float HONEY_PER_UNASSIGNED_WORKER = 0.5f;

        /// <summary>
        /// Всі зайтяті бджоли.
        /// </summary>
        private Bee[] workers = new Bee[0];
        /// <summary>
        /// Кількість яєць.
        /// </summary>
        private float eggs = 0;
        /// <summary>
        /// Кількість незайнятих бджолей.
        /// </summary>
        private float unassignedWorkers = 3;

        /// <summary>
        /// Кількість меду, яку споживає бджола за зміну (у Quenn = 2.15).
        /// </summary>
        public override float CostPerShift { get { return 2.15f; } }
        /// <summary>
        /// Стрічка зі звітом по кількості меду й нектару в сховищі,
        /// кількості яєць, незайнятих бджолей, робітників кожного типу та загальній кількості зайнятих бджолей.
        /// </summary>
        public string StatusReport { get; private set; }

        /// <summary>
        /// Призначає новій бджолі роботу.
        /// </summary>
        /// <param name="job">Завдання, яке має виконувати бджола.</param>
        public void AssignBee(string job)
        {
            switch (job)
            {
                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;

                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturer());
                    break;

                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;

                default:
                    break;
            }
            UpdateStatusReport();
        }
        /// <summary>
        /// Додає нового робітника в массив workers.
        /// </summary>
        /// <param name="worker">Робітник, який додається до массиву.</param>
        private void AddWorker(Bee worker)
        {
            if (unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }
        }
        /// <summary>
        /// Збільшує кількість яєць, викликає метод WorkTheNextShift кожного робітника,
        /// зменшує кількість меду в сховищі для кожної незайнятої бджоли та оновлюєх звіт.
        /// </summary>
        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;
            foreach (Bee worker in workers)
            {
                worker.WorkTheNextShift();
            }
            HoneyVault.ConsumeHoney(HONEY_PER_UNASSIGNED_WORKER * unassignedWorkers);
            UpdateStatusReport();
        }
        /// <summary>
        /// Перетворює частину яєць в нових незайнятих бджолей.
        /// </summary>
        /// <param name="eggsToConvert">Кількість перетворюваних яєць.</param>
        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }
        /// <summary>
        /// Формує звіт та оновлює ним властивість StatusReport.
        /// </summary>
        public void UpdateStatusReport()
        {
            StatusReport = $"Vault report:\n{HoneyVault.StatusReport}\n" +
                $"\nEgg count: {eggs:0.0}\nUnassigned workers: {unassignedWorkers:0.0}\n" +
                $"{CountWorkers("Nectar Collector")} Nectar Collector bee(s)\n{CountWorkers("Honey Manufacturer")} Honey Manufacturer bee(s)\n" +
                $"{CountWorkers("Egg Care")} Egg Care bee(s)\nTOTAL WORKERS: {workers.Length}";
            
        }
        /// <summary>
        /// Повертає кількість робітників із заданою роботою.
        /// </summary>
        /// <param name="job">Виконувана робота.</param>
        /// <returns>Кількість робітників.</returns>
        private int CountWorkers(string job)
        {
            int count = 0;
            foreach (Bee worker in workers)
            {
                if (worker.Job == job) count++;
            }
            return count;
        }
        /// <summary>
        /// Створює по одному робітнику на кожен тип роботи.
        /// </summary>
        public Quenn() : base("Quenn")
        {
            AssignBee("Nectar Collector");
            AssignBee("Honey Manufacturer");
            AssignBee("Egg Care");
        }
    }

    class NectarCollector : Bee
    {
        /// <summary>
        /// Константа для збільшення кількості нектару.
        /// </summary>
        private const float NECTAR_COLLECTED_PER_SHIFT = 33.25f;

        /// <summary>
        /// Кількість меду, яку споживає бджола за зміну (у NectarCollector = 1.95).
        /// </summary>
        public override float CostPerShift { get { return 1.95f; } }

        /// <summary>
        /// Викликає метод HoneyVault.CollectNectar для збільшення кількості нектару в сховищі.
        /// </summary>
        protected override void DoJob()
        {
            HoneyVault.CollectNectar(NECTAR_COLLECTED_PER_SHIFT);
        }

        public NectarCollector() : base("Nectar Collector") { }
    }

    class HoneyManufacturer : Bee
    {
        /// <summary>
        /// Константа для зменшення кількості нектару та збільшення кількості меду.
        /// </summary>
        private const float NECTAR_PROCESSED_PER_SHIFT = 33.15f;

        /// <summary>
        /// Кількість меду, яку споживає бджола за зміну (у HoneyManufacturer = 1.7).
        /// </summary>
        public override float CostPerShift { get { return 1.7f; } }

        /// <summary>
        /// Викликає метод HoneyVault.ConvertNectarToHoney для збільшення кількості меду в сховищі.
        /// </summary>
        protected override void DoJob()
        {
            HoneyVault.ConvertNectarToHoney(NECTAR_PROCESSED_PER_SHIFT);
        }

        public HoneyManufacturer() : base("Honey Manufacturer") { }
    }

    class EggCare : Bee
    {
        /// <summary>
        /// Константа для перетворення яєць в незайнятих бджолей.
        /// </summary>
        private const float CARE_PROGRESS_PER_SHIFT = 0.15f;

        /// <summary>
        /// Екземпляр Quenn, для якого викликається його метод CareForEggs.
        /// </summary>
        private Quenn quenn;

        /// <summary>
        /// Кількість меду, яку споживає бджола за зміну (у EggCare = 1.35).
        /// </summary>
        public override float CostPerShift { get { return 1.35f; } }

        /// <summary>
        /// Викликає метод Quenn.CareForEggs для перетворення яєць королеви в незайтятих бджолей.
        /// </summary>
        protected override void DoJob()
        {
            quenn.CareForEggs(CARE_PROGRESS_PER_SHIFT);
        }

        /// <summary>
        /// Присвоює значення приватному полю quenn.
        /// </summary>
        /// <param name="quenn">Екземпляр Quenn.</param>
        public EggCare(Quenn quenn) : base("Egg Care")
        {
            this.quenn = quenn;
        }
    }
}
