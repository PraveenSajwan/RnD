using System;

namespace VerifyAdhaar
{
    class Program
    {
        static void Main(string[] args)
        {
            long aadharNumber;
            Console.WriteLine("Hello This is Sample Application to verify if Aadhaar Number is valid or not!!");
            Console.WriteLine("Please Enter 12 Digit Aadhaar Number:");
            bool isParsed = Int64.TryParse(Console.ReadLine(), out aadharNumber);
            if(isParsed)
            {
                AadharVerification aadharVerification = new AadharVerification();
                bool isValid = aadharVerification.IsValidAadhaarNumber(aadharNumber);
                string status = isValid ? "Valid" : "Invalid";
                Console.WriteLine($"Aadhar Number provided is: { status}");
            }
            
        }
    }
}
