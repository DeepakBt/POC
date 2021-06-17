using System;

namespace MyProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Start:
            Console.Write("Are you married or Single Please press M/m for Married and S/s for Single : ");
            string isMarried = Console.ReadLine();
            if(isMarried=="M" || isMarried=="m" || isMarried=="S" || isMarried == "s")
            {
                StartSalary:
                Console.Write("Please Enter your monthly income : ");
                float Salary = float.Parse(Console.ReadLine());
                if (Salary <= 0f)
                {
                    Console.WriteLine("Please enter salary value greater then 0");
                    goto StartSalary;
                }
                float PerValue;
                if(isMarried=="s"|| isMarried == "S")
                {
                    if (Salary <= 8000f)
                    {
                        PerValue = (10f / 100f) * Salary;
                        Console.WriteLine($"Your Salary {Salary.ToString("0.00")}");
                        Console.WriteLine($"Your Total Tax {PerValue.ToString("0.00")}");
                        Console.WriteLine($"Your Salary After Tax {(Salary-PerValue).ToString("0.00")}");
                    }
                    else if(Salary>8000f && Salary <= 32000f)
                    {
                        PerValue = (15f / 100f) * 8000F;
                        PerValue = PerValue + 800.00F;
                        Console.WriteLine($"Your Salary {Salary.ToString("0.00")}");
                        Console.WriteLine($"Your Total Tax {PerValue.ToString("0.00")}");
                        Console.WriteLine($"Your Salary After Tax {(Salary - PerValue).ToString("0.00")}");
                    }
                    else if (Salary > 32000f)
                    {
                        PerValue = (25f / 100f) * 32000F;
                        PerValue = PerValue + 4400f;
                        Console.WriteLine($"Your Salary {Salary.ToString("0.00")}");
                        Console.WriteLine($"Your Total Tax {PerValue.ToString("0.00")}");
                        Console.WriteLine($"Your Salary After Tax {(Salary - PerValue).ToString("0.00")}");
                    }
                }
                else if(isMarried == "m" || isMarried == "M")
                {
                    if (Salary <= 16000f)
                    {
                        PerValue = (10f / 100f) * Salary;
                        Console.WriteLine($"Your Salary {Salary.ToString("0.00")}");
                        Console.WriteLine($"Your Total Tax {PerValue.ToString("0.00")}");
                        Console.WriteLine($"Your Salary After Tax {(Salary - PerValue).ToString("0.00")}");
                    }
                    else if (Salary > 16000f && Salary <= 64000f)
                    {
                        PerValue = (15f / 100f) * 16000f;
                        PerValue = PerValue + 1600.00f;
                        Console.WriteLine($"Your Salary {Salary.ToString("0.00")}");
                        Console.WriteLine($"Your Total Tax {PerValue.ToString("0.00")}");
                        Console.WriteLine($"Your Salary After Tax {(Salary - PerValue).ToString("0.00")}");
                    }
                    else if (Salary > 64000f)
                    {
                        PerValue = (25f / 100f) * 64000F;
                        PerValue = PerValue + 8800f;
                        Console.WriteLine($"Your Salary {Salary.ToString("0.00")}");
                        Console.WriteLine($"Your Total Tax {PerValue.ToString("0.00")}");
                        Console.WriteLine($"Your Salary After Tax {(Salary - PerValue).ToString("0.00")}");
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Invalid Choice");
                goto Start;
            }
        }
    }
}
