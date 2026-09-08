using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal class Quenn : Bee
    {
        private const float EGGS_PER_SHIFT = 0.45f;
        private const float HONEY_PER_UNASSIGNED_WORKER = 0.5f;

        private Bee[] workers = new Bee[0];
        private float eggs = 0;
        private float unassignedWorkers = 3;

        public override float CostPerShift { get { return 2.15f; } }
        public string StatusReport { get; private set; }

        private void AssignBee(string job)
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
        private void AddWorker(Bee worker)
        {
            if (unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }
        }

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

        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }

        public void UpdateStatusReport()
        {

        }

        public Quenn() : base("Quenn")
        {
            AssignBee("Nectar Collector");
            AssignBee("Honey Manufacturer");
            AssignBee("Egg Care");
        }
    }

    class NectarCollector : Bee
    {
        public override float CostPerShift { get { return 1.95f; } }

        public NectarCollector() : base("Nectar Collector") { }
    }

    class HoneyManufacturer : Bee
    {
        public override float CostPerShift { get { return 1.7f; } }

        public HoneyManufacturer() : base("Honey Manufacturer") { }
    }

    class EggCare : Bee
    {
        public override float CostPerShift { get { return 1.35f; } }

        public EggCare(Quenn quenn) : base("Egg Care") { }
    }
}
