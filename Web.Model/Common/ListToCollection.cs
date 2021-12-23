using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Web.Model.Common
{
    public static class ExtensionMethods
    {
        public static Collection<T> ToCollection<T>(this IList<T> items)
            {
                Collection<T> collection = new Collection<T>();

                for (int i = 0; i < items.Count; i++)
                {
                    collection.Add(items[i]);
                }

                return collection;
            }
        }
    }
