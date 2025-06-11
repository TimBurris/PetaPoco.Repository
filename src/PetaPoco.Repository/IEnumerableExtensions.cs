using System;
using System.Collections.Generic;
using System.Linq;

namespace PetaPoco.Repository
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(
            this IEnumerable<T> source, int size)
        {
            T[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new T[size];

                bucket[count++] = item;

                if (count != size)
                    continue;

                yield return bucket.Select(x => x);

                bucket = null;
                count = 0;
            }

            // Return the last bucket with all remaining elements
            if (bucket != null && count > 0)
            {
                Array.Resize(ref bucket, count);
                yield return bucket.Select(x => x);
            }
        }



        public static IEnumerable<TDest> ProcessInBatches<TSource, TDest>(this IEnumerable<TSource> sources, int batchSize, Func<IEnumerable<TSource>, IEnumerable<TDest>> code)
        {
            var results = new List<TDest>();
            if (sources == null || !sources.Any())
                return results;

            if (sources.Count() < batchSize)
            {
                //if the ids are less than the batch size, just run the query
                results.AddRange(code(sources));
            }
            else
            {

                var batches = sources.Batch(batchSize);
                foreach (var batch in batches)
                {
                    results.AddRange(code(batch));
                }
            }

            return results;
        }
    }
}