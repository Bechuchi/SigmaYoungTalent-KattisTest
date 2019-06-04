using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemB
{
    class Program
    {
        static void Main(string[] args)
        {
            var route = new Route();
            var train = new Train();
            var station = new Station();

            Console.WriteLine("*****************************");
            Console.WriteLine("Demands for input values: ");
            Console.WriteLine("*****************************");
            Console.WriteLine("- All input values has got to be integers and typed as a number");
            Console.WriteLine();
            Console.WriteLine();


            Console.WriteLine("*****************************");
            Console.WriteLine("Route values: ");
            Console.WriteLine("*****************************");

            Console.Write("Total capacity of the train: ");
            train.PassengerCapacity = Int32.Parse(Console.ReadLine());

            Console.Write("Number of stations the train stops in: ");
            route.Stops = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine();

            var passengersInsideWhenLeaving = 0;
            var passengersInsideWhenArriving = 0;

            for (int i = 0; i <= route.Stops; i++)
            {
                Console.WriteLine("*****************************");
                Console.WriteLine("Values for station " + (i + 1) + ":");
                Console.WriteLine("*****************************");

                Console.Write("Number of people that left the train: ");
                station.PassegersLeaving = Int32.Parse(Console.ReadLine());
                passengersInsideWhenLeaving -= station.PassegersLeaving;

                Console.Write("Enter the number of people that entered the train: ");
                station.PassegersEntering = Int32.Parse(Console.ReadLine());
                passengersInsideWhenLeaving += station.PassegersEntering;

                Console.Write("Enter the number of people that had to stay at the station: ");
                station.PassegersWaiting = Int32.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine();

                var calculation = new Calculation();

                //Demand 1
                var demandOne = calculation.PassengersInsideDidNotExceedCapacity(passengersInsideWhenLeaving,
                                                                                 train.PassengerCapacity);
                route.SummaryDemands.Add(demandOne);

                //Demand 2
                var demandTwo = calculation.PassengersInsideWhereNotBellowZero(passengersInsideWhenLeaving);
                route.SummaryDemands.Add(demandTwo);

                //Demand 3
                var demandThree = calculation.NoPassengerIsStayingAtTheStationWhenCouldHaveEntered(station.PassegersWaiting,
                                                                                                   passengersInsideWhenLeaving,
                                                                                                   train.PassengerCapacity);
                route.SummaryDemands.Add(demandThree);

                if (!(i == 0) && !(i == route.Stops))
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                }
                else if (i == 0)
                {
                    //Demand 4
                    var demandFour = calculation.TrainRouteStartsEmpty(passengersInsideWhenArriving);
                    route.SummaryDemands.Add(demandFour);

                    //Demand 5
                    var demandFive = calculation.NooneLeavsTrainWhenComingToFirstStation(station.PassegersLeaving);
                    route.SummaryDemands.Add(demandFive);
                }
                else if (i == (route.Stops - 1))
                {
                    //Demand 6
                    var demandSix = calculation.NooneEntersTrainAtFinalDestination(station.PassegersEntering);
                    route.SummaryDemands.Add(demandSix);

                    //Demand 7
                    var demandSeven = calculation.NooneWaitsAtFinalDestination(station.PassegersWaiting);
                    route.SummaryDemands.Add(demandSeven);
                }
                else
                {
                    //Demand 8
                    var demandEight = calculation.TrainRouteEndsEmpty(passengersInsideWhenArriving);
                    route.SummaryDemands.Add(demandEight);
                }
            }

            var isPossible = route.IsPossible;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("*****************************");
            Console.WriteLine("Summary of route:");
            Console.WriteLine("*****************************");
            if (route.SummaryDemands.Any(d => d.Equals(false)))
            {
                isPossible = false;
                Console.WriteLine("Impossible");
            }
            else
            {
                isPossible = true;
                Console.WriteLine("Possible");
            }

            //Calculation for the whole route
            Console.WriteLine();
            Console.ReadKey();
        }
    }

    public class Calculation
    {
        //public int GetNrOfPassengersInsideTrainWhenLeaving(int passengersInsideWhenArriving, int passengersLeaving, int passengersEntering)
        //{
        //    var passengersInsideWhenLeaving = passengersInsideWhenArriving - passengersLeaving + passengersEntering;

        //    return passengersInsideWhenLeaving;
        //}

        //Demand 1
        public bool PassengersInsideDidNotExceedCapacity(int nrOfPassengersInside, int passengerCapacity)
        {
            if (nrOfPassengersInside <= passengerCapacity)
            {
                Console.WriteLine("Valid: Not exceeding capacity");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid: Is exceeding capacity");
                return false;
            }
        }

        //Demand 2
        //public bool PassengersInsideWhereNotBellowZero(int passengersLeaving, int passengersEntering, int passengersInsideWhenArriving)
        public bool PassengersInsideWhereNotBellowZero(int passengersInsideWhenLeaving)
        {
            //if ((passengersInsideWhenArriving - passengersLeaving + passengersEntering) > 0)
            if (passengersInsideWhenLeaving >= 0)
            {
                Console.WriteLine("Valid: Not bellow zero when leaving station");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid: Is bellow zero when leaving station");
                return false;
            }
        }

        //Demand 3
        public bool NoPassengerIsStayingAtTheStationWhenCouldHaveEntered(int passengersWaiting, int passengersInsideWhenLeaving, int passengerCapacityu)
        {
            var seatsAvailableWhenLeaving = passengerCapacityu - passengersInsideWhenLeaving;

            if (!(seatsAvailableWhenLeaving > 0 && passengersWaiting > 0))
            {
                Console.WriteLine("Valid: Noone was waiting when could have entered");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid: One or more were waiting when they could have entered");
                return false;
            }
        }

        //Demand 4
        public bool TrainRouteStartsEmpty(int passengersInside)
        {
            if (passengersInside == 0)
            {
                Console.WriteLine("Valid: Train starts empty");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid: Train did not start empty");
                return false;
            }
        }

        //Demand 5
        public bool NooneLeavsTrainWhenComingToFirstStation(int passengersLeaving)
        {
            if (passengersLeaving == 0)
            {
                Console.WriteLine("Valid: Noone left the train when arrived to the first station");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid: Noone is supposed to be on the train when arriving to the first station");
                return false;
            }
        }

        //Demand 6
        public bool NooneEntersTrainAtFinalDestination(int passengersEntering)
        {
            if (passengersEntering == 0)
            {
                Console.WriteLine("Valid: Noone entered the train at the final destination");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid: Someone entered at the final destination");
                return false;
            }
        }

        //Demand 7
        public bool NooneWaitsAtFinalDestination(int passengersWaiting)
        {
            if (passengersWaiting == 0)
            {
                Console.WriteLine("Valid: Noone waited for a train at the final destination");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid: Someone waited for a train at the final destination");
                return false;
            }
        }

        //Demand 8
        public bool TrainRouteEndsEmpty(int passengersInside)
        {
            if (passengersInside == 0)
            {
                Console.WriteLine("Valid: Train ends empty");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid: Train did not end empty");
                return false;
            }
        }
    }

    public class Route
    {
        private int passegersStartStation = 0;
        private int passegersEndStation = 0;
        private int stops = 0;
        private bool isPossible = false;

        public int PassegersEndStation
        {
            get
            {
                return passegersEndStation;
            }
            set
            {
                if (value == 0)
                {
                    passegersEndStation = value;
                    //TODO: possible
                }
                else
                {
                    passegersEndStation = value;
                    //TODO: impossible
                }
            }
        }
        public int PassegersStartStation
        {
            get
            {
                return passegersStartStation;
            }
            set
            {
                if (value == 0)
                {
                    passegersStartStation = value;
                    //TODO: possible
                }
                else
                {
                    passegersStartStation = value;
                    //TODO: impossible
                }
            }
        }
        public int Stops
        {
            get
            {
                return stops;
            }
            set
            {
                if ((value >= 2) && (value <= 100))
                {
                    stops = value;
                }
                else
                {
                    //conslusion = false;
                }
            }
        }
        public bool IsPossible
        {
            get
            {
                return isPossible;
            }
            set
            {
                isPossible = value;
            }
        } //TODO: Ev bara prop
        public List<bool> SummaryDemands { get; set; }

        public Route()
        {
            SummaryDemands = new List<bool>();
        }

        //Demand 4
        public void AtStartStationTrainIsEmpty()
        {

        }

        //Demand 5
        public void AtEndingStationTrainIsEmpty()
        {

        }

        //Demand 6
        public void AtEndingStationNooneIsWaiting()
        {
            //next last station all pass is leaving
        }
    }

    public class Train
    {
        //TODO check max value
        private int passengerCapacity = 1;
        private int nrOfPassengersInside = 0;

        public int PassengerCapacity
        {
            get
            {
                return passengerCapacity;
            }
            set
            {
                //TODO: modify to Math.Pow
                if ((value >= 1) && (value <= 1000000000))
                {
                    passengerCapacity = value;
                    //TODO: Add f/t to list
                }
                else
                {

                }
            }
        }
        public int NrOfPassengersInside
        {
            get
            {
                return nrOfPassengersInside;
            }
            set
            {
                if ((value >= 0) && (value <= this.passengerCapacity))
                {
                    nrOfPassengersInside = value;
                    //TODO: Add f/t to list
                }
                else
                {

                }
            }
        }
    }

    public class Station
    {
        private int passegersLeaving = 0;
        private int passegersEntering = 0;
        private int passegersWaiting = 0;

        public int PassegersLeaving
        {
            get
            {
                return passegersLeaving;
            }
            set
            {

                passegersLeaving = value;
            }
        }
        public int PassegersEntering
        {
            get
            {
                return passegersEntering;
            }
            set
            {

                passegersEntering = value;
            }
        }
        public int PassegersWaiting
        {
            get
            {
                return passegersWaiting;
            }
            set
            {

                passegersWaiting = value;
            }
        }
    }
}
