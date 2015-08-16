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
        private string _currentPresent;

        private string _storedValue;
        private Operation _pendingOperation;

        public string CurrentValue
        {
            get { return this._currentPresent; }
            set
            {
                Set(ref this._currentPresent, value);
            }
        }

        public string StoredValue
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
            this.CurrentValue = "0";
            this.StoredValue = "0";
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
                if (this.CurrentValue == "0" || this.CurrentValue.Contains("Invalid"))
                {
                    this.CurrentValue = key.ToString();
                }
                else
                {
                    this.CurrentValue = this.CurrentValue + key;
                }
/*
                this._currentValue = 

                if (this._decimal == 0)
                {
                    this._currentValue = this._currentValue * 10 + (int)(Char.GetNumericValue(key));
                }
                else
                {
                    this._currentValue += Math.Pow(0.1, this._decimal) * (int)(Char.GetNumericValue(key));
                    this._decimal++;
                }
                */
            }
        }

        private void Add(object _)
        {
            this.Calculate();
            this._pendingOperation = Operation.Add;
            this.StoredValue =this.CurrentValue;
            this.CurrentValue = "0";
        }

        private void Minus(object _)
        {
            this.Calculate();
            this._pendingOperation = Operation.Subtract;
            this.StoredValue = this.CurrentValue;
            this.CurrentValue = "0";
        }

        private void Multiply(object _)
        {
            this.Calculate();
            this._pendingOperation = Operation.Multiply;
            this.StoredValue = this.CurrentValue;
            this.CurrentValue = "0";
        }

        private void Divide(object _)
        {
            this.Calculate();
            this._pendingOperation = Operation.Divide;
            this.StoredValue = this.CurrentValue;
            this.CurrentValue = "0";
        }

        private void Equal(object _)
        {
            this.Calculate();
            this.StoredValue = "0";
        }


        private void Point(object _)
        {
            if (!this.CurrentValue.Contains('0'))
            {
                this.CurrentValue += '.';
            }
        }

        private void Calculate()
        {
            try {
                switch (this._pendingOperation)
                {
                    case Operation.Add:
                        this.CurrentValue = (Convert.ToDouble(this.StoredValue) + Convert.ToDouble(this.CurrentValue)).ToString();
                        break;
                    case Operation.Subtract:
                        this.CurrentValue = (Convert.ToDouble(this.StoredValue) - Convert.ToDouble(this.CurrentValue)).ToString();
                        break;
                    case Operation.Multiply:
                        this.CurrentValue = (Convert.ToDouble(this.StoredValue) * Convert.ToDouble(this.CurrentValue)).ToString();
                        break;
                    case Operation.Divide:
                        if (this.CurrentValue == "0")
                        {
                            this.CurrentValue = "Invalid number to divide";
                            break;
                        }
                        this.CurrentValue = (Convert.ToDouble(this.StoredValue) / Convert.ToDouble(this.CurrentValue)).ToString();
                        break;
                    case Operation.None: return;
                }
            }
            catch (Exception e)
            {
                this.CurrentValue = "Invalid Calculation";
            }

            this._pendingOperation = Operation.None;
        }

        private void Back(object _)
        {
            this.CurrentValue = "0";
            this._pendingOperation = Operation.None;
            this.StoredValue = "0";
        }
    }

}
