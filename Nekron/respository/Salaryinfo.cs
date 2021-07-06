using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nekron.respository
{
    class Salaryinfo {
        public static string username, paymentDate, paymentMode, paymentModeDetails;
        public static int employeeID;
        public static double basicSalary, totalPayment, deduction;

        public static void recieveSalaryInfo(string Username, string PaymentDate, string PaymentMode, string PaymentModeDetails, double Deduction, double TotalPayment, int EmployeeID, double BasicSalary) {
            username = Username;
            paymentDate = PaymentDate;
            paymentMode = PaymentMode;
            paymentModeDetails = PaymentModeDetails;
            deduction = Deduction;
            employeeID = EmployeeID;
            basicSalary = BasicSalary;
            totalPayment = TotalPayment;
        }

    }
}
