using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
    class RegisterResult
    {
        private string _totalFee;
        private string _feeBeforeTax;
        private string _tax;
        private string _bookId;

        public string TotalFee
        {
            get { return _totalFee; }
            set { _totalFee = value; }
        }

        public string Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        public string BookId
        {
            get { return _bookId; }
            set { _bookId = value; }
        }

        public string FeeBeforeTax
        {
            get { return _feeBeforeTax; }
            set { _feeBeforeTax = value; }
        }
    }
}
