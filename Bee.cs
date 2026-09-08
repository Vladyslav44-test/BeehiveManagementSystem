using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal class Bee
    {
        /// <summary>
        /// Стрічка із завданням бджоли.
        /// </summary>
        public string Job { get; }
        /// <summary>
        /// Кількість меду, яка споживається бджолою для виконання роботи (перевизначається субкласами). 
        /// </summary>
        public virtual float CostPerShift { get; }

        /// <summary>
        /// Викликає метод DoJob об'єкта, якщо для виконання роботи достатньо меду в сховищі.
        /// </summary>
        public void WorkTheNextShift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift)) DoJob();
        }
        /// <summary>
        /// Перевизначається субклассом.
        /// </summary>
        protected virtual void DoJob()
        {
            /// Перевизначається субклассом.
        }

        /// <summary>
        /// Задає значення властивості Job.
        /// </summary>
        /// <param name="job">Завдання, яке дається бджолі.</param>
        public Bee(string job)
        {
            Job = job;
        }
    }
}
