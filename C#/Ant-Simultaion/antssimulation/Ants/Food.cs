using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Vitruvian.Distribution;
using Vitruvian.Serialization;
using Vitruvian.Distribution.SyncPatterns;

namespace Ants
{
    [OptimisticSerialization]
    public class Food : INotifyPropertyChanged
    {
        private int amount = 0;

        public Food()
        { }

        public Food(int amount)
        {
            this.amount = amount;
        }

		public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
            }
        }

		public bool IsGone
        {
            get { return (Amount == 0); }
        }

		public void Add(Food foodToAdd)
        {
            Amount += foodToAdd.amount;
        }

		public bool Consume(int amountToConsume)
        {
            bool result = false;
            if (amountToConsume <= amount)
            {
                Amount -= amountToConsume;
                result = true;
            }
            else
                Amount = 0;
            return result;
        }

		public Food Extract(int amountToExtract)
        {
            Food result = null;
            if (!IsGone)
            {
                int extractedAmount = Math.Min(amountToExtract, amount);
                Amount -= extractedAmount;
                result = new Food(extractedAmount);
            }
            return result;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
