using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DirtyMergeSort
{
    public static class Dms
    {
        public static IEnumerable<string> Sorted(this IEnumerable<string> lines, int size)
        {
            var files = lines.Partition(size)
                .Select(x => new {File = x, Lines = File.ReadLines(x).GetEnumerator()})
                .Where(x => x.Lines.MoveNext())
                .ToList();

            while(files.Any())
            {
                var current = files.Min(x => x.Lines.Current);
                yield return current;

                files = files.Where(x =>
                {
                    while(x.Lines.Current == current)
                    {
                        if(!x.Lines.MoveNext())
                        {
                            File.Delete(x.File);
                            Debug.WriteLine(string.Format("Deleted file {0}", x.File));
                            return false;
                        }
                    }
                    return true;
                }).ToList();
            }
        }

        public static IEnumerable<string> Partition(this IEnumerable<string> lines, int size)
        {
            return lines.Batch(size)
                .AsParallel()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Select(batch =>
                {
                    var temp = Path.GetTempFileName();
                    File.WriteAllLines(temp, batch.Where(w => w.Length > 1).OrderBy(x => x.Trim().Split('.')[1]).ThenBy(x => Convert.ToInt32(x.Trim().Split('.')[0])));
                    Debug.WriteLine(string.Format("Wrote file {0}", temp));
                    return temp;
                });
        }

        public static IEnumerable<List<T>> Batch<T>(this IEnumerable<T> collection, int size)
        {
            var batch = new List<T>(size);
            foreach(var item in collection)
            {
                batch.Add(item);
                if(batch.Count == size)
                {
                    yield return batch;
                    batch = new List<T>(size);
                }
            }

            if(batch.Any())
            {
                yield return batch;
            }
        }
    }
}