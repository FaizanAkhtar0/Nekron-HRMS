using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nekron.respository
{
    class Feeinfo
    {


        public static double tutionFee, labCharges, securityCharges, healthServices, TotalFee, avalibleConcession, finalFee, fine;
        public static string dueDate;

        public Feeinfo() {

        }

        public void recieveInfo(double TutionFee, double LabCharges, double SecurityCharges, double HealthServices, double totalFee, double AvalibleConcession, double FinalFee, string DueDate, double Fine) {
            tutionFee = TutionFee;
            labCharges = LabCharges;
            securityCharges = SecurityCharges;
            healthServices = HealthServices;
            TotalFee = totalFee;
            avalibleConcession = AvalibleConcession;
            finalFee = FinalFee;
            dueDate = DueDate;
            fine = Fine;
        }
    }
}
