using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.CommonUtility
{
    public class CommonClass
    {

    }

    public class Employee_Details
    {
       // string emp_id;
        //string emp_firstname, emp_lastname, location, doj, pannumber, account_number,designation,bank_name;
      //  bool gender,active;
        public string emp_id;
        public string doj { get; set; }
        public string pannumber { get; set; }
        public string account_number { get; set; }
        public string designation { get; set; }
        public string bank_name { get; set; }
        public bool gender { get; set; }
        public bool active { get; set; }
       
        public string emp_firstname;


        public string emp_lastname { get; set; }


        public string location { get; set; }

      

    }
    public class Login_Details
    {
        public string emp_id { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
    public class Earnings
    {

        public double getBasic()
        {
            return basic;
        }

        public void setBasic(double basic)
        {
            this.basic = basic;
        }

        public double getConveyance()
        {
            return conveyance;
        }

        public void setConveyance(double conveyance)
        {
            this.conveyance = conveyance;
        }

        public double getDearanceAllowance()
        {
            return dearanceAllowance;
        }

        public void setDearanceAllowance(double dearanceAllowance)
        {
            this.dearanceAllowance = dearanceAllowance;
        }

        public double getHra()
        {
            return hra;
        }

        public void setHra(double hra)
        {
            this.hra = hra;
        }

        public double getLoyalityIncentives()
        {
            return loyalityIncentives;
        }

        public void setLoyalityIncentives(double loyalityIncentives)
        {
            this.loyalityIncentives = loyalityIncentives;
        }

        public double getOthers()
        {
            return others;
        }

        public void setOthers(double others)
        {
            this.others = others;
        }

        public double getPerformanceIncentives()
        {
            return performanceIncentives;
        }

        public void setPerformanceIncentives(double performanceIncentives)
        {
            this.performanceIncentives = performanceIncentives;
        }

        public double getTelAllowance()
        {
            return telAllowance;
        }

        public void setTelAllowance(double telAllowance)
        {
            this.telAllowance = telAllowance;
        }

        private double basic;
        private double hra;
        private double telAllowance;
        private double dearanceAllowance;
        private double conveyance;
        private double loyalityIncentives;
        private double performanceIncentives;
        private double others;


    }
    public class Deductions
    {
        private double pf;
        private double esi;


        private double pt;
        private double tds;
        private double lossofpay;


        public double getLossofpay()
        {
            return lossofpay;
        }

        public void setLossofpay(double lossofpay)
        {
            this.lossofpay = lossofpay;
        }
        public double getEsi()
        {
            return esi;
        }

        public void setEsi(double esi)
        {
            this.esi = esi;
        }

        public double getGratuity()
        {
            return gratuity;
        }

        public void setGratuity(double gratuity)
        {
            this.gratuity = gratuity;
        }

        public double getPf()
        {
            return pf;
        }

        public void setPf(double pf)
        {
            this.pf = pf;
        }

        public double getPt()
        {
            return pt;
        }

        public void setPt(double pt)
        {
            this.pt = pt;
        }

        public double getTds()
        {
            return tds;
        }

        public void setTds(double tds)
        {
            this.tds = tds;
        }
        private double gratuity;


    }
    public class Pay
    {
        private int totalDays;
        private int workingDays;
        private double totalDeduction;
        private double totalEarnings;

        private double netincome;
        private double nettakehome;
        private double additional_Allowance;
        private double additional_emideduction;
        private double arrearsof_lastmonthsalary;
        private string deduction;
        private string earnings;
       // private Deductions deduction;
     //   private Earnings earnings;
        private Employee employee;

        public Employee getEmployee()
        {
            return employee;
        }

        public void setEmployee(Employee employee)
        {
            this.employee = employee;
        }

        public string getDeduction()
        {
            return deduction;
        }

        public void setDeduction(string deduction)
        {
            this.deduction = deduction;
        }

        public string getEarnings()
        {
            return earnings;
        }

        public void setEarnings(string earnings)
        {
            this.earnings = earnings;
        }

        //public Deductions getDeduction()
        //{
        //    return deduction;
        //}

        //public void setDeduction(Deductions deduction)
        //{
        //    this.deduction = deduction;
        //}

        //public Earnings getEarnings()
        //{
        //    return earnings;
        //}

        //public void setEarnings(Earnings earnings)
        //{
        //    this.earnings = earnings;
        //}

        public double getNetincome()
        {
            return netincome;
        }

        public void setNetincome(double netincome)
        {
            this.netincome = netincome;
        }

        public double getTotalDeduction()
        {
            return totalDeduction;
        }

        public void setTotalDeduction(double totalDeduction)
        {
            this.totalDeduction = totalDeduction;
        }

        public double getTotalEarnings()
        {
            return totalEarnings;
        }

        public void setTotalEarnings(double totalEarnings)
        {
            this.totalEarnings = totalEarnings;
        }

        public int getTotalDays()
        {
            return totalDays;
        }

        public void setTotalDays(int totalDays)
        {
            this.totalDays = totalDays;
        }

        public int getWorkingDays()
        {
            return workingDays;
        }

        public void setWorkingDays(int workingDays)
        {
            this.workingDays = workingDays;
        }
        public double getAdditional_Allowance()
        {
            return additional_Allowance;
        }

        public void setAdditional_Allowance(double additional_Allowance)
        {
            this.additional_Allowance = additional_Allowance;
        }

        public double getAdditional_emideduction()
        {
            return additional_emideduction;
        }

        public void setAdditional_emideduction(double additional_emideduction)
        {
            this.additional_emideduction = additional_emideduction;
        }

        public double getArrearsof_lastmonthsalary()
        {
            return arrearsof_lastmonthsalary;
        }

        public void setArrearsof_lastmonthsalary(double arrearsof_lastmonthsalary)
        {
            this.arrearsof_lastmonthsalary = arrearsof_lastmonthsalary;
        }

        public double getNettakehome()
        {
            return nettakehome;
        }

        public void setNettakehome(double nettakehome)
        {
            this.nettakehome = nettakehome;
        }
    }

    public class Employee
    {


        private string employeename;
        private string lastname;
        private bool gender;
        // @Gender bit, @Bank_Name varchar(50), @Account_Number varchar(50), @Active bit , @isApplicableForExtraDeduction bit)
        private string Account_Number;
        private string Bank_Name;
        private bool active;
        private bool isApplicableForExtraDeduction;
        private string location;


        public string getBank_Name()
        {
            return Bank_Name;
        }

        public void setBank_Name(string Bank_Name)
        {
            this.Bank_Name = Bank_Name;
        }
        public string getAccount_Number()
        {
            return Account_Number;
        }

        public void setAccount_Number(string Account_Number)
        {
            this.Account_Number = Account_Number;
        }

        public bool isActive()
        {
            return active;
        }

        public void setActive(bool active)
        {
            this.active = active;
        }

        public bool isGender()
        {
            return gender;
        }

        public void setGender(bool gender)
        {
            this.gender = gender;
        }

        public bool isIsApplicableForExtraDeduction()
        {
            return isApplicableForExtraDeduction;
        }

        public void setIsApplicableForExtraDeduction(bool isApplicableForExtraDeduction)
        {
            this.isApplicableForExtraDeduction = isApplicableForExtraDeduction;
        }

        public string getLastname()
        {
            return lastname;
        }

        public void setLastname(string lastname)
        {
            this.lastname = lastname;
        }
        private string employee_id;
        private string pan_number;
        private string Designation;
        private DateTime doj;
        private string email;

        public string getEmail()
        {
            return email;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }


        public string getDesignation()
        {
            return Designation;
        }

        public void setDesignation(string Designation)
        {
            this.Designation = Designation;
        }

        public DateTime getDoj()
        {
            return doj;
        }

        public void setDoj(DateTime doj)
        {
            this.doj = doj;
        }

        public string getEmployee_id()
        {
            return employee_id;
        }

        public void setEmployee_id(string employee_id)
        {
            this.employee_id = employee_id;
        }

        public string getEmployeename()
        {
            return employeename;
        }

        public void setEmployeename(string employeename)
        {
            this.employeename = employeename;
        }

        public string getLocation()
        {
            return location;
        }

        public void setLocation(string location)
        {
            this.location = location;
        }

        public string getPan_number()
        {
            return pan_number;
        }

        public void setPan_number(string pan_number)
        {
            this.pan_number = pan_number;
        }
        //private string


    }



}