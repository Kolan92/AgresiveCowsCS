using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgresiveCowsCS
{
    public class Distance : ICloneable {
        public int dist { get; set; }
        public int start { get; set; }
        public int end { get; set; }

        public Distance(int dist, int start, int end) {
            this.dist = dist;
            this.start = start;
            this.end = end;
        }

        public bool overlap(Distance distance) {
            var result = Math.Max(start, distance.start) < Math.Min(end, distance.end);
            return result;
        }

        public object Clone() {
            return this.MemberwiseClone();
        }
    }

    class Program {
        static void Main(string[] args) {
            var t = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < t; i++) {
                var NC = Console.ReadLine().Split(' ');
                var N = Int32.Parse(NC[0]);
                var C = Int32.Parse(NC[1]);

                var stallsLocations = new List<int>();
                for (int j = 0; j < N; j++) {
                    stallsLocations.Add(Int32.Parse(Console.ReadLine()));
                }

                IList<Distance> distances = new List<Distance>();
                var counter = 1;
                IList<Distance> tempList = new List<Distance>();
                List<List<int>> xxx = new List<List<int>>();
                var tempDist = 0;

                foreach (var stall1 in stallsLocations) {
                    foreach (var stall2 in stallsLocations) {
                        if (stall2 - stall1 > 0)
                            distances.Add(new Distance(stall2 - stall1, stall1, stall2));
                    }
                }
                distances =  distances.OrderByDescending(x => x.dist).ToList();

                var results = new List<KeyValuePair<int, IList<Distance>>>();
                for (int k = 0; k < distances.Count; k++) {
                    tempDist = distances[k].dist;
                    tempList.Add(distances[k]);
                    for (int l = k+1; l < distances.Count; l++) {
                        if(!distances[k].overlap(distances[l])) {
                            if(counter < C - 1) {
                                counter++;
                                if (distances[l].dist < tempDist || tempDist == 0) {
                                    tempDist = distances[l].dist;
                                    tempList.Add(distances[l]);
                                }
                            }
                        }    
                        
                        if(l == distances.Count - 1) {
                            if(counter == C - 1) {
                                var listToClone = Extensions.Clone(tempList);
                                results.Add(new KeyValuePair<int, IList<Distance>>(tempDist, listToClone));
                                xxx.Add(tempList.Select(x=>x.dist).ToList());
                            }
                            counter = 1;
                            tempList.Clear();
                        }
                    }
                }

                //var max = results.Max();
                var max = xxx.Select(x => x.Min()).Max();
                Console.WriteLine(max);
                Console.ReadLine();
            }
        }
    }

    static class Extensions {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
