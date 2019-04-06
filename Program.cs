using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CoreAsync
{
    class Program
    {
        //Version 1 All task launched but await in static order : Eggs, then Bacon and finally Bread
        // static async Task Main(string[] args)
        // {
        //     Object cup = PourCoffee();
        //     Console.WriteLine("GO !!!!!");
        //     //Launch All task one after the other without waiting
        //     Task<Object> taskEggs = FryEggs(5000);
        //     Task<Object> taskBacon = FryBacon(7000);
        //     Task<Object> taskBread = ToastBread(2000);

        //     Console.WriteLine("All task are launched ... ");

        //     Object eggs = await taskEggs;
        //     Console.WriteLine("eggs are ready 5s");
        //     Object bacon = await taskBacon;
        //     Console.WriteLine("bacon is ready 7s");
        //     Object toast = await taskBread;
        //     Console.WriteLine("bread is ready : 2s");
        //     ApplyButter(toast);
        //     ApplyJam(toast);
        //     Console.WriteLine("toast is ready");
        //     Object oj = PourOJ();
        //     Console.WriteLine("oj is ready");

        //     Console.WriteLine("Breakfast is ready!");
        // }


    //Version 2 All task launched and await in first finished order
        static async Task Main(string[] args)
        {
            Object cup = PourCoffee();
            Console.WriteLine("GO !!!!!");
            //Launch All task one after the other without waiting
            Task<Object> taskEggs = FryEggs(5000);
            Task<Object> taskBacon = FryBacon(7000);
            Task<Bread> taskBread = ToastBread(2000);
            
            Console.WriteLine("All task are launched ... ");
            
            
            List<Task> allTasks = new List<Task>{taskBacon, taskBread, taskEggs};
            while(allTasks.Count != 0)
            {
                //Await any task to finish
                Task justFinishedTask = await Task.WhenAny(allTasks);
                //Let's see wich opne is finished
                if(justFinishedTask == taskEggs)
                {
                    allTasks.Remove(taskEggs);
                    Console.WriteLine("eggs are ready 5s");
                }
                else if(justFinishedTask == taskBacon)
                {
                    allTasks.Remove(taskBacon);
                    Console.WriteLine("bacon is ready 7s");
                }
                else if(justFinishedTask == taskBread)
                {
                    Console.WriteLine("State of received Bread form async processus is : " + taskBread.Result._dirtyState);
                    allTasks.Remove(taskBread);
                    ApplyButter(taskBread.Result);
                    ApplyJam(taskBread.Result);
                    Console.WriteLine("bread is ready : 2s");
                }
                else
                {
                    //Strange .. must have missed something...
                    allTasks.Remove(justFinishedTask);
                }
            }

            Object oj = PourOJ();
            Console.WriteLine("oj is ready");

            Console.WriteLine("Breakfast is ready!");
        }

        private static object PourOJ()
        {
            return new Object();
        }

        private static void ApplyJam(object toast)
        {
            //Dummy
        }

        private static void ApplyButter(object toast)
        {
            //Dummy
        }

        private async static Task<Bread> ToastBread(int v)
        {
            await Task.Delay(v);
            Bread retour = new Bread();
            retour._dirtyState = "Initialized";
            return retour;

        }

        private async static Task<object> FryBacon(int v)
        {
            await Task.Delay(v);
            return new Object();
        }

        private async static Task<object> FryEggs(int v)
        {
            await Task.Delay(v);
            return new Object();
        }

        private static Object PourCoffee()
        {
            return new Object();
        }
    }
}
