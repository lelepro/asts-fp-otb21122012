using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flights
{
	public class Parameters
	{
	    private string _from = "";
	    private string _to ="";
	    private string _departDay = "1";
	    private string _departMonth = "1";
	    private string _departYear = "1900";
	    private string _returnDay = "1";
	    private string _returnMonth = "1";
	    private string _returnYear = "1900";
	    private string _bookingType = "oneway";

	    public string From
	    {
	        get { return _from; }
	        set { _from = value; }
	    }

	    public string To
	    {
	        get { return _to; }
	        set { _to = value; }
	    }

	    public string DepartDay
	    {
	        get { return _departDay; }
	        set { _departDay = value; }
	    }

	    public string DepartMonth
	    {
	        get { return _departMonth; }
	        set { _departMonth = value; }
	    }

	    public string DepartYear
	    {
	        get { return _departYear; }
	        set { _departYear = value; }
	    }

	    public string ReturnDay
	    {
	        get { return _returnDay; }
	        set { _returnDay = value; }
	    }

	    public string ReturnMonth
	    {
	        get { return _returnMonth; }
	        set { _returnMonth = value; }
	    }

	    public string ReturnYear
	    {
	        get { return _returnYear; }
	        set { _returnYear = value; }
	    }

	    public string BookingType
	    {
	        get { return _bookingType; }
	        set { _bookingType = value; }
	    }
	}
}
