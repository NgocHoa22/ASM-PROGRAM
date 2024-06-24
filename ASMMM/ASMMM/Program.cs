using System;
using System.Globalization;

namespace CalculateWaterBill
{
    internal class Program
    {
        static string userName; // dùng để gọi lại tên khách hàng

        static void GetUserName() // dùng để nhập tên khách hàng
        {
            Console.Write("Enter your name: ");
            userName = Console.ReadLine();
        }

        static void ApplyTax(ref double waterCost, out double tax) // áp dụng thuế
        {
            tax = waterCost * 0.10;
            waterCost += tax;
        }

        static void DisplayMenu() // in menu
        {
            Console.WriteLine("|------------------------------------------------|");
            Console.WriteLine($"|Hello {userName}, please choose your customer group:   |");
            Console.WriteLine("|------------------------------------------------|");
            Console.WriteLine("|1. -----------------HouseHold-------------------|");
            Console.WriteLine("|------------------------------------------------|");
            Console.WriteLine("|2. -------------Administrative agency-----------|");
            Console.WriteLine("|------------------------------------------------|");
            Console.WriteLine("|3. ---------------Production unit---------------|");
            Console.WriteLine("|------------------------------------------------|");
            Console.WriteLine("|4. -------------Business service----------------|");
            Console.WriteLine("|------------------------------------------------|");
            Console.WriteLine("|------------Which group do you belong to--------|");
            Console.WriteLine("|------------------------------------------------|");
        }

        static double CalculateWaterUsage(double lastMonthMeter, double thisMonthMeter) // tính lượng nước sử dụng
        {
            return thisMonthMeter - lastMonthMeter;
        }

        static void CalculateHouseHoldBill(double waterUsage, int peopleCount) // hộ gia đình
        {
            double waterCost, tax;
            double[] waterRates = { 5.973, 7.052, 8.699, 15.929 };
            double[] waterLimits = { 10, 20, 30 };

            if (waterUsage <= waterLimits[0] * peopleCount)
            {
                waterCost = waterUsage * waterRates[0];
            }
            else if (waterUsage <= waterLimits[1] * peopleCount)
            {
                waterCost = waterUsage * waterRates[1];
            }
            else if (waterUsage <= waterLimits[2] * peopleCount)
            {
                waterCost = waterUsage * waterRates[2];
            }
            else
            {
                waterCost = waterUsage * waterRates[3];
            }
            ApplyTax(ref waterCost, out tax);
            PrintBill(waterCost, tax);
        }

        static void CalculateAdministrativeAgencyBill(double waterUsage) // cơ quan hành chính
        {
            double tax;
            double waterCost = waterUsage * 9.955;
            ApplyTax(ref waterCost, out tax);
            PrintBill(waterCost, tax);
        }

        static void CalculateProductionUnitBill(double waterUsage) // đơn vị sản xuất
        {
            double tax;
            double waterCost = waterUsage * 11.615;
            ApplyTax(ref waterCost, out tax);
            PrintBill(waterCost, tax);
        }

        static void CalculateBusinessServiceBill(double waterUsage) // dịch vụ kinh doanh
        {
            double tax;
            double waterCost = waterUsage * 22.068;
            ApplyTax(ref waterCost, out tax);
            PrintBill(waterCost, tax);
        }

        static void PrintBill(double waterCost, double tax) // in ra hoá đơn
        {
            CultureInfo culture = new CultureInfo("vi-VN"); // chuyển sang ngôn ngữ tiếng việt
            double totalCost = waterCost + tax;

            Console.WriteLine("|\n--------------------------------------------|");
            Console.WriteLine("|          Water Bill Details               |");
            Console.WriteLine("|-------------------------------------------|");
            Console.WriteLine("| Item                             | Amount |");
            Console.WriteLine("|-------------------------------------------|");
            Console.WriteLine($"| This Month's Tax            | {tax.ToString("#,##0.000", culture),7} VND |");
            Console.WriteLine($"| This Month's Water Cost     | {waterCost.ToString("#,##0.000", culture),7} VND |");
            Console.WriteLine("|-------------------------------------------|");
            Console.WriteLine($"| Total Amount to Pay         | {totalCost.ToString("#,##0.000", culture),7} VND |");
            Console.WriteLine("|-------------------------------------------|");
            Console.WriteLine($"Thanks {userName}");
        }

        static void MainFunction() // hàm này để gọi các chức năng
        {
            DisplayMenu();
            Console.Write("Enter your user group: ");
            int customerGroup = int.Parse(Console.ReadLine());
            if (customerGroup < 1 || customerGroup > 4) // nếu nhập ngoài khoảng 1 - 4 in ra lỗi
            {
                Console.WriteLine("Invalid customer group");
            }
            else
            {
                Console.Write("Enter last month's water meter reading: ");
                double lastMonthMeter = double.Parse(Console.ReadLine());

                Console.Write("Enter this month's water meter reading: ");
                double thisMonthMeter = double.Parse(Console.ReadLine());

                double waterUsage = CalculateWaterUsage(lastMonthMeter, thisMonthMeter);



                if (customerGroup == 1)
                {
                    Console.Write("Enter number of people: ");
                    int peopleCount = int.Parse(Console.ReadLine());
                    CalculateHouseHoldBill(waterUsage, peopleCount);
                }
                else
                {
                    switch (customerGroup)
                    {
                        case 2:
                            CalculateAdministrativeAgencyBill(waterUsage);
                            break;
                        case 3:
                            CalculateProductionUnitBill(waterUsage);
                            break;
                        case 4:
                            CalculateBusinessServiceBill(waterUsage);
                            break;
                    }
                }
            }
        }

        static void Main(string[] args) // hàm chính
        {
            GetUserName();
            MainFunction();
        }
    }
}
