using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator
{
    class CalculatorViewModel : ViewModelBase
    {
        private decimal _currentValue;
        private int _decimal;
        private decimal _storedValue;
        private bool _isFloat;
        private Operation _pendingOperation;

        public decimal CurrentValue
        {
            get { return this._currentValue; }
            set
            {
                Set(ref this._currentValue, value);
            }
        }

        public decimal StoredValue
        {
            get { return this._storedValue; }
            set
            {
                Set(ref this._storedValue, value);
            }
        }

  
        public ICommand KeyCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand MinusCommand { get; private set; }
        public ICommand MultiplyCommand { get; private set; }
        public ICommand DivideCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand PointCommand { get; private set; }
        public ICommand EqualCommand { get; private set; }

        public CalculatorViewModel()
        {
            this.CurrentValue = 0;
            this.StoredValue = 0;
            this._isFloat = false;
            this._pendingOperation = Operation.None;
            
            this.KeyCommand = new RelayCommand(this.AppendKeys);
            this.AddCommand = new RelayCommand(this.Add);
            this.MinusCommand = new RelayCommand(this.Minus);
            this.MultiplyCommand = new RelayCommand(this.Multiply);
            this.DivideCommand = new RelayCommand(this.Divide);
            this.EqualCommand = new RelayCommand(this.Equal);
            this.BackCommand = new RelayCommand(this.Back);
            this.PointCommand = new RelayCommand(this.Point);
        }

        private void AppendKeys(object keyString)
        {
            var keystring = keyString.ToString();
            foreach (char key in keystring)
            {
                if (!char.IsDigit(key))
                {
                    throw new ArgumentException("Invalid key", "key");
                }
                if (!this._isFloat)
                    this.CurrentValue = (this.CurrentValue * 10) + (int)char.GetNumericValue(key);
                else
                    this.CurrentValue = (this.CurrentValue * 10) + (int)char.GetNumericValue(key);
            }
        }

        private void Add(object _)
        {
            this.Calculate();
            this._pendingOperation = Operation.Add;
            this.StoredValue = this.CurrentValue;
            this.CurrentValue = 0;
        }

        private void Minus(object _)
        {
            this.Calculate();
            this._pendingOperation = Operation.Subtract;
            this.StoredValue = this.CurrentValue;
            this.CurrentValue = 0;
        }

        private void Multiply(object _)
        {
            this.Calculate();
            this._pendingOperation = Operation.Multiply;
            this.StoredValue = this.CurrentValue;
            this.CurrentValue = 0;
        }

        private void Divide(object _)
        {
            this.Calculate();
            this._pendingOperation = Operation.Divide;
            this.StoredValue = this.CurrentValue;
            this.CurrentValue = 0;
        }

        private void Equal(object _)
        {
            this.Calculate();
            this.StoredValue = 0;
        }


        private void Point(object _)
        {
            this._isFloat = true;
        }

        private void Calculate()
        {
            switch (this._pendingOperation)
            {
                case Operation.Add:
                    this.CurrentValue = this.StoredValue + this.CurrentValue;
                    break;
                case Operation.Subtract:
                    this.CurrentValue = this.StoredValue - this.CurrentValue;
                    break;
                case Operation.Multiply:
                    this.CurrentValue = this.StoredValue * this.CurrentValue;
                    break;
                case Operation.Divide:
                    this.CurrentValue = this.StoredValue / this.CurrentValue;
                    break;
                case Operation.None: return;
            }

            this._pendingOperation = Operation.None;
        }

        private void Back(object _)
        {
            this.CurrentValue = 0;
            this.StoredValue = 0;
        }
    }

}
