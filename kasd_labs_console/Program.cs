using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using kasd_labs_console;


namespace kasd_labs_console
{
    internal class Program
    {
            static void Main(string[] args)
            {

                string file = "../../log.txt";
                try
                {
                    StreamWriter streamWriter = new StreamWriter(file);
                    Console.WriteLine("Enter n:");
                    int n = int.Parse(Console.ReadLine());


                    MyPriorityQueue<Request> queue = new MyPriorityQueue<Request>();


                    Random random = new Random();
                    Stopwatch stopwatch = new Stopwatch();
                    RequestInfo[] times = new RequestInfo[n * 10 + 1];

                    Request req, lastReq;
                    RequestInfo time;
                    int requestsCount, number = 1;


                    stopwatch.Start();
                    for (int i = 1; i <= n; i++)
                    {
                        requestsCount = random.Next(1, 11);
                        for (int j = 1; j <= requestsCount; j++)
                        {
                            req = new Request(random.Next(1, 6), number, i);
                            queue.Add(req);
                            time = new RequestInfo(stopwatch.Elapsed.TotalMilliseconds, req.priority, req.number, req.step);
                            times[number] = time;
                            streamWriter.Write($"add {req.number} {req.priority} {req.step}\n");
                            number++;
                        }


                        lastReq = queue.Poll();
                        double waitingTime = stopwatch.Elapsed.TotalMilliseconds - times[lastReq.number].time;
                        time = new RequestInfo(waitingTime, lastReq.priority, lastReq.number, lastReq.step);
                        times[lastReq.number] = time;
                        streamWriter.Write($"remove {lastReq.number} {lastReq.priority} {lastReq.step}\n");
                    }

                    while (!queue.IsEmpty())
                    {
                        lastReq = queue.Poll();
                        double waitingTime = stopwatch.Elapsed.TotalMilliseconds - times[lastReq.number].time;
                        time = new RequestInfo(waitingTime, lastReq.priority, lastReq.number, lastReq.step);
                        times[lastReq.number] = time;
                        streamWriter.Write($"remove " + $"{lastReq.number} " + $"{lastReq.priority} " + $"{lastReq.step}\n");
                    }
                    stopwatch.Stop();

                    int maxIndex = 1;
                    for (int i = 2; i < times.Length; i++)
                    {
                        if (times[i].time > times[maxIndex].time) { maxIndex = i; }
                    }

                    Console.Write("max time :" + times[maxIndex].time);
                    Console.WriteLine();
                    Console.Write("num : " + times[maxIndex].number);
                    Console.WriteLine();
                    Console.Write("priority : " + times[maxIndex].priority);
                    Console.WriteLine();
                    Console.Write("iteration :" + times[maxIndex].step);
                    Console.WriteLine();

                    streamWriter.Close();

                    Console.ReadLine();
                }
                catch (Exception exep) { Console.WriteLine(exep.ToString()); }
            }
            static int Comparison(Request request1, Request request2)
            {
                return request1.priority - request2.priority;
            }
        }

    }
        
