using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace OpenTelemtryIntegration
{

    public class OpenTelemetryBuilder
    {
        private class MyCollection<T> : ICollection<T>
        {
            private List<T> _list;
            string filepath;

            public MyCollection(string filepath)
            {
                _list = new List<T>();
                this.filepath = filepath;
            }

            public int Count => _list.Count;

            public bool IsReadOnly => false;

            public void Add(T item)
            {
                _list.Add(item);
                File.AppendAllText(filepath, "Baggage: \n");
                foreach(var x in Baggage.Current.GetBaggage())
                {
                    File.AppendAllText
                        (filepath, string.Format("{0}: {1}\n", x.Key, x.Value));
                }

            }

            public void Clear()
            {
                _list.Clear();
            }

            public bool Contains(T item)
            {
                return _list.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            public bool Remove(T item)
            {
                return _list.Remove(item);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
               return _list.GetEnumerator();
            }
        }

        const string serviceName = "web-goat";
        static MyCollection<Activity> exportedItemsTrace = new MyCollection<Activity>("C:\\Users\\jruszil\\Desktop\\optm_research\\baggage.txt");

        public static void AddOpenTelemetry(IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName)).WithTracing(builder => builder.AddAspNetCoreInstrumentation());
        }
    }
}