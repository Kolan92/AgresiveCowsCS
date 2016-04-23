using System;
using System.Collections.Generic;
using System.Linq;

namespace AgresiveCowsCS
{
    

    class Program {
        public struct Distance {
            public long dist;
            public long start;
            public long end;

            public Distance(long dist, long start, long end) {
                this.dist = dist;
                this.start = start;
                this.end = end;
            }

            public bool overlap(Distance distance) {
                var result = Math.Max(start, distance.start) < Math.Min(end, distance.end);
                return result;
            }
        }

        static void Main(string[] args) {
            try {
                var t = long.Parse(Console.ReadLine());
                for (int i = 0; i < t; i++) {
                    var NC = Console.ReadLine().Split(' ');
                    var stallNumbers = long.Parse(NC[0]);
                    var intervalsNumber = long.Parse(NC[1]) - 1;

                    var stallsLocations = new long[stallNumbers];
                    for (int j = 0; j < stallNumbers; j++) {
                        stallsLocations[j] = long.Parse(Console.ReadLine());
                    }

                    var distances1 = new List<Distance>();
                    IList<long[]> result = new List<long[]>();

                    foreach (var stall1 in stallsLocations) {
                        foreach (var stall2 in stallsLocations) {
                            if (stall2 - stall1 > 0)
                                distances1.Add(new Distance(stall2 - stall1, stall1, stall2));
                        }
                    }

                    var distances = distances1.OrderByDescending(x => x.dist).ToArray();
                    var counter = 1;
                    var intervals = new long[intervalsNumber];
                    var lenth = distances.Length;
                    for (int k = 0; k < lenth; k++) {
                        if (counter == intervalsNumber) {
                            result.Add((long[])intervals.Clone());
                        }
                        var tempDist = distances[k].dist;
                        counter = 1;


                        intervals[counter - 1] = distances[k].dist;

                        for (int l = k + 1; l < lenth; l++) {
                            if (!distances[k].overlap(distances[l])) {
                                if (counter < intervalsNumber) {
                                    if (distances[l].dist < tempDist) {
                                        counter++;
                                        tempDist = distances[l].dist;
                                        intervals[counter - 1] = distances[l].dist;
                                    }
                                }
                            }
                        }
                    }

                    var max = result.Select(x => x.Min()).Max();
                    Console.WriteLine(max);
                }
            }
            catch {
                return;
            }
        }
    }
}
