using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemA
{
    class Program
    {
        static void Main(string[] args)
        {
            var wantsToEvaluateAnotherStudy = false;

            do
            {
                var study = new Study();

                Console.WriteLine("********************************");
                Console.WriteLine("Demands of the study: ");
                Console.WriteLine("********************************");
                Console.WriteLine("- All input values has got to be integers and typed as a number");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Number of temperatures collected: ");
                study.NrOfTemperaturesCollected = Int32.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("********************************");
                Console.WriteLine("Demands for temperature inputs:");
                Console.WriteLine("********************************");
                Console.WriteLine("- The following temperatures has to be between: -1000 000 and 1000 0000");
                Console.WriteLine("- Remember that the values has got to be integers typed as numbers");
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < study.NrOfTemperaturesCollected; i++)
                {
                    var currentTemperature = 0;

                    do
                    {
                        //TODO: Fix {0}
                        Console.Write("Value of temperature " + (i + 1) + ": ");
                        currentTemperature = Int32.Parse(Console.ReadLine());
                        //TOOD: Ev lägg till validering för string värden

                        //TODO skriv med exponenter snyggare
                    } while (!(currentTemperature >= -1000000 && currentTemperature <= 1000000));

                    study.CollectedTemperatures.Add(currentTemperature);
                }

                foreach (var t in study.CollectedTemperatures)
                {
                    if (t < 0)
                    {
                        study.TemperaturesBellowZero.Add(t);
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("********************************");
                Console.WriteLine("Summary: ");
                Console.WriteLine("********************************");
                Console.Write("The number of temperatures bellow zero was: " + study.TemperaturesBellowZero.Count);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Would you like to evaluate another study? (Yes/No): ");
                var input = Console.ReadLine();

                //TODO fixa to lower/upper
                if (input == "Yes" || input == "YES" || input == "yes")
                {
                    wantsToEvaluateAnotherStudy = true;
                    Console.Clear();
                }
                else
                {
                    wantsToEvaluateAnotherStudy = false;
                }

            } while (wantsToEvaluateAnotherStudy == true);

            Console.ReadLine();
        }
    }

    public class Study
    {
        public List<int> CollectedTemperatures { get; set; }
        public List<int> TemperaturesBellowZero { get; set; }

        public Study()
        {
            CollectedTemperatures = new List<int>();
            TemperaturesBellowZero = new List<int>();
        }

        private int nrOfTempCollected = 1;

        public int NrOfTemperaturesCollected
        {
            get
            {
                return nrOfTempCollected;
            }
            set
            {
                if ((value >= 1) && (value <= 100))
                {
                    nrOfTempCollected = value;
                }
            }
        }
    }
}
